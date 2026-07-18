using System;

public class ProgressLinesHandler: IDisposable
{
    public event Action<int> OnChangeTotalLines;
    public event Action<int> OnAddCountLines;
    public event Action<int> OnSetLevel;

    public int Level => _progresData.Level;
    public int TotalLines => _progresData.TotalLines;

    private ProgressLinesData _progresData;

    private LevelUpConfig _levelUpConfig;
    private GameArea _gameArea;

    public ProgressLinesHandler(ProgressLinesData progressLinesData, LevelUpConfig levelUpConfig,
        GameArea gameArea)
    {
        _progresData = progressLinesData;
        _gameArea = gameArea;

        _levelUpConfig = levelUpConfig;

        _gameArea.OnLinesDelete += AddLines;
    }

    public void Dispose()
    {
        if(_gameArea!= null)
        {
            _gameArea.OnLinesDelete -= AddLines;
        }
    }

    public void AddLines(int count)
    {
        _progresData.TotalLines += count;
        OnChangeTotalLines?.Invoke(_progresData.TotalLines);
        OnAddCountLines?.Invoke(count);

        CheckNumLines();
    }

    public void CheckNumLines()
    {
        if(_progresData.Level >= _levelUpConfig.LinesForUp.Count)
            return;

        if (_levelUpConfig.LinesForUp[_progresData.Level-1].LinesNeed < _progresData.TotalLines)
        {
            LevelUP();
        }      
    }

    private void LevelUP()
    {
        _progresData.Level++;
        OnSetLevel?.Invoke(_progresData.Level);
    }
}