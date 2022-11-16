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
    public List<List<string>> LastMaze = new List<List<string>>();
    private int height;
    private int width;
    private string Name;
    public Game()
    {
        height = 0;
        width = 0;
        current = this;
        Name = "";
    }
    public void InputMaze(List<List<GameObject>> Maze)
    {
        current.width = Maze.Count;
        current.height = Maze[0].Count;
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
    public void InputLastMaze(List<List<GameObject>> Maze)
    {
        current.width = Maze.Count;
        current.height = Maze[0].Count;
        current.LastMaze.Clear();
        foreach (var X in Maze)
        {
            List<string> tmp = new List<string>();
            foreach (var Y in X)
            {
                tmp.Add(Y.tag);
            }
            current.LastMaze.Add(tmp);
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
    public static void LoadLastMaze(List<List<GameObject>> Maze)
    { //Логика заключается в присваивание сохранённых тегов объектам лабиринта
        for (int x = 0; x < current.SaveMaze.Count; x++)
        {
            for (int y = 0; y < current.SaveMaze[0].Count; y++)
            {
                Maze[x][y].tag = current.LastMaze[x][y];
            }
        }
    }
    public static void InputName(string name)
    {
        current.Name = name;
    }




}