using System;
using System.IO;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;

public class Storage : MonoBehaviour
{
    private FirebaseStorage _storage;
    private Storage _instance;
    private bool _initialized;
    public bool Initialized => _initialized;
    private string _baseURL;
    private string _versionURL;

    public static event Action OnStorageInitialized;

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
        if (!Initialized)
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
        OnStorageInitialized?.Invoke();
    }

    private void DefineBaseUrl()
    {
        string storageBucket = FindObjectOfType<JSONHelper>()
            .GetValueFromJson("google-services", "$.project_info.storage_bucket");
        
        int version = 1;
        
        if (PlayerPrefs.HasKey("version"))
            version = PlayerPrefs.GetInt("version");

        _versionURL = $"Versions/{version}";
        _baseURL = $"gs://{storageBucket}/{_versionURL}";
        Debug.Log($"Set BaseUrl: '{_baseURL}'");
    }
    
    public void InitializeSprite(string storagePath, ITarget target)
    {
        string localPath = $"{Application.persistentDataPath}/{_versionURL}/{storagePath}";
        
        if (File.Exists(localPath))
        { // Load
            Debug.Log($"Found local sprite in '{localPath}'");
            SpriteLoader.GetInstance().LoadSprite(localPath, target);
        }
        else
        { // Download
            Debug.Log($"Didn't find local sprite in '{localPath}'. Preparing things to get it from Firebase Storage");
            GetSpriteFromStorage(storagePath, target);
        }
    }

    private void GetSpriteFromStorage(string storagePath, ITarget target)
    {
        StorageReference pathReference = _storage.GetReferenceFromUrl($"{_baseURL}/{storagePath}");
        pathReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"Error getting '{storagePath}' from Storage. Exception: '{task.Exception}'");
            }
            else
            {
                string localPath = $"{Application.persistentDataPath}/{_versionURL}/{storagePath}";
                StartCoroutine(SpriteLoader.GetInstance().DownloadSprite(task.Result.ToString(), localPath, target));
            }
        });        
    }
}
