using UnityEngine;

public class GalleryController : MonoBehaviour
{
    private ImageManager imageManager;

    void Start()
    {
        imageManager = FindObjectOfType<ImageManager>();
        if (imageManager != null)
        {
            imageManager.LoadGalleryImages();
        }
        else
        {
            Debug.LogError("ImageUploader object not found in the scene!");
        }
    }
}

