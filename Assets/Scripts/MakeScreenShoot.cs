using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class MakeScreenShoot : MonoBehaviour
{
    [SerializeField] Camera captureCamera;
    private int screenNum
    {
        get => PlayerPrefs.GetInt(screenIndexKey, 0);
        set
        {
            PlayerPrefs.SetInt(screenIndexKey, value);
            PlayerPrefs.Save();
        }
    }
    private const string screenIndexKey = "ScreenIndex";
    IEnumerator MakeScrenshot()
    {
        yield return new WaitForEndOfFrame();
        int width = this.captureCamera.pixelWidth;
        int height = this.captureCamera.pixelHeight;
        Texture2D texture = new Texture2D(width, height); //Промежуточная

        RenderTexture targetTexture = RenderTexture.GetTemporary(width, height); //целевая

        this.captureCamera.targetTexture = targetTexture;
        this.captureCamera.Render();
        RenderTexture.active = targetTexture;

        Rect rect = new Rect(0, 0, width, height);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        Object.Destroy(texture);
        File.WriteAllBytes(Application.persistentDataPath + "/SavedScreen_" + screenNum + ".png", bytes);
        Debug.Log(Application.persistentDataPath + "/SavedScreen_" + screenNum + ".png");
        screenNum++;
    }
    public void ScreenShoot()
    {
        StartCoroutine(MakeScrenshot());
    }
}
