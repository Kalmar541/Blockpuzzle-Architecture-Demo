using GameProject.Game;
using kalmar541.Pool;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameProject
{
    public class BoardRenderer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _activePieceContainer;
        [SerializeField] private BlockView _prefabBoardWall;
        [SerializeField] private ShapeView _prefabTetraminoView;

        [Header("Settings")]
        [SerializeField] private int _borderWidth = 1;

        private GameArea _gameArea;
        private ShapeEngine _engine;
        private ICollectionPool<string, BlockView> _blockPool;

        private BlockView[,] _gridViews;
        private List<BlockView> _walls = new();
        private Shape _activeTetramino;
        private ShapeView _instantiatedTetraminoView;
        private List<BlockView> _cacheBlockViews = new();

        private int _width;
        private int _height;
        private Vector3 _offsetPosition;

        [Inject]
        public void Construct(GameArea gameArea, ShapeEngine engine,
            StringCollectionPool<BlockView> blockPool)
        {
            _gameArea = gameArea;
            _engine = engine;
            _blockPool = blockPool;
        }

        public void Init(Transform pointBoard)
        {
            _width = _gameArea.Width;
            _height = _gameArea.Height;
            _gridViews = new BlockView[_width, _height];
            _offsetPosition = pointBoard.position;

            CreateActiveTetraminoView();
            SubscribeToEvents();
            RenderAll();
        }

        private void CreateActiveTetraminoView()
        {
            if (_prefabTetraminoView == null) return;

            _instantiatedTetraminoView = Instantiate(_prefabTetraminoView, _activePieceContainer);
            _instantiatedTetraminoView.SetVisible(false);
        }

        private void SubscribeToEvents()
        {
            _engine.OnGameOver += OnGameOver;
            _engine.OnSpawnedTetramino += OnSpawnedTetramino;

            _gameArea.OnChangeGrid += OnChangeGrid;
        }
        private void UnsubscribeFromEvents()
        {
            if (_engine == null) return;

            if (_engine != null)
            {
                _engine.OnGameOver -= OnGameOver;
                _engine.OnSpawnedTetramino -= OnSpawnedTetramino;
            }

            if (_gameArea != null)
            {
                _gameArea.OnChangeGrid -= OnChangeGrid;
            }
        }

        private void OnChangeGrid(GameArea.ChangeBlockInfo info)
        {
            RenderBlock(info.X, info.Y);
        }

        private void OnSpawnedTetramino(Shape tetramino)
        {
            RenderActivePiece(tetramino);
        }

        private void OnGameOver(string reason)
        {
            HideAll();
        }

        public void RenderAll()
        {
            RenderGrid();
        }

        [NaughtyAttributes.Button]
        public void RenderGrid()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    RenderBlock(x,y);
                }
            }
        }

        private void RenderBlock(int x, int y)
        {
            var blockData = _gameArea.GetBlock(x, y);
            var BlockView = _gridViews[x, y];

            if (blockData == null)
            {
                if (BlockView != null)
                    ClearView(x, y);
            }
            else
            {
                if (BlockView != null && BlockView.Data != blockData)
                {
                    ClearView(x, y);
                }

                RenderView(x, y);
            }
        }

        private void RenderView(int x, int y)
        {
            var blockData = _gameArea.GetBlock(x, y);
            var newView = _blockPool.GetObject(blockData.Id);
            newView.Init(blockData);
            newView.SetPosition(new Vector3(x, y, 0f) + _offsetPosition);
            newView.SetVisible(true);
            _gridViews[x, y] = newView;
        }

        private void ClearView(int x, int y)
        {
            var currentView = _gridViews[x, y];
            if (currentView != null)
            {
                _blockPool.ReleaseObject(currentView);
                _gridViews[x, y] = null;
            }
        }

        public void RenderActivePiece(Shape tetramino)
        {
            if (_activeTetramino != null)
            {
                _activeTetramino.OnChangeOrignPosition -= OnChangeOrignPosition;
                
                foreach (var blockView in _cacheBlockViews)
                {
                    if (blockView != null)
                        _blockPool.ReleaseObject(blockView);
                }
                _cacheBlockViews.Clear();
            }

            _activeTetramino = tetramino;
            _activeTetramino.OnChangeOrignPosition += OnChangeOrignPosition;

            var blockViews = new BlockView[_activeTetramino.BlockCount];

            for (int i = 0; i < _activeTetramino.BlockCount; i++)
            {
                blockViews[i] = _blockPool.GetObject(_activeTetramino.Blocks[i].Id);
                _cacheBlockViews.Add(blockViews[i]);
            }

            _instantiatedTetraminoView.Init(tetramino, blockViews);
            _instantiatedTetraminoView.SetVisible(true);
        }

        private void OnChangeOrignPosition(Position pos)
        {
            _instantiatedTetraminoView.UpdatePositions();
        }

        public void HideAll()
        {
            _instantiatedTetraminoView?.SetVisible(false);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _gridViews[x, y]?.SetVisible(false);
                }
            }
        }

        public void Clear()
        {
            UnsubscribeFromEvents();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_gridViews[x, y] != null)
                    {
                        _blockPool.ReleaseObject(_gridViews[x, y]);
                        _gridViews[x, y] = null;
                    }
                }
            }

            foreach (var wall in _walls)
            {
                _blockPool.ReleaseObject(wall);
            }
            _walls.Clear();

            if (_instantiatedTetraminoView != null)
            {
                Destroy(_instantiatedTetraminoView.gameObject);
                _instantiatedTetraminoView = null;
            }
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}