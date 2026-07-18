using KG.Viewports;
using System;

public class UILevelHandler : IDisposable
{
    private IViewportHandler _viewportHandler;
    private LevelHandler _levelStateManager;


    public UILevelHandler(IViewportHandler viewportHandler, LevelHandler levelStateManager)
    {
        _viewportHandler = viewportHandler;
        _levelStateManager = levelStateManager;


        _levelStateManager.OnResetLevel += ShowPreparePopup;
        _levelStateManager.OnLoseLevel += ShowGameOverPopup;
    }

    public void Dispose()
    {
        if(_levelStateManager != null)
        {
            _levelStateManager.OnResetLevel -= ShowPreparePopup;
            _levelStateManager.OnResetLevel -= ShowPreparePopup;
        }
    }

    private void ShowPreparePopup()
    {
        _viewportHandler.Show<LevelStarterPopup>();
    }

    private void ShowGameOverPopup()
    {
        _viewportHandler.Show<GameOverPopup>();
    }
}