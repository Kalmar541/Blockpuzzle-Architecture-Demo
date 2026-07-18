using GameProject.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockDataBase", menuName = "Scriptable Objects/Block DataBase")]
public class BlockDataBase : ScriptableObject
{
    [SerializeField] private BlockView[] _content;

    public IReadOnlyList<BlockView> Content => _content;

    private Dictionary<string, BlockView> _dataBase;

    private void OnEnable()
    {
        if (_content == null) return;

        _dataBase = new();

        foreach (var block in _content)
        {
            if (block == null) throw new NullReferenceException("Block DB has NULL objects!");

            if (_dataBase.TryAdd(block.name, block) == false)
                throw new ArgumentException("Block DB has duplicates!");
        }
    }


    public bool Containe(string nameBlock) => _dataBase.ContainsKey(nameBlock);

    public BlockView GetPrefab(string nameBlock)
    {
        if (_dataBase.TryGetValue(nameBlock, out BlockView blockView))
            return blockView;
        else
            return null;
    }
}
