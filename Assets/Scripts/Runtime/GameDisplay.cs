using UnityEngine;
using TMPro;

public class GameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private HexagonGrid grid;
    private ScoreManager score;

    private void Start()
    {
        score = ScoreManager.instance;
        grid.onUpdateDisplay += UpdateDisplay;
        score.onScoreUpdated += UpdateScore;
        UpdateDisplay();
        UpdateScore(0);
    }

    private void UpdateDisplay()
    {
        
    }

    private void UpdateScore(int _score)
    {
        scoreText.text = _score.ToString();
    }
}