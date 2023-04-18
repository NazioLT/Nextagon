using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : ScriptableObject
{   
    [SerializeField] private Sprite gameModeIcon;

    public virtual bool MakeNeighbourSelectableCondition(Dictionary<Hexagon, HexagonCase> _cases, Hexagon _key, HexagonCase _selectedCase)
    {
        return _cases.ContainsKey(_key) && _cases[_key].Number == _selectedCase.Number + 1;
    }

    public Sprite Icon => gameModeIcon;
    public virtual bool CanReClickOnOnes => false;
}
