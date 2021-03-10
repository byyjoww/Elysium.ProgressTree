using Elysium.Utils.Attributes;
using UnityEngine;

namespace Elysium.ProgressTree
{
    public abstract class TreeImporter : MonoBehaviour
    {
        [Separator("Text Asset (Json/Yaml/Etc)", true)]
        [SerializeField] protected TextAsset importedTextAsset = default;

        [Separator("Runtime Progress Tree", true)]
        [SerializeField, ReadOnly] protected ProgressTree ProgressTree;

        [Separator("Database of Tree Elements", true)]
        [SerializeField] protected TestNodeElementSODatabase nodeElementDatabase = new TestNodeElementSODatabase();

        [Separator("Database of Tree Dependencies", true)]
        [SerializeField] protected DependencyDatabase dependencyDatabase = new DependencyDatabase();

        protected abstract void Import();

        protected abstract bool IsValidFormat();

        private void Start() => Import();

        private void OnValidate()
        {
            nodeElementDatabase.Refresh();
            dependencyDatabase.Refresh();

            if (!IsValidFormat())
            {
                Debug.LogError("Text Asset isn't in a valid format");
                importedTextAsset = null;
            }
        }
    }
}