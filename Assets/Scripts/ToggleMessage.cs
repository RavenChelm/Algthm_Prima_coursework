using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMessage : MonoBehaviour
{
    public void Message()
    {
        GameObject v = GameObject.Find("Canvas");
        v.SendMessage("SelectToggle", this.gameObject);
    }
}
