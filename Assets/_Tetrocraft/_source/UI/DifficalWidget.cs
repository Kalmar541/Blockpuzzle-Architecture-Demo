using Lean.Localization;
using TMPro;
using UnityEngine;

public class DifficalWidget : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelTxt;
    [SerializeField] private LeanToken _tokenValue;

    private ProgressLinesHandler _progressLinesHandler;

    public void Init(ProgressLinesHandler progressLinesHandler)
    {
        _progressLinesHandler = progressLinesHandler;

        _progressLinesHandler.OnSetLevel += OnSetLevelEvent;

        OnSetLevelEvent(_progressLinesHandler.Level);
    }

    private void OnDestroy()
    {
        if (_progressLinesHandler != null)
        {
            _progressLinesHandler.OnSetLevel -= OnSetLevelEvent;
        }
    }

    private void OnSetLevelEvent(int totalLines)
    {
        _tokenValue.Value = totalLines.ToString();
    }
}
