using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.ProgressTree
{
    [System.Serializable]
    public class Node
    {
        [SerializeField] private ScriptableObject element;
        [SerializeField] private int level = 0;
        [SerializeField] private int maxLevel = 10;
        [SerializeField] private int cost = 1;
        [SerializeField] private bool isAvailable = false;
        [SerializeField] private Dependency[] dependencies;

        public INodeElement Element => element as INodeElement;
        public int Level => level;
        public int MaxLevel => maxLevel;
        public int Cost => cost;
        public bool IsAvailable => isAvailable;
        public IReadOnlyList<Dependency> Dependencies => dependencies;

        public Node(INodeElement _element, int _maxLevel, Dependency[] _dependencies, int _cost)
        {
            element = (ScriptableObject)_element;
            maxLevel = _maxLevel;
            dependencies = _dependencies;
            cost = _cost;
            level = 0;
            isAvailable = false;
        }

        public void IncreaseLevel() => level++;

        public bool TryToUnlock(INodeElement[] _achieved)
        {
            if (_achieved == null || _achieved.Length < 1)
            {
                Debug.Log("list of achieved progress doesn't have any elements");
                return false;
            }

            foreach (var dependency in Dependencies)
            {
                var achieve = _achieved.SingleOrDefault(x => x == dependency.Requirement);
                if (achieve == null) { throw new System.Exception("required dependency isn't present in skill tree"); }
                if (achieve.Level < dependency.Level) { return isAvailable; }
            }

            isAvailable = true;
            return isAvailable;
        }
    }
}