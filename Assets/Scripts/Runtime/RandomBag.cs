using System.Collections.Generic;
using UnityEngine;

public class RandomBag
{
    public RandomBag(int _minInclusiveValue, int _maxExclusiveValue, int _count)
    {
        content = new();

        minInclusiveValue = _minInclusiveValue;
        maxExclusiveValue = _maxExclusiveValue;

        //Equicount
        int _itemNum = _maxExclusiveValue - _minInclusiveValue;
        int _equiCount = _count / _itemNum;

        AddEquiCount(_equiCount);
    }

    private List<int> content;

    private int minInclusiveValue, maxExclusiveValue;

    public void AddEquiCount(int _equiCount)
    {
        for (int i = 0; i < _equiCount; i++)
        {
            for (int n = minInclusiveValue; n < maxExclusiveValue; n++)
            {
                content.Add(n);
            }
        }
    }

    public void Add(int _item) => content.Add(_item);

    public int PickRandom()
    {
        if (content.Count == 0) return Random.Range(minInclusiveValue, maxExclusiveValue);

        int _randomIndex = Random.Range(0, content.Count);
        int _result = content[_randomIndex];
        content.RemoveAt(_randomIndex);

        return _result;
    }
}