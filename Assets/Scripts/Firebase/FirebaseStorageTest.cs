using System.Collections;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseStorageTest : MonoBehaviour
{
    private FirebaseStorage _storage;
    private FirebaseStorageTest _instance;
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
        //InitializeStorage();
    }

    private void OnDestroy()
    {
        FirebaseInitializer.OnDependenciesFixed -= InitializeStorage;
    }

    private void InitializeStorage()
    {
        Debug.Log("Initializing Firebase Storage...");
        _storage = FirebaseStorage.DefaultInstance;
        _baseURL = "gs://" + FindObjectOfType<JSONHelper>().GetValueFromJson("google-services", "$.project_info.storage_bucket");
        _initialized = true;
    }

    public void GetImage(string path, ITarget target)
    {
        Debug.Log("Getting image...");
        
        StorageReference pathReference = _storage.GetReferenceFromUrl($"{_baseURL}/{path}");
        pathReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
                StartCoroutine(LoadSprite(task.Result.ToString(), target));
            else
                Debug.LogError($"FirebaseStorage - Error while getting image for the url '{path}'");
        });
    }
    
    private IEnumerator LoadSprite(string reference, ITarget target)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(reference);
        yield return www.SendWebRequest();
        
        if (www.result.Equals(UnityWebRequest.Result.ConnectionError) || www.result.Equals(UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError($"FirebaseStorage - Error while loading image for the reference '{reference}'");
        }
        else
        {
            Debug.Log("FirebaseStorage - Success while loading image");
            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            target.SetSprite(mySprite);

        }
    }
}
