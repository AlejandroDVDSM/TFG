using System.Collections;
using System.IO;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;

public class Storage : MonoBehaviour
{
    private FirebaseStorage _storage;
    private Storage _instance;
    private bool _initialized;
    private string _baseURL;
    
    private void Awake()
    {
        // Singleton
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }    
    
    void Start()
    {
        if (!_initialized)
            FirebaseInitializer.OnDependenciesFixed += InitializeStorage;
    }

    private void OnDestroy()
    {
        FirebaseInitializer.OnDependenciesFixed -= InitializeStorage;
    }

    private void InitializeStorage()
    {
        Debug.Log("Initializing Firebase Storage...");
        _storage = FirebaseStorage.DefaultInstance;
        DefineBaseUrl();
        _initialized = true;
    }

    private void DefineBaseUrl()
    {
        string storageBucket = FindObjectOfType<JSONHelper>()
            .GetValueFromJson("google-services", "$.project_info.storage_bucket"); 
        _baseURL = $"gs://{storageBucket}";
    }
    
    public void InitializeSprite(string storagePath, ITarget target)
    {
        string localPath = $"{Application.persistentDataPath}/{storagePath}";
        
        if (File.Exists(localPath))
        { // Load
            Debug.Log($"Found local sprite in '{localPath}'");
            SpriteLoader.GetInstance().LoadSprite(localPath, target);
        }
        else
        { // Download
            Debug.Log($"Didn't find local sprite in '{localPath}'. Preparing things to download sprite from Firebase Storage");
            DownloadSprite(storagePath, target);
        }
    }

    private void DownloadSprite(string storagePath, ITarget target)
    {
        StorageReference pathReference = _storage.GetReferenceFromUrl($"{_baseURL}/{storagePath}");
        pathReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"Error downloading '{storagePath}'. Exception: '{task.Exception}'");
            }
            else
            {
                string localPath = $"{Application.persistentDataPath}/{storagePath}";
                StartCoroutine(SpriteLoader.GetInstance().DownloadSprite(task.Result.ToString(), localPath, target));
            }
        });        
    }
}
