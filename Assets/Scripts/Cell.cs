using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<GameObject> neighbors = new List<GameObject>();
    public int x;
    public int y;
}
