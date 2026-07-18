using System;
using UnityEngine;

namespace kalmar541.Pool
{
    public interface ICollectionPool<TKey, TPrefab> : IDisposable
        where TPrefab : MonoBehaviour, IPoolElement<TKey>
    {
        event Action<TPrefab> OnCreateObject;
        event Action<TPrefab> OnGetObject;
        event Action<TPrefab> OnReleaseObject;

        void AddNewElement(TPrefab prefab);
        bool IsHasObject(TKey key);
        TPrefab GetObject(TKey key);
        TPrefab GetRandomObject();
        bool ReleaseObject(TPrefab prefab);
        void ReleaseAll();
    }
}