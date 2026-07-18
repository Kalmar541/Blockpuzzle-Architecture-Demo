using UnityEngine;

public class DifficalHandler 
{
    private ProgressLinesHandler _progressLinesHandler;

    public DifficalHandler(ProgressLinesHandler progressLinesHandler)
    {
        _progressLinesHandler = progressLinesHandler;      
    }

    public float GetSpeedBlock()
    {
        return Mathf.Pow((0.94f - ((_progressLinesHandler.Level - 1) * 0.007f)), (_progressLinesHandler.Level - 1));
    }
}