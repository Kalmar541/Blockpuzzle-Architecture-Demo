using KG.Viewports;
using UnityEngine;
using Zenject;

public class EventBusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        IViewportHandler viewportHandler = Container.Resolve<IViewportHandler>();
        LevelHandler levelHandler = Container.Resolve<LevelHandler>();

        UILevelHandler uILevelHandler = new(viewportHandler, levelHandler);
        Container.BindInterfacesAndSelfTo<UILevelHandler>().FromInstance(uILevelHandler).AsSingle().NonLazy();
    }
}