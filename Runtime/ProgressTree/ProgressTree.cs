using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Elysium.ProgressTree
{
    [System.Serializable]
    public partial class ProgressTree
    {
        [SerializeField] protected List<Node> graph;

        public event UnityAction<Node> OnNodeChanged;
        public event UnityAction OnValueChanged;

        protected IReadOnlyList<Node> Graph => graph;        
        protected virtual INodeElement[] CurrentProgress => AllDependencies;
        protected INodeElement[] AllNodeElements { get; set; }
        protected INodeElement[] AllDependencies { get; set; }

        private ProgressTree()
        {
            graph = new List<Node>();
        }

        public static ProgressTree Create()
        {
            return new ProgressTree();
        }

        public void Build(INodeElement[] _allNodeElements, INodeElement[] _allDependencies, ITreeRequirement[] _requirements)
        {
            this.AllNodeElements = _allNodeElements;
            this.AllDependencies = _allDependencies;
            BuildNodes(_requirements);
        }

        public Node GetNode(INodeElement _element)
        {
            var node = Graph.SingleOrDefault(x => x.Element.Equals(_element));
            return node;
        }

        public void IncreaseNodeLevel(INodeElement _element)
        {
            var node = GetNode(_element);
            IncreaseNodeLevel(node);
            return;
        }

        public void IncreaseNodeLevel(Node _node)
        {
            if (_node == null) { throw new Exception("Node doesnt exist"); }
            if (!_node.TryToUnlock(CurrentProgress)) { Debug.LogError("Can't learn node yet"); return; }
            if (_node.Level >= _node.MaxLevel) { Debug.LogError("Node is already at max level"); return; }

            _node.IncreaseLevel();
            OnNodeChanged?.Invoke(_node);
            OnValueChanged?.Invoke();
            TryToUnlockAllNodes();
            return;
        }

        public void TryToUnlockAllNodes()
        {

            for (int i = 0; i < Graph.Count; i++)
            {
                var prev = Graph[i].IsAvailable;
                var current = Graph[i].TryToUnlock(CurrentProgress);
                if (current != prev)
                {
                    OnNodeChanged?.Invoke(Graph[i]);
                    OnValueChanged?.Invoke();
                }
            }
        }

        protected virtual void InvokeOnStatusChanged(Node _node)
        {
            OnNodeChanged?.Invoke(_node);
        }

        protected void BuildNodes(ITreeRequirement[] _requirements)
        {
            if (_requirements.Length < 1) 
            { 
                Debug.LogError($"No elements in the list of requirements"); 
                return; 
            }

            Debug.Log($"List of requirements received. Building Progress Tree:");
            foreach (var requirement in _requirements)
            {
                // TODO: REMOVE DEBUG LOGS
                Debug.Log($"Requirement: {requirement.Name} | Cost: {requirement.Cost}");
                foreach (var kvp in requirement.Dependencies) { Debug.Log($"Dependency => Name: {kvp.Key} | Level: {kvp.Value}"); }

                if (!TryExtractNode(requirement, out INodeElement nodeElement))
                {
                    Debug.LogError($"Couldn't find element for {requirement.Name} in database");
                    continue;
                }

                if (!TryExtractDependencies(requirement, out List <Dependency> dependencies))
                {
                    Debug.LogError($"Couldn't find dependencies for {requirement.Name} in database");
                    continue;
                }

                var node = new Node(nodeElement, nodeElement.MaxLevel, dependencies.ToArray(), requirement.Cost);
                graph.Add(node);
                InvokeOnStatusChanged(node);
            }

            TryToUnlockAllNodes();
        }

        private bool TryExtractNode(ITreeRequirement _requirement, out INodeElement _element)
        {
            _element = AllNodeElements.SingleOrDefault(x => x.ElementName == _requirement.Name);
            return _element != null;
        }

        private bool TryExtractDependencies(ITreeRequirement requirement, out List<Dependency> _dependencies)
        {
            List<Dependency> dependencies = new List<Dependency>();
            foreach (var kvp in requirement.Dependencies)
            {
                INodeElement dependencyElement = AllDependencies.SingleOrDefault(x => x.ElementName == kvp.Key);
                if (dependencyElement == null)
                {
                    Debug.LogError($"couldn't find json dependency {kvp.Key} in dependency database");
                    continue;
                }

                dependencies.Add(new Dependency(dependencyElement, kvp.Value));
            }

            _dependencies = dependencies;
            return _dependencies != null && _dependencies.Count > 0;
        }
    }
}