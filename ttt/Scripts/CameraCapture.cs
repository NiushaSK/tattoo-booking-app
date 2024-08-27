using UnityEngine;
using System.Collections;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public Camera  MainCamera; // Deine XR Origin Kamera
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/Gallery/";
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
    }

    // Diese Methode kann nun direkt im Unity-Editor der On Click() Funktion des Buttons hinzugefügt werden
    public void TakePhoto()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        byte[] imageBytes = screenImage.EncodeToPNG();
        string fileName = "Image_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        string filePath = savePath + fileName;
        File.WriteAllBytes(filePath, imageBytes);

        Debug.Log("Screenshot saved to: " + filePath);

        Destroy(screenImage);
    }
}
