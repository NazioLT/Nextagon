using System;
using System.Collections.Generic;
using UnityEngine;

public struct Hexagon : IEquatable<Hexagon>
{
    public Hexagon(int _q, int _r, int _s)
    {
        q = _q;
        r = _r;
        s = _s;
    }

    public Hexagon(int _q, int _r)
    {
        q = _q;
        r = _r;
        s = -q - r;
    }

    public static readonly Hexagon[] Directions = new Hexagon[]{
        new Hexagon(1, 0, -1),
        new Hexagon(1, -1, 0),
        new Hexagon(0, -1, 1),
        new Hexagon(-1, 0, 1),
        new Hexagon(-1, 1, 0),
        new Hexagon(0, 1, -1)
    };

    public readonly int q;
    public readonly int r;
    public readonly int s;


    #region Static Operators

    public static bool operator ==(Hexagon _a, Hexagon _b) => _a.q == _b.q && _a.r == _b.r && _a.s == _b.s;
    public static bool operator !=(Hexagon _a, Hexagon _b) => !(_a == _b);
    public static Hexagon operator +(Hexagon _a, Hexagon _b) => new Hexagon(_a.q + _b.q, _a.r + _b.r, _a.s + _b.s);
    public static Hexagon operator -(Hexagon _a, Hexagon _b) => new Hexagon(_a.q - _b.q, _a.r - _b.r, _a.s - _b.s);
    public static Hexagon operator *(Hexagon _a, int _k) => new Hexagon(_a.q * _k, _a.r * _k, _a.s * _k);

    #endregion

    #region Static Methods

    public static int GetLength(Hexagon _a) => (int)((Mathf.Abs(_a.q) + Mathf.Abs(_a.r) + Mathf.Abs(_a.s)) / 2);
    public static int Distance(Hexagon _a, Hexagon _b) => GetLength(_a - _b);
    public static Hexagon GetNeighbour(Hexagon _hex, int _direction) => _hex + Directions[_direction];
    public static Hexagon[] GetNeighbours(Hexagon _hex)
    {
        Hexagon[] _neighbours = new Hexagon[6];

        for (int i = 0; i < 6; i++)
        {
            _neighbours[i] = GetNeighbour(_hex, i);
        }

        return _neighbours;
    }
    public static bool IsNeighbours(Hexagon _a, Hexagon _b)
    {
        List<Hexagon> _aNeighbours = new(_a.Neighbours);
        return _aNeighbours.Contains(_b);
    }

    #endregion

    public int Length => GetLength(this);
    public Hexagon[] Neighbours => GetNeighbours(this);

    public bool IsNeighbours(Hexagon _other) => IsNeighbours(this, _other);

    public override int GetHashCode() => HashCode.Combine(q, r, s);

    bool IEquatable<Hexagon>.Equals(Hexagon other) => this == other;

    public override bool Equals(object obj)
    {
        return obj is Hexagon hexagon &&
               q == hexagon.q &&
               r == hexagon.r &&
               s == hexagon.s;
    }

    public override string ToString() => $"Hexagon({q}, {r}, {s})";
}