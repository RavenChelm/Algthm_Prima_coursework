using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject CanvasController;
    private Game gm;
    private Vector2 scale = new Vector2(101, 101);

    public void CreateCanvas()
    {
        CanvasController.SendMessage("GenerateGrid", scale);
    }
    public void GenerateMaze()
    {
        CanvasController.SendMessage("GenerateMaze");
        Game.current.InputLastMaze(CanvasController.GetComponent<AlgPrim>().Cells);
        Debug.Log(Game.current.LastMaze.Count);
        SaveLoad.Save();
    }
    private void SaveCurrentState() //TODO изменить на сохранение в новый слот, а Reset оставить на первоначальную генерацию SAv
    {
        Game.current.InputLastMaze(CanvasController.GetComponent<AlgPrim>().Cells);
        SaveLoad.Save();
    }
    public void Clear_DeadEnd()
    {
        CanvasController.SendMessage("Clear_DeadEnd");
    }
    public void GrowingMap()
    {
        CanvasController.SendMessage("GrowingMap");
    }
    public void ResetMaze()
    {
        RemoveCanvas();
        CanvasController.SendMessage("GenerateGrid", new Vector2(Game.current.LastMaze.Count, Game.current.LastMaze[0].Count));
        Game.LoadLastMaze(CanvasController.GetComponent<AlgPrim>().Cells);
        CanvasController.SendMessage("ColorMaze");

    }
    public void ExitAplication()
    {
        Application.Quit();
    }
    public void RemoveCanvas()
    {
        CanvasController.SendMessage("GridDelete");
    }
    public void InputScaleGrid()
    {

    }
    public void MenuOn(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void MenuOff(GameObject obj)
    {
        obj.SetActive(false);
    }
}

