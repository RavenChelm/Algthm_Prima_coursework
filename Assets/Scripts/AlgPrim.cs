using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgPrim : MonoBehaviour
{
    public List<List<GameObject>> Cells = new List<List<GameObject>>();
    private string Clear = "Clear";
    private string Wall = "Wall";

    [ContextMenu("Generate Maze")]
    private void GenerateMaze()
    {
        //Подготовка
        int width = Cells.Count;
        int height = Cells[0].Count;
        //Отчищение рандомной нечётной ячейки
        int x = Random.Range(0, width / 2) * 2 + 1;
        int y = Random.Range(0, height / 2) * 2 + 1;
        Cells[x][y].tag = Clear;

        //Получение стартовых ячеек для проверки
        List<GameObject> to_check = new List<GameObject>();
        if (y - 2 >= 0)
        {
            to_check.Add(Cells[x][y - 2]);
        }
        if (y + 2 < height)
        {
            to_check.Add(Cells[x][y + 2]);
        }
        if (x - 2 >= 0)
        {
            to_check.Add(Cells[x - 2][y]);
        }
        if (x + 2 < width)
        {
            to_check.Add(Cells[x + 2][y]);
        }

        while (to_check.Count > 0)
        {
            int index = Random.Range(0, to_check.Count - 1);
            (x, y) = to_check[index].GetComponent<Cell>().getXY();
            Cells[x][y].tag = Clear;
            to_check.Remove(item: Cells[x][y]);


            //conect
            List<string> dir = new List<string> { "North", "South", "East", "West" };
            while (dir.Count > 0)
            {
                int dir_index = Random.Range(0, dir.Count - 1);
                string tmp = dir[dir_index];
                switch (dir[dir_index])
                {
                    case "North":
                        if (y - 2 >= 0 && Cells[x][y - 2].tag == Clear)
                        {
                            Cells[x][index: y - 1].tag = Clear;
                            dir.Clear();

                        }
                        break;
                    case "South":
                        if (y + 2 < height && Cells[x][y + 2].tag == Clear)
                        {
                            Cells[x][index: y + 1].tag = Clear;
                            dir.Clear();
                        }
                        break;
                    case "East":
                        if (x - 2 >= 0 && Cells[x - 2][index: y].tag == Clear)
                        {
                            Cells[x - 1][index: y].tag = Clear;
                            dir.Clear();
                        }
                        break;
                    case "West":
                        if (x + 2 < width && Cells[x + 2][y].tag == Clear)
                        {
                            Cells[x + 1][y].tag = Clear;
                            dir.Clear();
                        }
                        break;
                    default:
                        break;
                }

                if (dir.Count != 0)
                    dir.RemoveAt(dir_index);
            }

            //добавить точки для посещения
            if (y - 2 >= 0 && Cells[x][y - 2].tag == Wall && !to_check.Contains(Cells[x][y - 2]))
            {
                to_check.Add(Cells[x][y - 2]);
            }
            if (y + 2 < height && Cells[x][y + 2].tag == Wall && !to_check.Contains(Cells[x][y + 2]))
            {
                to_check.Add(Cells[x][y + 2]);
            }
            if (x - 2 >= 0 && Cells[x - 2][y].tag == Wall && !to_check.Contains(Cells[x - 2][y]))
            {
                to_check.Add(Cells[x - 2][y]);
            }
            if (x + 2 < width && Cells[x + 2][y].tag == Wall && !to_check.Contains(Cells[x + 2][y]))
            {
                to_check.Add(Cells[x + 2][y]);
            }
        }
        //Отрисовка
        foreach (var list in Cells)
        {
            foreach (var cell in list)
            {
                if (cell.tag == Clear)
                    cell.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 255);
            }
        }
    }




    [ContextMenu("ClearList")]
    private void ClearList()
    {
        Cells.Clear();

    }

    public void AddList(List<GameObject> tmp)
    {
        Cells.Add(tmp);
        // Debug.Log(Cells.Count);
        // Debug.Log(Cells[0].Count);
        // Debug.Log(Cells[0][1]);

    }
}
