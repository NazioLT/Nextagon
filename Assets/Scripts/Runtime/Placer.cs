using UnityEngine;

public class Placer : MonoBehaviour
{
    [SerializeField] private Vector2 origin, size;
    [SerializeField, Range(1, 8)] private int layers = 1;
    [SerializeField] private RectTransform hexagonPrefab;

    private void Awake()
    {
        Layout _layout = new Layout(Orientation.LayoutFlat, size, origin);

        for (int q = -layers; q <= layers; q++)
        {
            int r1 = Mathf.Max(-layers, -q - layers);
            int r2 = Mathf.Min(layers, -q + layers);
            for (int r = r1; r <= r2; r++)
            {
                Hexagon _hex = new Hexagon(q, r);
                RectTransform _newHexTransform = Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity, transform);

                Vector2 _pos = _layout.HexagonToPixel(_hex);

                _newHexTransform.anchoredPosition = _pos;
            }
        }
    }

    private void SpawnHexagon()
    {

    }
}