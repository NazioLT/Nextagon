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

        rectTransform.anchoredPosition = _layout.HexagonToPixel(_hex);

        button.onClick.AddListener(OnClick);
    }

    public void NeighbourHighlight()
    {
        image.color = Color.yellow;
    }

    public void ResetHighlight()
    {
        image.color = Color.white;
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