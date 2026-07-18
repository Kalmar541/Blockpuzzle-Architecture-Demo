using GameProject.Game;
using UnityEngine;

[System.Serializable]
public struct BlockPreset 
{
    [field: SerializeField] public BlockView[] BlockVariants { get; private set;}  
}