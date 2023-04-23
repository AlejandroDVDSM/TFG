using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ImageAdapter))]
public class UISpriteLoader : MonoBehaviour
{
    [SerializeField] private string _path;

    private FirebaseStorageTest _firebaseStorageTest;
    
    // Start is called before the first frame update
    void Start()
    {
        _firebaseStorageTest = FindObjectOfType<FirebaseStorageTest>();
        
        if (_firebaseStorageTest != null)
            _firebaseStorageTest.GetImage(_path, GetComponent<ITarget>());
        else
            Debug.LogError("UISpriteLoader - Couldn't find an object of type FirebaseStorage");
    }
}
