using Elysium.ProgressTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Tree/TestNodeElementSO")]
public class TestNodeElementSO : ScriptableObject, INodeElement
{
    public string elementName;
    public int level;
    public int maxLevel;
        
    public string ElementName => elementName;
    public int Level => level;
    public int MaxLevel => maxLevel;
}
