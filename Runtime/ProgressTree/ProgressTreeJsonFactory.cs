using Elysium.ProgressTree.Json;
using UnityEngine;

namespace Elysium.ProgressTree
{
    public partial class ProgressTree
    {
        public static ProgressTree FromJson(INodeElement[] _allNodeElements, INodeElement[] _allDependencies, TextAsset _json)
        {
            Debug.Log($"Building progressTree from JSON:\n{_json.text}");

            ITreeRequirement[] requirements = JsonRequirementCollection.FromJson(_json.text);
            return new ProgressTree(_allNodeElements, _allDependencies, requirements);
        }
    }
}