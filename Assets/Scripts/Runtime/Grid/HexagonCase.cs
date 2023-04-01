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

    public float MoveTo(Hexagon _hex)
    {
        hexagon = _hex;
        Vector2 _origin = rectTransform.anchoredPosition;
        Vector2 _target = layout.HexagonToPixel(_hex);
        float _distance = Vector2.Distance(_origin, _target);

        if (_distance < 0.03f) return 0f;//No Anim

        float _animationTime = _distance / animSettings.fallingSpeed;//V = d / t => T = D / V

        NTweening.NTBuild((_t) =>
        {
            rectTransform.anchoredPosition = Vector2.Lerp(_origin, _target, _t);
        }, _animationTime).StartTween();

        return _animationTime;
    }

    public float Respawn(float _originY, int _number)
    {
        gameObject.SetActive(true);

        number = _number;
        numberText.text = number.ToString();

        image.color = Color.white;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, _originY);

        return MoveTo(hexagon);
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