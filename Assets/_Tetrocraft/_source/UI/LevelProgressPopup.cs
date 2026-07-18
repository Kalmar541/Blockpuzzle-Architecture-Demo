using KG.Viewports;
using UnityEngine;
using Zenject;

public class LevelProgressPopup : Window, IPermanentWindow
{
    [SerializeField] private LineProgressWiget _lineProgressWiget;
    [SerializeField] private DifficalWidget _difficalWidget;
    [SerializeField] private ScoreWindget _scoreWindget;

    private ProgressLinesHandler _progressLinesHandler;
    private ScoreHandler _scoreHandler;

    [Inject]
    public void Construct(ProgressLinesHandler progressLinesHandler, ScoreHandler scoreHandler)
    {
        _progressLinesHandler = progressLinesHandler;
        _scoreHandler = scoreHandler;
    }

    public override void Init()
    {
        _lineProgressWiget.Init(_progressLinesHandler);
        _difficalWidget.Init(_progressLinesHandler);
        _scoreWindget.Init(_scoreHandler);
    }
}
