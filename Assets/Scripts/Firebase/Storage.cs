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
    private int _latestVersion = 3;
    
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


        if (PlayerPrefs.HasKey("version"))
            _latestVersion = 2;/*PlayerPrefs.GetInt("version");*/ // TEST TEST TEST TEST

        //_versionURL = $"Versions/{/*version*/2}";
        _baseURL = $"gs://{storageBucket}/Versions/{_latestVersion}";// TEST TEST TEST TEST// TEST TEST TEST TEST
        Debug.Log($"Set BaseUrl: '{_baseURL}'");
    }
    
    private string GetVersionURL(int version)
    {
        string storageBucket = FindObjectOfType<JSONHelper>()
            .GetValueFromJson("google-services", "$.project_info.storage_bucket");

        return $"gs://{storageBucket}/Versions/{version}";
    }
    
    public void InitializeSprite(string storagePath, ITarget target)
    {
        string localPath = $"{Application.persistentDataPath}/Versions/{_latestVersion}/{storagePath}";
        
        if (File.Exists(localPath))
        { // Load
            Debug.Log($"Found local sprite in '{localPath}'");
            SpriteLoader.GetInstance().LoadSprite(localPath, target);
        }
        else
        { // Download
            Debug.Log($"Didn't find local sprite in '{localPath}'. Preparing things to get it from Firebase Storage");
            GetSpriteFromStorage(storagePath, target, _latestVersion);
        }
    }

    
    private void GetSpriteFromStorage(string storagePath, ITarget target, int version)
    {
        StorageReference pathReference = _storage.GetReferenceFromUrl($"{GetVersionURL(version)}/{storagePath}");
        pathReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"Error getting '{pathReference}' from Storage. Exception: '{task.Exception}'");
                RetryInitializeSprite(storagePath, target, version - 1);
            }
            else
            {
                string localPath = $"{Application.persistentDataPath}/Versions/{version}/{storagePath}";
                StartCoroutine(SpriteLoader.GetInstance().DownloadSprite(task.Result.ToString(), localPath, target));
            }
        });        
    }

    private void RetryInitializeSprite(string storagePath, ITarget target, int version)
    {
        Debug.Log($"Retrying initialize sprite with version: '{version}'");
        if (version < 1)
            return;
        
        string localPath = $"{Application.persistentDataPath}/Versions/{version}/{storagePath}";
        
        if (File.Exists(localPath))
        { // Load
            Debug.Log($"Found local sprite with older version in '{localPath}'");
            SpriteLoader.GetInstance().LoadSprite(localPath, target);
        }
        else
        { // Download
            Debug.Log($"Didn't find local sprite with older version in '{localPath}'. Preparing things to get it from Firebase Storage");
            GetSpriteFromStorage(storagePath, target, version);
        }
    }    
}
