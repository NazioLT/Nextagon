using UnityEngine;
using UnityEngine.UI;
using Nazio_LT.Tools.UI;

public class HexagonCase : MonoBehaviour
{
    private Hexagon hexagon;

    [SerializeField] private NButton button;
    [SerializeField] private Image image;
    private RectTransform rectTransform;
    private HexagonGrid grid;

    public void Init(HexagonGrid _grid, Hexagon _hex, Layout _layout)
    {
        rectTransform = transform as RectTransform;
        grid = _grid;
        hexagon = _hex;

        grid.onUpdateDisplay += UpdateDisplay;

        rectTransform.anchoredPosition = _layout.HexagonToPixel(_hex);

        button.onClick.AddListener(OnClick);
    }

    private void UpdateDisplay()
    {
        image.color = grid.selectedCase == hexagon ? Color.blue : Color.white;
    }

    public void NeighbourHighlight()
    {
        image.color = Color.yellow;
    }

    public void ResetHighlight()
    {
        image.color = Color.white;
    }

    public void NonInteractable()
    {
        image.color = Color.grey;
    }

    public void SelectedHighlight()
    {
        image.color = Color.blue;
    }

    private void OnClick()
    {
        print("Click on : " + hexagon);

        grid.SelectSlot(hexagon);
    }
}