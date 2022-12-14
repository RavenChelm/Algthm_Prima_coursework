using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
    //Функционал
    public static Game current;
    private string Clear = "Clear";
    private string Wall = "Wall";
    //Сохраняемые данные
    private List<List<string>> SaveMaze = new List<List<string>>();
    public int height;
    public int width;
    public string Name;
    public Game()
    {
        height = 0;
        width = 0;
        current = this;
        Name = "";
    }
    public static void InputMaze(List<List<GameObject>> Maze)
    {
        current.width = Maze.Count;
        current.height = Maze[0].Count;
        foreach (var insideList in current.SaveMaze)
            insideList.Clear();
        current.SaveMaze.Clear();
        foreach (var X in Maze)
        {
            List<string> tmp = new List<string>();
            foreach (var Y in X)
            {
                tmp.Add(Y.tag);
            }
            current.SaveMaze.Add(tmp);
        }
    }
    public static void LoadMaze(List<List<GameObject>> Maze)
    { //Логика заключается в присваивание сохранённых тегов объектам лабиринта
        for (int x = 0; x < current.SaveMaze.Count; x++)
        {
            for (int y = 0; y < current.SaveMaze[0].Count; y++)
            {
                Maze[x][y].tag = current.SaveMaze[x][y];
            }
        }
    }
    public static void InputName(string name)
    {
        current.Name = name;
    }




}