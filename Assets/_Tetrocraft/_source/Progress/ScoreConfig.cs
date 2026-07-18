using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
public class ScoreConfig : ScriptableObject
{
    [SerializeField] private List<int> _scoreList;

    public IReadOnlyList<int> ScoreList => _scoreList;
}
