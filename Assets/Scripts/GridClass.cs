using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClass : MonoBehaviour
{
    [SerializeField] private GameObject cell_prefab;
    [SerializeField] private int height;
    [SerializeField] private int wight;
    [SerializeField] private int offset = 1;

    [ContextMenu("Generate Grid")]
    private void GenerateGrid()
    {
        AlgPrim alg = GetComponent<AlgPrim>();
        Transform trn = transform;
        for (int x = 0; x < wight; x++)
        {
            List<GameObject> tmp = new List<GameObject>();
            for (int y = 0; y < height; y++)
            {
                var cell = Instantiate(cell_prefab, trn);
                Vector2 cell_scale = cell.transform.lossyScale;
                cell.transform.position = new Vector2(x * cell_scale.x + trn.position.x + offset, y * cell_scale.y + trn.position.y + offset);
                cell.transform.name = cell.transform.name.Replace("Square(Clone)", $"x:{x};y:{y}");
                cell.tag = "Wall";
                cell.GetComponent<Cell>().setXY(x, y);
                tmp.Add(cell);
            }
            alg.AddList(tmp);

        }
    }
    [ContextMenu("Delete Grid")]
    public void GridDelete()
    {
        Transform trn = transform;
        for (int j = trn.childCount; j > 0; --j)
            DestroyImmediate(trn.GetChild(0).gameObject);
    }
}
