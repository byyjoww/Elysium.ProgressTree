using UnityEngine;

namespace Elysium.ProgressTree.Json
{
    public static class ProgressTreeFactory
    {
        public static ProgressTree FromJson(this ProgressTree _tree, TextAsset _json)
        {
            Debug.Log($"Building progressTree from JSON:\n{_json.text}");
            ITreeRequirement[] requirements = JsonRequirementCollection.FromJson(_json.text);
            _tree.Build(requirements);
            return _tree;
        }
    }
}