using KG.Viewports;
using UnityEngine;
using Zenject;

public class GameOverPopup : Window, ISingleWindow
{
    [SerializeField] private ButtonBehaviour _restartBtn;

    private LevelHandler _levelHandler;

    [Inject]
    public void Construct(LevelHandler levelHandler)
    {
        _levelHandler = levelHandler;
    }


    public override void Init()
    {
        _restartBtn.Init(OnClickRestart);
    }

    private void OnClickRestart()
    {
        _levelHandler.Restart();
    }


}
