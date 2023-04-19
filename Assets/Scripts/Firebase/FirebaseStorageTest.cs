using System.Collections;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseStorageTest : MonoBehaviour
{
    private FirebaseStorage _storage;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the storage service, using the default Firebase App
        _storage = FirebaseStorage.DefaultInstance;
        GetImage();
    }

    private void GetImage()
    {
        Debug.Log("Getting image...");
        StorageReference pathReference = _storage.GetReferenceFromUrl("gs://tfgunity-f1271.appspot.com/");
        StorageReference image = pathReference.Child("Pawn.png");

        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadImage(task.Result.ToString()));
            }
        });
    }

    private IEnumerator LoadImage(string url)
    {
        Debug.Log("IN THE COROUTINE");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        if (www.result.Equals(UnityWebRequest.Result.ConnectionError) || www.result.Equals(UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("SOME MISTAKES WERE MADE, HONEY");
        }
        else
        {
            var myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            var mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            GetComponent<SpriteRenderer>().sprite = mySprite;
        }
    }
}
