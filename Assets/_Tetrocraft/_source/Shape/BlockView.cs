using NaughtyAttributes;
using UnityEngine;

namespace GameProject.Game
{
    public class BlockView : MonoBehaviour, IPoolElement<string>
    {
        [System.Serializable]
        public class BlockPrefabData
        {
            public string Id;
            public MaterialType MaterialType;
        }

        [SerializeField] private BlockPrefabData _prefabData;

        public string KeyPrefab => _prefabData.Id;
        public MaterialType MaterialType => _prefabData.MaterialType;
        public Block Data { get; private set; }

        public void Init(Block blockData)
        {
            Data = blockData;
            SetVisible(true);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }

        public void Reboot()
        {
            Data = null;
            SetVisible(false);
        }

#if UNITY_EDITOR
        [Button]
        private void CreateIdFromName()
        {
            _prefabData.Id = gameObject.name;
        }
#endif
    }
}