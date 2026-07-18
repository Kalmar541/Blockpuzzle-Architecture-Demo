using System;

public class ScoreHandler : IDisposable
{
    public event Action<int> OnSetScore;

    public int CurrentScore => _scoreData.Score;

    private ScoreData _scoreData;
    private ProgressLinesHandler _progressLinesHandler;
    private ScoreConfig _scoreConfig;

    public ScoreHandler(ScoreData scoreData, ProgressLinesHandler progressLinesHandler,
        ScoreConfig scoreConfig)
    {
        _scoreData = scoreData;
        _progressLinesHandler = progressLinesHandler;
        _scoreConfig = scoreConfig;

        _progressLinesHandler.OnAddCountLines += OnAddCountLinesEvent;
    }

    public void Dispose()
    {
        _progressLinesHandler.OnAddCountLines -= OnAddCountLinesEvent;
    }

    private void OnAddCountLinesEvent(int countLines)
    {
        if (countLines <= 0) return;

        int scoreReward;

        if (countLines <= _scoreConfig.ScoreList.Count)
            scoreReward = _scoreConfig.ScoreList[countLines - 1];
        else
            scoreReward = _scoreConfig.ScoreList[_scoreConfig.ScoreList.Count - 1];

        _scoreData.Score += scoreReward;
        OnSetScore?.Invoke(_scoreData.Score);
    }
}
