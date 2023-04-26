using System.Collections;
using System.IO;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteLoader
{
    private static SpriteLoader _instance;

    public static SpriteLoader GetInstance()
    {
        if (_instance == null)
            _instance = new SpriteLoader();

        return _instance;
    }
    
    public void LoadSprite(string localPath, ITarget target)
    {
        Debug.Log("Loading sprite...");
        File.ReadAllBytesAsync(localPath).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("SpriteLoader - Error loading sprite");
            }
            else
            {
                Debug.Log("Load sprite successfully");
                byte[] spriteData = task.Result;
                Texture2D myTexture = new Texture2D(2, 2);
                myTexture.LoadImage(spriteData);

                var mySprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height),
                    new Vector2(0.5f, 0.5f), 100.0f);
                target.SetSprite(mySprite);
            }
        });
    }
    
    public IEnumerator DownloadSprite(string referenceToStorage, string localPath, ITarget target)
    {
        Debug.Log("Downloading sprite...");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(referenceToStorage);
        yield return www.SendWebRequest();

        if (www.result.Equals(UnityWebRequest.Result.ConnectionError) ||
            www.result.Equals(UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError($"SpriteLoader - Error while downloading sprite for the reference '{referenceToStorage}'");
        }
        else
        {
            var bytes = www.downloadHandler.data;
            //Directory.CreateDirectory($"{Application.persistentDataPath}/chesspieces/Black/");
            var directoryPath = $"{localPath.Substring(0, localPath.LastIndexOf('/'))}/";
            
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
                
            File.WriteAllBytes(localPath, bytes);
            LoadSprite(localPath, target);
            Debug.Log("Success while downloading sprite");
        }
    }

}
