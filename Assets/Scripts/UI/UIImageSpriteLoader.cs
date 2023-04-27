using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImageAdapter))]
public class UIImageSpriteLoader : MonoBehaviour
{
    [SerializeField] private string _path;

    private Storage _storage;
    
    // Start is called before the first frame update
    void Start()
    {
        _storage = FindObjectOfType<Storage>();

        if (_storage != null)
        {
            if (_storage.Initialized)
                InitializeImageSprite();
            else
                Storage.OnStorageInitialized += InitializeImageSprite;
        }
        else
        {
            Debug.LogError("UISpriteLoader - Couldn't find an object of type Storage");
        }
    }

    private void InitializeImageSprite()
    {
        _storage.InitializeSprite(_path, GetComponent<ITarget>());
    }
}
