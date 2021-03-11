using UnityEngine;

namespace Elysium.ProgressTree.Json
{
    public class JsonTreeImporter : TreeImporter
    {
        protected override void Import()
        {
            ProgressTree = ProgressTree
                .Create(nodeElementDatabase.ElementsAsInterface, dependencyDatabase.ElementsAsInterface)
                .FromJson(importedTextAsset);
        }

        protected override bool IsValidFormat()
        {
            // TODO: CHECK IF ITS A VALID JSON FILE
            return true;
        }
    }
}