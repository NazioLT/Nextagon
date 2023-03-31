using System.Collections.Generic;
using UnityEngine;

public delegate void SimpleDelegate();

public class HexagonGrid : MonoBehaviour
{
    public event SimpleDelegate onUpdateDisplay;

    [SerializeField] private Vector2 origin, size;
    [SerializeField, Range(1, 8)] private int layers = 1;
    [SerializeField] private HexagonCase hexagonPrefab;

    public Hexagon selectedCase { private set; get; }
    public List<Hexagon> highlightedCases { private set; get; }

    private Dictionary<Hexagon, HexagonCase> cases = new();

    private void Awake()
    {
        Layout _layout = new Layout(Orientation.LayoutFlat, size, origin);

        for (int q = -layers; q <= layers; q++)
        {
            int _r1 = Mathf.Max(-layers, -q - layers);
            int _r2 = Mathf.Min(layers, -q + layers);
            for (int r = _r1; r <= _r2; r++)
            {
                Hexagon _hex = new Hexagon(q, r);
                HexagonCase _case = Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity, transform);

                _case.Init(this, _hex, _layout);
                cases.Add(_hex, _case);
            }
        }
    }

    private void SetAllGridNormal()
    {
        foreach (var _case in cases.Values)
        {
            _case.ResetHighlight();
        }
    }

    public void SelectSlot(Hexagon _hex)
    {
        // Hexagon[] _neighbours = _hex.Neighbours;

        // SetAllGridNormal();

        // cases[_hex].SelectedHighlight();

        // foreach (var _neighbour in _neighbours)
        // {
        //     if (!cases.ContainsKey(_neighbour)) continue;

        //     cases[_neighbour].NeighbourHighlight();
        // }

        selectedCase = _hex;
        highlightedCases = new(_hex.Neighbours);

        onUpdateDisplay();
    }
}