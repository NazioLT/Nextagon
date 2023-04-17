using UnityEngine;

public struct Layout
{
    public Layout(Orientation _orientation, Vector2 _size, Vector2 _origin)
    {
        orientation = _orientation;
        size = _size;
        origin = _origin;
    }

    public readonly Orientation orientation;
    public readonly Vector2 size, origin;

    public Vector2 HexagonToPixel(Hexagon _hexagon) => HexagonToPixel(this, _hexagon);

    #region Statics Methods

    public static Vector2 HexagonToPixel(Layout _layout, Hexagon _hexagon)
    {
        Orientation _orientation = _layout.orientation;
        float _x = (_orientation.f0 * _hexagon.q + _orientation.f1 * _hexagon.r) * _layout.size.x;
        float _y = (_orientation.f2 * _hexagon.q + _orientation.f3 * _hexagon.r) * _layout.size.y;

        return new Vector2(_x, _y) + _layout.origin;
    }

    public static void ForEachHexagonIn(Layout _layout, int _layerNumber, System.Action<int, Hexagon> _callback, int _maxIteration = int.MaxValue)
    {
        int i = 0;
        for (int q = -_layerNumber; q <= _layerNumber; q++)
        {
            int _r1 = Mathf.Max(-_layerNumber, -q - _layerNumber);
            int _r2 = Mathf.Min(_layerNumber, -q + _layerNumber);
            for (int r = _r1; r <= _r2; r++)
            {
                if (i > _maxIteration) break;

                Hexagon _hex = new Hexagon(q, r);
                _callback(i, _hex);

                i++;
            }
        }
    }

    public void ForEachHexagon(int _layerNumber, System.Action<int, Hexagon> _callback, int _maxIteration = int.MaxValue) => ForEachHexagonIn(this, _layerNumber, _callback, _maxIteration);

    #endregion
}