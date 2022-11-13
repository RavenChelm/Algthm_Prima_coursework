using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    // TODO: Заменить на лямбда функции
    public void setXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public (int, int) getXY()
    {
        return (x, y);
    }
}
