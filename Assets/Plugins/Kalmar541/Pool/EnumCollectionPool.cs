using System;
using UnityEngine;

public class EnumCollectionPool<TPrefab> : CollectionPool<Enum, TPrefab>
    where TPrefab : MonoBehaviour, IPoolElement<Enum>
{
    public EnumCollectionPool(TPrefab[] collection = null, string namePool = default, Transform parent = null)
        : base(collection, namePool, parent)
    {
    }
}