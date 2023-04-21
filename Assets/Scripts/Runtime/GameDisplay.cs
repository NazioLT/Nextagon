using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button powerChooseButton;
    [SerializeField] private Image scoreBar;
    [SerializeField] private HexagonGrid grid;
    private ScoreManager score;

    private int barScore = 0;

    private const int FIRST_STEP = 100;
    private const int SECOND_STEP = 150;
    private const int MAX_STEP = 200;

    private void Start()
    {
        score = ScoreManager.instance;
        grid.onUpdateDisplay += UpdateDisplay;
        score.onScoreUpdated += UpdateScore;
        powerChooseButton.onClick.AddListener(TryUpgradePower);
        UpdateDisplay();
        UpdateScore(0, 0);
    }

    private void UpdateDisplay()
    {

    }

    private void UpdateScore(int _score, int _scoreAdded)
    {
        barScore += _scoreAdded;
        scoreText.text = _score.ToString();

        scoreBar.fillAmount = (float)barScore / 200f;
    }

    private void TryUpgradePower()
    {
        if (barScore < FIRST_STEP)
        {
            return;
        }

        Power _power = null;

        if (barScore < SECOND_STEP)
        {
            _power = grid.PowerFactory(GameManager.Powers[0]);
        }
        else if (barScore < MAX_STEP)
        {
            _power = grid.PowerFactory(GameManager.Powers[1]);
        }
        else
        {
            _power = grid.PowerFactory(GameManager.Powers[2]);
        }

        _power.GainCount();
        _power.UpdateDisplay();
        ResetBar();
        return;
    }

    private void ResetBar()
    {
        barScore = 0;
        scoreBar.fillAmount = (float)barScore / 200f;
    }
}