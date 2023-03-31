using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Core;

public delegate void SimpleDelegate();

public class HexagonGrid : MonoBehaviour
{
    public event SimpleDelegate onUpdateDisplay;

    [SerializeField] private Vector2 origin, size;
    [SerializeField, Range(1, 8)] private int layers = 1;
    [SerializeField] private HexagonCase hexagonPrefab;

    public HexagonCase selectedCase { private set; get; } = null;
    public List<Hexagon> selectableCases { private set; get; } = new();
    public List<Hexagon> pathCases { private set; get; } = new();

    private Dictionary<Hexagon, HexagonCase> cases = new();
    private List<Hexagon> gridCoords = new();

    private void Awake()
    {
        CreateGrid();

        SelectAllOne();
    }

    private void CreateGrid()
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

                _case.Init(this, _hex, _layout, Random.Range(1, 4));
                cases.Add(_hex, _case);
                gridCoords.Add(_hex);
            }
        }
    }

    private void SelectAllOne()
    {
        pathCases = new();
        selectedCase = null;
        selectableCases = new();
        foreach (var _case in cases.Values)
        {
            if (_case.Number == 1) selectableCases.Add(_case.Hexagon);
        }

        onUpdateDisplay();
    }

    public void Undo()
    {
        if (pathCases.Count == 0)
        {
            SelectAllOne();
            return;
        }

        Hexagon _lastPathCaseID = pathCases.Last();
        selectedCase = cases[_lastPathCaseID];
        pathCases.Remove(_lastPathCaseID);

        MakeNeighboursSelectables();
    }

    private void MakeCaseFalling()
    {
        foreach (var _coord in gridCoords)
        {
            if(cases[_coord].gameObject.activeSelf == false) continue;

            Hexagon _botCoords = _coord.Neighbours[2];

            bool _botIsOutOfGrid = !cases.ContainsKey(_botCoords);

            if(_botIsOutOfGrid) continue;

            bool _botIsHere = cases[_botCoords].gameObject.activeSelf;

            // print(cases[_botHex].name);
            if(!_botIsHere)
            {
                HexagonCase _currentCase = cases[_coord];
                cases[_coord] = cases[_botCoords];
                cases[_botCoords] = _currentCase;

                _currentCase.MoveTo(_botCoords);
            }
        }
    }

    public void SelectSlot(HexagonCase _case)
    {
        if (!selectableCases.Contains(_case.Hexagon) && _case != selectedCase) return;

        if (_case == selectedCase && pathCases.Count > 0)//Confirm
        {
            foreach (Hexagon _hex in pathCases)
            {
                cases[_hex].gameObject.SetActive(false);
            }

            selectedCase.NextLevel();

            MakeCaseFalling();
            // SelectAllOne();

            // onUpdateDisplay();
            return;
        }

        if (selectedCase) pathCases.Add(selectedCase.Hexagon);
        selectedCase = _case;
        Hexagon[] _neighbours = selectedCase.Hexagon.Neighbours;

        MakeNeighboursSelectables();
    }

    private void MakeNeighboursSelectables(bool _updateDisplay = true)
    {
        Hexagon[] _neighbours = selectedCase.Hexagon.Neighbours;
        selectableCases = new();

        foreach (Hexagon _neighbour in _neighbours)
        {
            if (cases.ContainsKey(_neighbour) && cases[_neighbour].Number == selectedCase.Number + 1)
            {
                selectableCases.Add(_neighbour);
            }
        }

        if (_updateDisplay) onUpdateDisplay();
    }
}