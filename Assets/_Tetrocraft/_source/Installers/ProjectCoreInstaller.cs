using System.Collections.Generic;
using GameProject.Game;
using GameProject.Input;
using UnityEngine;
using Zenject;

public class ProjectCoreInstaller : MonoInstaller
{
    [SerializeField] private PresetBlocksDataBase _presetBlocksDataBase;
    [SerializeField] private ShapeBluePrint[] _pieceBluePrints;
    
    public override void InstallBindings()
    {
        GameInput playerInput = new();
        playerInput.Enable();
        Container.Bind<GameInput>().FromInstance(playerInput).AsSingle().NonLazy();

        BlockGenerator tetraminoGenerator = new(_presetBlocksDataBase);
        Container.Bind<BlockGenerator>().FromInstance(tetraminoGenerator).AsSingle().NonLazy();

        ShapeDefinition[] pieces = new ShapeDefinition[_pieceBluePrints.Length];
        for (int i = 0; i < _pieceBluePrints.Length; i++)
        {
            pieces[i] = (PieceDefinitionWrapper.CastPieceDefinition(_pieceBluePrints[i]));
        }

        ShapeDefinitionDatabase pieceDefinitionDB = new(pieces);
        Container.Bind<ShapeDefinitionDatabase>().FromInstance(pieceDefinitionDB).AsSingle().NonLazy();
    }
}