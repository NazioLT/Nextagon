using System.Collections.Generic;
using UnityEngine;

public class HexagonalPlacer : MonoBehaviour
{
    [SerializeField] private Vector2 origin, size;
    [SerializeField] private int maxLayers = 2;

    private void PlaceInGrid(List<RectTransform> _rects)
    {
        Layout _layout = new Layout(Orientation.LayoutFlat, size, origin);
        _rects.Remove(transform as RectTransform);
        print(_rects.Count);

        _layout.ForEachHexagon(maxLayers, (i, _hex) =>
        {
            _rects[i].anchoredPosition = _layout.HexagonToPixel(_hex);
        }, _rects.Count);
    }

    private void OnValidate()
    {
        PlaceInGrid(new List<RectTransform>(GetComponentsInChildren<RectTransform>()));
    }
}
