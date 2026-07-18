using GameProject.Game;
using UnityEngine;
using Zenject;

namespace GameProject
{
    public class LevelController : MonoBehaviour
    {
        private ShapeEngine _engine;
        private ShapeDefinitionDatabase _pieceDatabase;
        private BlockGenerator _blockGenerator;
        private LevelHandler _levelHandler;
        private GameArea _gameArea;

        private Vector3 _offsetPosition;
        private Vector3 _spawnPosition;
        private const int _heigthDeadZone = 4;

        [Inject]
        public void Construct(ShapeEngine engine, ShapeDefinitionDatabase pieceDatabase,
            BlockGenerator blockGenerator, LevelHandler levelHandler, GameArea gameArea)
        {
            _engine = engine;
            _pieceDatabase = pieceDatabase;
            _blockGenerator = blockGenerator;
            _levelHandler = levelHandler;
            _gameArea = gameArea;
        }

        public void Init(Transform pointBoard)
        {
            _offsetPosition = pointBoard.position;
            _spawnPosition = _offsetPosition + new Vector3(_gameArea.Width/2, _gameArea.Height- _heigthDeadZone, 0);

            _engine.OnPieceLocked += OnPieceLocked;
            _levelHandler.OnStartLevel += SpawnNewPiece;
        }

        private void Update()
        {
            _engine.UpdateGravity(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _engine.OnPieceLocked -= OnPieceLocked;
            _levelHandler.OnStartLevel -= SpawnNewPiece;
        }

        private void SpawnNewPiece()
        {
            var definition = _pieceDatabase.GetRandomDefinition();
            var blocks = _blockGenerator.GenerateBlocks(definition, PresetBlockType.STONE); //TODO from level
            var tetramino = new Shape(
                definition,
                new Position((int)_spawnPosition.x, (int)_spawnPosition.y),
                blocks
            );

            _engine.SpawnNewShape(tetramino);
        }

        // ========== EVENT HANDLERS ==========

        private void OnPieceLocked(Shape tetramino)
        {
            SpawnNewPiece();
        }

        // ========== PUBLIC ==========

        public void Restart()
        {
            _engine.Reset();
            SpawnNewPiece();
        }
    }
}