using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpConfig", menuName = "Scriptable Objects/LevelUpConfig")]
public class LevelUpConfig : ScriptableObject
{
    public List<LevelBlockKit> LinesForUp = new();
}

[System.Serializable]
public struct LevelBlockKit
{
    public int LinesNeed;
    [NaughtyAttributes.Expandable]
    public PresetBlockGroup ConfigBlocks;
}