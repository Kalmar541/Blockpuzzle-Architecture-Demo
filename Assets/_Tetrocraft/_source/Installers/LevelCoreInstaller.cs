using GameProject.Game;
using GameProject.Input;
using UnityEngine;
using Zenject;

public class LevelCoreInstaller : MonoInstaller
{
    [SerializeField] private LevelUpConfig _levelUpConfig;
    [SerializeField] private ScoreConfig _scoreConfig;
    [SerializeField] private Vector2Int _sizeArea = new(10,24);   
    
    public override void InstallBindings()
    {
        GameInput inputActions = Container.Resolve<GameInput>();
        StringCollectionPool<BlockView> poolBlock = Container.Resolve<StringCollectionPool<BlockView>>();

        LevelHandler levelHandler = new LevelHandler();
        Container.BindInterfacesAndSelfTo<LevelHandler>().FromInstance(levelHandler).AsSingle().NonLazy();

        TetraminoContainer spawnerTetramino = new(200, levelHandler);
        Container.Bind<TetraminoContainer>().FromInstance(spawnerTetramino).AsSingle().NonLazy();

        GameArea gameArea = new(_sizeArea.x, _sizeArea.y);
        Container.Bind<GameArea>().FromInstance(gameArea).AsSingle().NonLazy();

        ProgressLinesHandler progressLinesHandler = new(new(), _levelUpConfig, gameArea);
        Container.BindInterfacesAndSelfTo<ProgressLinesHandler>().FromInstance(progressLinesHandler).AsSingle().NonLazy();

        DifficalHandler difficalHandler = new(progressLinesHandler);
        Container.Bind<DifficalHandler>().FromInstance(difficalHandler).AsSingle().NonLazy();

        ShapeEngine tetraminoEngine = new(gameArea, difficalHandler);
        Container.BindInterfacesAndSelfTo<ShapeEngine>().FromInstance(tetraminoEngine).AsSingle().NonLazy();

        InputHandler inputHandler = new(tetraminoEngine, inputActions);
        Container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(inputHandler).AsSingle().NonLazy();

        ScoreHandler scoreHandler = new(new(), progressLinesHandler, _scoreConfig);
        Container.BindInterfacesAndSelfTo<ScoreHandler>().FromInstance(scoreHandler).AsSingle().NonLazy();
    }
}