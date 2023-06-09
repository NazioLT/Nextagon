using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Nazio_LT.Tools.Core;

public delegate void SimpleDelegate();

public enum Powers
{
    None,
    Jump,
    Clean,
    GrowUp
}

public class HexagonGrid : MonoBehaviour
{
    public event SimpleDelegate onUpdateDisplay;

    private bool canClick = true;

    [SerializeField] private Vector2 origin, size;
    [SerializeField, Range(1, 8)] private int layers = 1;
    [SerializeField] private HexagonCase hexagonPrefab;
    [SerializeField] private GridAnimationSettings animSettings;

    [Header("Powers")]
    [SerializeField] private Power jumpPower;
    [SerializeField] private Power cleanPower;
    [SerializeField] private Power growUpPower;
    [SerializeField] private Power nonePower;

    private Dictionary<Powers, Power> powers;

    private int jumpInUsing = 0;
    private bool growUp = false;

    public HexagonCase selectedCase { private set; get; } = null;
    public List<Hexagon> selectableCases { private set; get; } = new();
    public List<Hexagon> pathCases { private set; get; } = new();

    private Dictionary<Hexagon, HexagonCase> cases = new();
    private List<Hexagon> gridCoords = new();

    private RandomBag bag;
    private ScoreManager score;

    private const int MAXSTARTNUMBER = 3;

    private void Start()
    {
        // POWERS
        powers = new(){
            { Powers.Clean, cleanPower },
            { Powers.Jump, jumpPower },
            { Powers.GrowUp, growUpPower },
            { Powers.None, nonePower },
        };

        jumpPower.Init(this, Jump);
        cleanPower.Init(this, Clean);
        growUpPower.Init(this, GrowUp);

        foreach (Power power in powers.Values)
        {
            power.Show(false);
        }

        foreach (Powers power in GameManager.Powers)
        {
            powers[power].Show(true);
        }
        

        // GRID
        CreateGrid();

        ResetDisplay();

        score = ScoreManager.instance;
        bag.AddEquiCount(12);
    }

    #region Inputs

    public void SelectSlot(HexagonCase _case)
    {
        if (!canClick) return;

        if (!selectableCases.Contains(_case.Hexagon) && _case != selectedCase) return;

        if (growUp)
        {
            _case.NextLevel();
            growUp = false;
            growUpPower.RemoveCount();
            StartCoroutine(EndTurn());
            return;
        }

        if (GameManager.GameMode.CanCombineOnes && selectedCase != null && selectedCase.Number == 1 && _case.Number == 1)//Combine 2 1
        {
            pathCases.Add(selectedCase.Hexagon);
            selectedCase = _case;
            StartCoroutine(EndTurn());
            return;
        }

        if (GameManager.GameMode.CanCombineOnes == false && selectedCase != null && selectedCase.Number == 1 && _case.Number == 1) return;//Reclick sur le 1

        if (_case == selectedCase && pathCases.Count > 0)//Confirm
        {
            StartCoroutine(EndTurn());
            return;
        }

        if (selectedCase)
        {
            pathCases.Add(selectedCase.Hexagon);

            //Use un jump si la nouvelle case n'est pas voisine de l'actuelle.
            if (!_case.Hexagon.IsNeighbours(selectedCase.Hexagon)) jumpInUsing++;
        }

        selectedCase = _case;

        MakeNeighboursSelectables();
    }

    public void Undo()
    {
        if (growUp)
        {
            growUp = false;
            ResetDisplay();
            return;
        }

        if (pathCases.Count == 0)
        {
            ResetDisplay();
            return;
        }

        Hexagon _lastPathCaseID = pathCases.Last();
        HexagonCase _undoCase = cases[_lastPathCaseID];

        if (!_undoCase.Hexagon.IsNeighbours(selectedCase.Hexagon)) jumpInUsing--;

        selectedCase = _undoCase;
        pathCases.Remove(_lastPathCaseID);

        MakeNeighboursSelectables();
    }

    public void Jump()
    {
        if (!canClick || selectedCase == null || JumpRemaining <= 0) return;

        SelectAllNumbers(selectedCase.Number + 1);
    }

    public void Clean()
    {
        selectedCase = null;
        pathCases.Clear();
        selectableCases = new();

        cleanPower.RemoveCount();

        foreach (var _case in cases.Keys)
        {
            if (cases[_case].Number <= 3)
            {
                bag.Add(cases[_case].Kill());//Remet le chiffre dans le "sac"
            }
        }

        StartCoroutine(FallingAnim());
    }

    public void GrowUp()
    {
        selectableCases = null;
        pathCases.Clear();
        selectableCases = new();

        foreach (Hexagon _id in cases.Keys)
        {
            if (cases[_id].Number <= score.MaxNumber) selectableCases.Add(_id);
        }

        growUp = true;
        onUpdateDisplay();
    }

    #endregion

    public Power PowerFactory(Powers _type)
    {
        switch (_type)
        {
            case Powers.Jump:
                return jumpPower;

            case Powers.Clean:
                return cleanPower;

            case Powers.GrowUp:
                return growUpPower;
        }

        return null;
    }

    private void OnEndTurn()
    {
        if(selectedCase)
            score.AddScore(selectedCase.Number);
        jumpPower.RemoveCount(jumpInUsing);
    }

    private IEnumerator EndTurn()
    {
        canClick = false;

        OnEndTurn();

        foreach (Hexagon _hex in pathCases)
        {
            bag.Add(cases[_hex].Kill());//Remet le chiffre dans le "sac"
        }

        selectedCase?.NextLevel();

        yield return StartCoroutine(FallingAnim());
    }

    private IEnumerator FallingAnim()
    {
        MakeCaseFalling();

        yield return new WaitForSeconds(0.25f);

        ResetDisplay();

        onUpdateDisplay();

        canClick = true;
    }

    private void MakeCaseFalling()
    {
        List<HexagonCase> _destroyedCases = new();

        for (int q = -layers; q <= layers; q++)//Couche par couche en partant du bas
        {
            foreach (var _coord in gridCoords)//Check toutes les cases
            {
                if (_coord.q != q) continue;//qui sont a la bonne couche

                if (cases[_coord].gameObject.activeSelf == false)//Si vide
                {
                    _destroyedCases.Add(cases[_coord]);
                    continue;
                }

                Hexagon _currentCoord = _coord;
                Hexagon _botCoords = _currentCoord.Neighbours[2];

                HexagonCase _currentCase = cases[_coord];

                while (true)//Bot out of grid
                {
                    bool _botOutOfGrid = !cases.ContainsKey(_botCoords);

                    if (_botOutOfGrid) break;//Case en dessous

                    bool _botIsActive = cases[_botCoords].gameObject.activeSelf;

                    if (_botIsActive) break;

                    //Switch
                    HexagonCase _botCase = cases[_botCoords];

                    _currentCase.Hexagon = _botCoords;
                    _botCase.Hexagon = _currentCoord;

                    cases[_currentCoord] = _botCase;
                    cases[_botCoords] = _currentCase;

                    _currentCoord = _botCoords;
                    _botCoords = _currentCoord.Neighbours[2];
                }

                _currentCase.MoveTo(_currentCoord);
            }
        }

        foreach (var _case in _destroyedCases)
        {
            _case.Respawn(animSettings.SpawnHeight, bag.PickRandom());
        }
    }

    private void MakeNeighboursSelectables(bool _updateDisplay = true)
    {
        Hexagon[] _neighbours = selectedCase.Hexagon.Neighbours;
        selectableCases = new();

        foreach (Hexagon _neighbour in _neighbours)
        {
            if (GameManager.GameMode.MakeNeighbourSelectableCondition(cases, _neighbour, selectedCase))
            {
                selectableCases.Add(_neighbour);
            }
        }

        if (_updateDisplay) onUpdateDisplay();
    }

    private void CreateGrid()
    {
        Layout _layout = new Layout(Orientation.LayoutFlat, size, origin);

        _layout.ForEachHexagon(layers, (i, _hex) =>
        {
            HexagonCase _case = Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity, transform);

            cases.Add(_hex, _case);
            gridCoords.Add(_hex);
        });

        bag = new RandomBag(1, MAXSTARTNUMBER + 1, cases.Count);//+1 car exclusive

        foreach (var _case in cases)
        {
            _case.Value.Init(this, _case.Key, _layout, bag.PickRandom());
        }
    }

    private void ResetDisplay()
    {
        pathCases = new();
        selectedCase = null;
        SelectAllNumbers(1);

        jumpInUsing = 0;
    }

    private void SelectAllNumbers(int _number)
    {
        selectableCases = new();
        foreach (var _case in cases.Values)
        {
            if (_case.Number == _number) selectableCases.Add(_case.Hexagon);
        }

        onUpdateDisplay();
    }

    public GridAnimationSettings AnimSettings => animSettings;
    public int JumpRemaining => jumpPower.Count - jumpInUsing;
}