using GameProject;
using GameProject.Game;
using UnityEngine;
using Zenject;

public class EntrypointLevel : MonoBehaviour
{
    [SerializeField] private BoardRenderer _boardRenderer;
    [SerializeField] private LevelController _levelController;

    [SerializeField] private Transform _pointBoard;

    private LevelHandler _levelStateManager;

    [Inject]
    public void Construct(LevelHandler levelStateManager)
    {
        _levelStateManager = levelStateManager;
    }

    private void Start()
    {
        _levelController.Init(_pointBoard);
        _boardRenderer.Init(_pointBoard);

        _levelStateManager.Restart();
    }
}
