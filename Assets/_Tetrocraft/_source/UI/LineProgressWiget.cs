using Lean.Localization;
using TMPro;
using UnityEngine;

public class LineProgressWiget : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalLinesTxt;
    [SerializeField] private LeanToken _tokenValue;

    private ProgressLinesHandler _progressLinesHandler;

    public void Init(ProgressLinesHandler progressLinesHandler)
    {
        _progressLinesHandler = progressLinesHandler;

        _progressLinesHandler.OnChangeTotalLines += OnChangeTotalLinesEvent;

        OnChangeTotalLinesEvent(_progressLinesHandler.TotalLines);
    }

    private void OnDestroy()
    {
        if(_progressLinesHandler!= null)
        {

            _progressLinesHandler.OnChangeTotalLines -= OnChangeTotalLinesEvent;
        }
    }

    private void OnChangeTotalLinesEvent(int totalLines)
    {
        _tokenValue.Value = totalLines.ToString();
    }
}
