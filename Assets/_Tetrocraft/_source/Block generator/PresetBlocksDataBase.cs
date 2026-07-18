using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Preset Blocks DB", menuName = "Scriptable Objects/Preset Blocks DB")]
public class PresetBlocksDataBase : ScriptableObject
{
    [field: SerializeField] public PresetBlockGroup[] Content { get; private set; }

    private Dictionary<PresetBlockType, PresetBlockGroup> _database;

    private void OnEnable()
    {
        _database = new();

        if (Content == null) return;

        foreach (var preset in Content)
        {
            if (preset == null) throw new NullReferenceException("Daba base blocks preset is NOT VALID");

            if (_database.ContainsKey(preset.PresetBlockType)) throw new ArgumentException("Data base blocks preset has duplicates!");

            _database[preset.PresetBlockType] = preset;
        }
    }

    public bool HasPreset(PresetBlockType presetType)
    {
        return _database.ContainsKey(presetType);
    }

    public PresetBlockGroup GetPresetGroup(PresetBlockType presetBlockType)
    {
        if (_database.TryGetValue(presetBlockType, out PresetBlockGroup presetBlockGroup))
            return presetBlockGroup;
        else
            return null;
    }
}