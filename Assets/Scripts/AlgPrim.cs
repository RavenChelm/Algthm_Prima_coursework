using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlgPrim : MonoBehaviour
{
    private List<GameObject> listCell = new List<GameObject>(); //Список всех ячеек
    private List<GameObject> ListWallCell = new List<GameObject>(); //Список стен для алгоритма
    private List<GameObject> ListMaze = new List<GameObject>(); //Отдельно лабиринт
    [SerializeField] public GameObject Cell; // префаб ячйки 

    void Start()
    {
        //______Подготовка_____

        int Number = 0; // счётчик
        int targetCells = 20; //количестве ячеек на стороне 
        int Index = 0; //Индекс полученный из имени
        Transform trnform = transform;
        //Генерация поля
        for (int i = 0; i < targetCells; i++)
        {
            for (int n = 0; n < targetCells; n++)
            {
                var cell = Instantiate(Cell, trnform);
                listCell.Add(cell);
                cell.transform.name = cell.transform.name.Replace("Image(Clone)", Number.ToString());
                cell.GetComponent<Cell>().x = n;
                cell.GetComponent<Cell>().y = i;
                cell.tag = "Empty";
                Number++;
            }
        }
        //Поиск соседей
        for (int i = 0; i < targetCells; i++)
        {
            for (int n = 0; n < targetCells; n++)
            {
                if (int.TryParse(listCell[Index].name, out Index))
                {
                    if (Index % targetCells == 0)
                    {
                        if (Index + 1 < targetCells * targetCells)
                            listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index + 1]);
                    }
                    else if ((Index + 1) % targetCells == 0)
                    {
                        if (Index - 1 >= 0)
                            listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index - 1]);

                    }
                    else
                    {
                        if (Index - 1 >= 0)
                            listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index - 1]);
                        if (Index + 1 < targetCells * targetCells)
                            listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index + 1]);
                    }
                    if (Index + targetCells < targetCells * targetCells)
                        listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index + targetCells]);
                    if (Index - targetCells >= 0)
                        listCell[Index].GetComponent<Cell>().neighbors.Add(listCell[Index - targetCells]);
                }
                Index++;
            }
        }

        //_____Начало алгоритма_____

        //Получение рандомной ячейки
        int RandomCellIndex = Random.Range(0, listCell.Count - 1);
        ListMaze.Add(listCell[RandomCellIndex]);
        ListMaze[0].tag = "Maze";
        foreach (var cl in listCell[RandomCellIndex].GetComponent<Cell>().neighbors)
        {
            if (int.TryParse(cl.name, out Index))
            {
                //North
                if (Index - targetCells * 2 >= 0)
                {
                    ListWallCell.Add(listCell[Index - targetCells * 2]);
                }
                //South
                if (Index + targetCells * 2 <= targetCells * targetCells)
                {
                    ListWallCell.Add(listCell[Index + targetCells * 2]);
                }
                //East
                if (Index - 2 >= 0)
                {
                    ListWallCell.Add(listCell[Index - 2]);
                }
                //West
                if (Index + 2 <= targetCells * targetCells)
                {
                    ListWallCell.Add(listCell[Index + 2]);
                }
            }
            else
                continue;
        }

        while (ListWallCell.Count != 0)
        {

            RandomCellIndex = Random.Range(0, ListWallCell.Count - 1);
            ListMaze.Add(ListWallCell[RandomCellIndex]);
            ListMaze[ListMaze.Count - 1].tag = "Maze";
            int num = int.Parse(ListMaze[ListMaze.Count - 1].name);
            ListWallCell.RemoveAt(RandomCellIndex);

            List<int> dir = new List<int> { 0, 1, 2, 3 };
            while (dir.Count != 0)
            {
                int dir_index = Random.Range(0, dir.Count - 1);
                Debug.Log(dir.Count);
                switch (dir_index)
                {
                    case 0: //Noth
                        if (num - targetCells * 2 >= 0 && listCell[num - targetCells * 2].tag == "Maze")
                        {
                            listCell[num - targetCells].tag = "Maze";
                            ListMaze.Add(listCell[num - targetCells]);
                            dir.Clear();
                        }
                        break;
                    case 1: //South
                        if (num + targetCells * 2 <= targetCells * targetCells && listCell[num + targetCells * 2].tag == "Maze")
                        {
                            listCell[num + targetCells].tag = "Maze";
                            ListMaze.Add(listCell[num + targetCells]);
                            dir.Clear();
                        }
                        break;
                    case 2: //East
                        if (num - 2 <= 0 && listCell[num - 2].tag == "Maze")
                        {
                            listCell[num - 1].tag = "Maze";
                            ListMaze.Add(listCell[num - 1]);
                            dir.Clear();
                        }
                        break;
                    case 3: //West
                        if (num + 2 <= 0 && listCell[num + 2].tag == "Maze")
                        {
                            listCell[num + 1].tag = "Maze";
                            ListMaze.Add(listCell[num + 1]);
                            dir.Clear();
                        }
                        break;
                }
                // dir.Remove(dir_index);
                dir.Clear();

            }
            //     int dir_index = Random.Range(0, dir.Count - 1);
            //    

            // }
            // if (num - targetCells * 2 >= 0 && listCell[num - targetCells * 2].tag == "Empty")
            //     ListWallCell.Add(listCell[num - targetCells * 2]);
            // if (num + targetCells * 2 <= targetCells * targetCells && listCell[num + targetCells * 2].tag == "Empty")
            //     ListWallCell.Add(listCell[num + targetCells * 2]);
            // if (num - 2 <= 0 && listCell[num - 2].tag == "Empty")
            //     ListWallCell.Add(listCell[num - 2]);
            // if (num + 2 >= 0 && listCell[num + 2].tag == "Empty")
            //     ListWallCell.Add(listCell[num + 2]);
        }
        foreach (var maze in ListMaze)
        {
            var q = maze.GetComponent<Image>();
            q.color = new Vector4(255, 255, 255, 255);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
