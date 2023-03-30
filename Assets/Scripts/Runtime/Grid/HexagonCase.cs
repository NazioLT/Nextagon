using UnityEngine;
using Nazio_LT.Tools.UI;

public class HexagonCase : MonoBehaviour
{
    private Hexagon hexagon;

    [SerializeField] private NButton button;
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

    private void OnClick()
    {
        print("Click on : " + hexagon);
    }
}