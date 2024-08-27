using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class ImageManager : MonoBehaviour
{
    public RectTransform galleryPanel;  // Panel für die Bilder in der Galerie
    public GameObject imagePrefab;      // Prefab für die kleinen Bilder in der Galerie
    private string galleryPath;
    private static string pendingImagePath;

    void Awake()
    {
        galleryPath = Application.persistentDataPath + "/Gallery/";

        if (!Directory.Exists(galleryPath))
        {
            Directory.CreateDirectory(galleryPath);
        }
    }

    void Start()
    {
        // Lade die Bilder nur in der Galerie-Szene
        if (SceneManager.GetActiveScene().name == "2_GalleryScene")
        {
            LoadGalleryImages();
        }
    }

    public void UploadImageFromGallery()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Bildpfad speichern und zur Bestätigungs-Szene wechseln
                pendingImagePath = path;
                SceneManager.LoadScene("4_UploadScene");
            }
        }, "Select an image", "image/*");

        if (permission == NativeGallery.Permission.ShouldAsk)
        {
            Debug.LogWarning("Permission to access gallery is required!");
        }
    }

    public void ConfirmImageUpload()
    {
        if (!string.IsNullOrEmpty(pendingImagePath))
        {
            // Speicher das Bild im Galerie-Verzeichnis
            byte[] imageBytes = File.ReadAllBytes(pendingImagePath);
            string fileName = "Uploaded_" + Path.GetFileName(pendingImagePath);
            string filePath = Path.Combine(galleryPath, fileName);
            File.WriteAllBytes(filePath, imageBytes);

            // Zur Galerie-Szene wechseln
            SceneManager.LoadScene("2_GalleryScene");
        }
    }

    public void CancelImageUpload()
    {
        // Zur Main-Scene zurückkehren
        SceneManager.LoadScene("1_MainScene");
    }

    private void AddImageToGallery(string filePath)
    {
        byte[] imageBytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        GameObject newImage = Instantiate(imagePrefab, galleryPanel);
        newImage.GetComponent<RawImage>().texture = texture;

        // Optional: Füge eine Funktion hinzu, um das Bild bei Klick zu vergrößern
        newImage.GetComponent<Button>().onClick.AddListener(() => EnlargeImage(texture));
    }

    public void EnlargeImage(Texture2D texture)
    {
        // Hier sollte die Logik zur Anzeige des vergrößerten Bildes integriert werden
        Debug.Log("Image enlarged: " + texture.name);
    }

    public void LoadGalleryImages()
    {
        Debug.Log("GalleryPath:" + galleryPath);
        
        if (!Directory.Exists(galleryPath))
        {
            Debug.LogError("Gallery directory does not exist!");
            return;
        }

        // Lade alle Bilder im Galerie-Verzeichnis und füge sie der Galerie hinzu
        string[] files = Directory.GetFiles(galleryPath);
        foreach (string file in files)
        {
            AddImageToGallery(file);
        }
    }
}
