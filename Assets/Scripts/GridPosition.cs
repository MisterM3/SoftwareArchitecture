using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct GridPosition
{
    public int x;
    public int y;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return ("(X: " + x + ", Y: " + y + ")");
    }

    //public static GridPosition operator *(int )
}
