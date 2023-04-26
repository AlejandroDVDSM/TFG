using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImageAdapter))]
public class UISpriteLoader : MonoBehaviour
{
    [SerializeField] private string _path;

    private Storage _storage;
    
    // Start is called before the first frame update
    void Start()
    {
        _storage = FindObjectOfType<Storage>();
        
        if (_storage != null)
            _storage.TestDownloadLoad(_path, GetComponent<ITarget>());
        else
            Debug.LogError("UISpriteLoader - Couldn't find an object of type FirebaseStorage");
    }
}
