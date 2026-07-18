using System;
using GameProject.Game;

public class BlockGenerator
{
    private readonly PresetBlocksDataBase _presetDatabase;
    private readonly Random _random = new();

    public BlockGenerator(PresetBlocksDataBase presetDatabase)
    {
        _presetDatabase = presetDatabase ?? throw new ArgumentNullException(nameof(presetDatabase));
    }

    public Block[] GenerateBlocks(ShapeDefinition definition, PresetBlockType presetType)
    {
        if (definition == null)
            throw new ArgumentNullException(nameof(definition));

        if (!_presetDatabase.HasPreset(presetType))
            throw new ArgumentException($"Unknown preset type: {presetType}");

        var presetGroup = _presetDatabase.GetPresetGroup(presetType);
        var blockCount = definition.BlockCount;
        var blocks = new Block[blockCount];

        for (int i = 0; i < blockCount; i++)
        {
            var blockPrefab = GetRandomBlock(presetGroup);
            var block = new Block(blockPrefab.MaterialType, blockPrefab.KeyPrefab);
            blocks[i] = block;
        }

        return blocks;
    }

    private BlockView GetRandomBlock(PresetBlockGroup presetGroup)
    {
        var presetIndex = _random.Next(0, presetGroup.Presets.Length);
        var preset = presetGroup.Presets[presetIndex];

        var variantIndex = _random.Next(0, preset.BlockVariants.Length);
        return preset.BlockVariants[variantIndex];
    }
}
