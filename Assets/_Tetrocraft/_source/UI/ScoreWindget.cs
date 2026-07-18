using Lean.Localization;
using TMPro;
using UnityEngine;

public class ScoreWindget : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private LeanToken _tokenValue;

    private ScoreHandler _scoreHandler;

    public void Init(ScoreHandler scoreHandler)
    {
        _scoreHandler = scoreHandler;

        _scoreHandler.OnSetScore += OnSetScoreEvent;

        OnSetScoreEvent(_scoreHandler.CurrentScore);
    }

    private void OnDestroy()
    {
        if (_scoreHandler != null)
        {
            _scoreHandler.OnSetScore -= OnSetScoreEvent;
        }
    }

    private void OnSetScoreEvent(int scrore)
    {
        _tokenValue.Value = scrore.ToString();
    }
}
