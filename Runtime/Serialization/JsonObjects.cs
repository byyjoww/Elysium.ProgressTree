using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.ProgressTree.Json
{
    [System.Serializable]
    public struct JsonRequirementCollection
    {
        [SerializeField] private JsonRequirement[] requirements;

        public IReadOnlyList<JsonRequirement> Requirements => requirements;

        public static ITreeRequirement[] FromJson(string _json)
        {
            return JsonUtility.FromJson<JsonRequirementCollection>(_json).requirements.Cast<ITreeRequirement>().ToArray();
        }
    }

    [System.Serializable]
    public struct JsonRequirement : ITreeRequirement
    {
        [SerializeField] private string name;
        [SerializeField] private int cost;
        [SerializeField] private JsonDependency[] dependencies;

        public string Name => name;
        public int Cost => cost;
        public Dictionary<string, int> Dependencies => dependencies.ToDictionary(x => x.Name, x => x.Level);
    }

    [System.Serializable]
    public struct JsonDependency
    {
        [SerializeField] private string name;
        [SerializeField] private int level;

        public string Name => name;
        public int Level => level;
    }
}

