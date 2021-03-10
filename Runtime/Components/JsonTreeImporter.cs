using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elysium.ProgressTree.Json
{
    public class JsonTreeImporter : TreeImporter
    {
        protected override void Import() => ProgressTree = ProgressTree.FromJson(nodeElementDatabase.Elements, dependencyDatabase.ElementsAsInterface, importedTextAsset);

        protected override bool IsValidFormat()
        {
            // TODO: CHECK IF ITS A VALID JSON FILE
            return true;
        }
    }
}