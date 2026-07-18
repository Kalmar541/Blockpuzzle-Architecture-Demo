using System;
using UnityEngine;

namespace kalmar541.Pool
{
    public interface ISimplePool<T> where T : MonoBehaviour
    {
        event Action<T> OnCreate;

        T Get();
        void Release(T obj);
    }
}