using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameModes/Chill")]
public class ChillGameMode : GameMode
{
    public override bool MakeNeighbourSelectableCondition(Dictionary<Hexagon, HexagonCase> _cases, Hexagon _key, HexagonCase _selectedCase)
    {
        if(!_cases.ContainsKey(_key)) return false;

        int _selectedNumber = _selectedCase.Number;
        HexagonCase _nextCase = _cases[_key];

        return _nextCase.Number == _selectedNumber + 1 || (_selectedNumber == 1 && _nextCase.Number == 1);
    }

    public override bool CanReClickOnOnes => true;
}