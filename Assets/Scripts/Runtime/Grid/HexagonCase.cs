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
    private GridAnimationSettings animSettings;

    public void Init(HexagonGrid _grid, Hexagon _hex, Layout _layout, int _number)
    {
        rectTransform = transform as RectTransform;
        grid = _grid;
        hexagon = _hex;
        number = _number;
        layout = _layout;
        animSettings = grid.AnimSettings;

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

    public int Kill()
    {
        gameObject.SetActive(false);

        return number;
    }

    public void MoveTo(Hexagon _hex)
    {
        hexagon = _hex;
        Vector2 _origin = rectTransform.anchoredPosition;
        Vector2 _target = layout.HexagonToPixel(_hex);
        float _distance = Vector2.Distance(_origin, _target);

        if (_distance < 0.03f) return;//No Anim

        animSettings.MoveToAnim(rectTransform, _origin, _target);
    }

    public void Respawn(float _originY, int _number)
    {
        gameObject.SetActive(true);

        number = _number;
        numberText.text = number.ToString();

        image.color = Color.white;

        //Sample spacing.
        float _yDelta = _originY - rectTransform.position.y;
        Vector2 _hexPixelPosition = layout.HexagonToPixel(hexagon);
        rectTransform.position = rectTransform.position + Vector3.up * (_originY + (_hexPixelPosition.y * animSettings.FallingSpacementFactor));

        MoveTo(hexagon);
    }

    private void UpdateDisplay()
    {
        numberText.text = number.ToString();

        Color _caseColor = Color.white;
        if (grid.selectedCase == this) _caseColor = Color.blue;
        else if (grid.pathCases.Contains(hexagon)) _caseColor = Color.cyan;
        else if (grid.selectableCases.Contains(hexagon)) _caseColor = Color.yellow;

        image.color = _caseColor;
    }

    private void OnClick()
    {
        grid.SelectSlot(this);
    }

    private void OnDestroy()
    {
        grid.onUpdateDisplay -= UpdateDisplay;
    }

    public int Number => number;
    public Hexagon Hexagon { get => hexagon; set => hexagon = value; }
}