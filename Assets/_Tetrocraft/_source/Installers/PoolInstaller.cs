using GameProject.Game;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField] private BlockDataBase _blockDataBase;
    public override void InstallBindings()
    {
       StringCollectionPool<BlockView> poolBlock = new(_blockDataBase.Content.ToArray());
        Container.Bind(typeof(IDisposable), typeof(StringCollectionPool<BlockView>)).FromInstance(poolBlock).AsSingle().NonLazy();
    }
}