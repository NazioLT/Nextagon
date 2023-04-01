using Nazio_LT.Tools.Core;

public delegate void SimpleDelegate<T>(T _arg);

public class ScoreManager : Singleton<ScoreManager>
{
    public event SimpleDelegate<int> onScoreUpdated = (_s) => {};

    private int totalMaxNumber;
    private int totalMaxScore;

    private int maxNumber = 3;
    private int score = 0;

    protected override void Awake()
    {
        base.Awake();

        onScoreUpdated += UpdateMaxScore;
    }

    public bool AddScore(int _maxNumber)//3
    {
        int _score = 0;
        for (var i = 1; i <= _maxNumber; i++)//1+2+3
        {
            _score += i;
        }

        _score *= _maxNumber;

        score += _score;
        onScoreUpdated(score);

        bool _newMaxNumber = _maxNumber > maxNumber;
        if(_newMaxNumber) maxNumber = _maxNumber;

        return _newMaxNumber;
    }

    private void UpdateMaxScore(int _newScore) => totalMaxScore = _newScore > totalMaxScore ? _newScore : totalMaxScore;
}