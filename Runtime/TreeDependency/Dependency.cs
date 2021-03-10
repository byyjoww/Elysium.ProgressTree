using UnityEngine;

namespace Elysium.ProgressTree
{
    [System.Serializable]
    public class Dependency
    {
        [SerializeField] private ScriptableObject requirement;        
        [SerializeField] [Range(1, 10)] private int level = 1;

        public INodeElement Requirement => requirement as INodeElement;
        public int Level => level;

        public Dependency(INodeElement _requirement, int _level)
        {
            this.requirement = (ScriptableObject)_requirement;
            this.level = _level;
        }
    }
}