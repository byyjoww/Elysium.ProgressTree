using System.Collections.Generic;
using UnityEngine;

namespace Elysium.ProgressTree
{
    public interface ITreeRequirement
    {
        string Name { get; }
        int Cost { get; }
        Dictionary<string, int> Dependencies { get; }
    }
}