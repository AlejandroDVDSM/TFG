using System.Collections;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseStorageTest : MonoBehaviour
{
    private FirebaseStorage _storage;
    
    private FirebaseStorageTest _instance;
    
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
        _storage = FirebaseStorage.DefaultInstance;
    }

    public void GetImage(string path, SpriteRenderer spriteRenderer)
    {
        Debug.Log("Getting image...");
        
        StorageReference pathReference = _storage.GetReferenceFromUrl($"gs://tfgunity-f1271.appspot.com/{path}");
        pathReference.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
                StartCoroutine(LoadSprite(task.Result.ToString(), spriteRenderer));
            else
                Debug.LogError($"FirebaseStorage - Error while getting image for the url '{path}'");
        });
    }
    
    private IEnumerator LoadSprite(string reference, SpriteRenderer spriteRenderer)
    {
        Debug.Log("Loading image...");
        
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(reference);
        yield return www.SendWebRequest();
        
        if (www.result.Equals(UnityWebRequest.Result.ConnectionError) || www.result.Equals(UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError($"FirebaseStorage - Error while loading image for the reference '{reference}'");
        }
        else
        {
            Debug.Log("FirebaseStorage - Success while loading image for the reference'");
            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            spriteRenderer.sprite = mySprite;

        }
    }
}
