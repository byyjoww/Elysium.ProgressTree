using UnityEngine;

namespace Elysium.ProgressTree.Json
{
    public static class ProgressTreeFactory
    {
        public static ProgressTree FromJson(this ProgressTree _tree, INodeElement[] _allNodeElements, INodeElement[] _allDependencies, TextAsset _json)
        {
            Debug.Log($"Building progressTree from JSON:\n{_json.text}");
            ITreeRequirement[] requirements = JsonRequirementCollection.FromJson(_json.text);
            _tree.Build(_allNodeElements, _allDependencies, requirements);
            return _tree;
        }
    }
}