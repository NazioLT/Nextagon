using UnityEngine;
using TMPro;

public class GameDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI jumpCountText;
    [SerializeField] private HexagonGrid grid;

    private void Start()
    {
        grid.onUpdateDisplay += UpdateDisplay;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        jumpCountText.text = "Jump : " + grid.JumpRemaining;
    }
}