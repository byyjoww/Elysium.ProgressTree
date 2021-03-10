using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elysium.ProgressTree
{
    public interface INodeElement
    {
        string ElementName { get; }
        int Level { get; }
        int MaxLevel { get; }
    }
}