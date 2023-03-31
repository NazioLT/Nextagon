using UnityEngine;
using UnityEngine.UI;
using Nazio_LT.Tools.UI;
using Nazio_LT.Tools.NTween;
using TMPro;

public class HexagonCase : MonoBehaviour
{
    private Hexagon hexagon;
    private Layout layout;
    private int number;

    [SerializeField] private NButton button;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI numberText;
    private RectTransform rectTransform;
    private HexagonGrid grid;

    public void Init(HexagonGrid _grid, Hexagon _hex, Layout _layout, int _number)
    {
        rectTransform = transform as RectTransform;
        grid = _grid;
        hexagon = _hex;
        number = _number;
        layout= _layout;

        grid.onUpdateDisplay += UpdateDisplay;
        rectTransform.anchoredPosition = layout.HexagonToPixel(_hex);
        button.onClick.AddListener(OnClick);

        name = hexagon.ToString();

        UpdateDisplay();
    }

    public void NextLevel()
    {
        number++;
    }

    public void MoveTo(Hexagon _hex)
    {
        hexagon = _hex;
        Vector2 _origin = rectTransform.anchoredPosition;
        Vector2 _target = layout.HexagonToPixel(_hex);

        NTweening.NTBuild((_t) => 
        {
            rectTransform.anchoredPosition = Vector2.Lerp(_origin, _target, _t);
        }, 0.5f).StartTween();
    }

    public void HasBotCases(bool _value)
    {
        image.color = _value ? Color.green : Color.red;
    }

    public void SetColor(Color _color) => image.color = _color;

    private void UpdateDisplay()
    {
        numberText.text = number.ToString();

        Color _caseColor = Color.white;
        if (grid.selectedCase == this) _caseColor = Color.blue;
        else if (grid.pathCases.Contains(hexagon)) _caseColor = Color.cyan;
        else if (grid.selectableCases.Contains(hexagon)) _caseColor = Color.yellow;

        image.color = _caseColor;

        // numberText.text = hexagon.ToString().Substring(7);
        // image.color = Color.Lerp(Color.white, Color.red, Mathf.InverseLerp(-2f, 2f, hexagon.r));
    }

    private void OnClick()
    {
        print("Click on : " + hexagon);

        grid.SelectSlot(this);
    }

    private void OnDestroy()
    {
        grid.onUpdateDisplay -= UpdateDisplay;
    }

    public int Number => number;
    public Hexagon Hexagon => hexagon;
}