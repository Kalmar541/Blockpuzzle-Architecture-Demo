using UnityEngine;

[CreateAssetMenu(fileName = "Preset Block Group", menuName = "Scriptable Objects/Preset Block Group")]
public class PresetBlockGroup : ScriptableObject
{
    [field: SerializeField] public PresetBlockType PresetBlockType { get; private set; }
    public BlockPreset[] Presets;
}