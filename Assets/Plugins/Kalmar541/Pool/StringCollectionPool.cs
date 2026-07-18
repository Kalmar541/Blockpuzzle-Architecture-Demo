using UnityEngine;

public class StringCollectionPool<TPrefab> : CollectionPool<string, TPrefab>
    where TPrefab : MonoBehaviour, IPoolElement<string>
{
    public StringCollectionPool(TPrefab[] collection = null, string namePool = default, Transform parent = null)
        : base(collection, namePool, parent)
    {
    }
}