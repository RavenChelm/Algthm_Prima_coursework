using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{

    [SerializeField] private GameObject CanvasController;
    [SerializeField] private GameObject MazeVieyField;
    [SerializeField] private GameObject MazeNamePrefab;
    private List<GameObject> ToggleSave = new List<GameObject>();
    private GameObject CurrentSave = null;
    private Game gm;
    private Vector2 scale = new Vector2(101, 101);


    //controll maze
    public void CreateCanvas()
    {
        CanvasController.SendMessage("GenerateGrid", scale);
    }
    public void GenerateMaze()
    {
        CanvasController.SendMessage("GenerateMaze");
        Game.InputMaze(CanvasController.GetComponent<AlgPrim>().Cells);
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
        CreateCanvas();
        Game.LoadMaze(CanvasController.GetComponent<AlgPrim>().Cells);
        CanvasController.GetComponent<AlgPrim>().ColorMaze();
    }
    public void RemoveCanvas()
    {
        CanvasController.SendMessage("GridDelete");
    }

    //controll app
    public void ExitAplication()
    {
        Application.Quit();
    }
    public void MenuOn(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void MenuOff(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void EndInputName(string name)
    {
        Game.InputName(name);
    }
    public void SaveMaze()
    {
        Game.InputMaze(CanvasController.GetComponent<AlgPrim>().Cells);//change
        SaveLoad.Save();
    }
    public void LoadMazeToView()
    {
        SaveLoad.Load();
        if (MazeVieyField.transform.childCount > 0)
        {
            for (int j = MazeVieyField.transform.childCount; j > 0; --j)
            {
                DestroyImmediate(MazeVieyField.transform.GetChild(0).gameObject);
            }
        }
        ToggleSave.Clear();
        int i = 0;
        foreach (Game gm in SaveLoad.savedGames)
        {
            GameObject textOBj = Instantiate(MazeNamePrefab, MazeVieyField.transform);
            textOBj.name = i.ToString();
            i++;
            ToggleSave.Add(textOBj);
            var text = textOBj.transform.GetChild(1).GetComponent<TMP_Text>();
            text.SetText(" Name: " + gm.Name + " , " + "Size: " + gm.width + "x" + gm.height);
        }
    }
    public void SelectToggle(GameObject toggle)
    {
        foreach (var tog in ToggleSave)
        {
            if (tog.GetComponent<Toggle>().isOn == true)
            {
                CurrentSave = tog;
            }
            tog.GetComponent<Toggle>().isOn = false;
            tog.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0, 0, 0, 255);
        }
        if (CurrentSave != null)
        {
            CurrentSave.transform.GetChild(0).GetComponent<Image>().color = new Vector4(200, 200, 200, 255);
        }
        else
            Debug.Log("error!");
    }
    public void Load()
    {
        int i = 0;

        foreach (var tog in ToggleSave)
        {
            Debug.Log(SaveLoad.savedGames[i].Name);
            if (tog == CurrentSave)
            {
                Game.current = SaveLoad.savedGames[i];
            }
            i++;
        }
        ResetMaze();
    }
}

