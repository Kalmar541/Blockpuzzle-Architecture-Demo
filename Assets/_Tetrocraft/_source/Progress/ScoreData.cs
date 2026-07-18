public class ScoreData
{
    public int Score = 0;
    public int RecordScore = 0;

    public ScoreData(int startScore = 0, int recordScore = 0)
    {
        if (startScore >= 0)
            Score = startScore;

        if (recordScore >= 0)
            RecordScore = recordScore;
    }
}