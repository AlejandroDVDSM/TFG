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
        //Debug.Log("Loading sprite...");
        File.ReadAllBytesAsync(localPath).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("SpriteLoader - Error loading sprite");
            }
            else
            {
                //Debug.Log("Load sprite successfully");
                byte[] spriteData = task.Result;
                Texture2D texture2D = CreateTextureFromBytes(spriteData);
                Sprite sprite = CreateSpriteFromTexture2D(texture2D);
                target.SetSprite(sprite); 
            }
        });
    }
    
    public IEnumerator DownloadSprite(string referenceToStorage, string localPath, ITarget target)
    {
        //Debug.Log($"SpriteLoader - Downloading sprite in '{localPath}'...");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(referenceToStorage);
        yield return www.SendWebRequest();

        if (www.result.Equals(UnityWebRequest.Result.ConnectionError) ||
            www.result.Equals(UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError($"SpriteLoader - Error while downloading sprite for the reference '{referenceToStorage}' | Exception: '{www.error}'");
        }
        else
        {
            var bytes = www.downloadHandler.data;
            var directoryPath = $"{localPath.Substring(0, localPath.LastIndexOf('/'))}/";
            
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
                
            File.WriteAllBytes(localPath, bytes);
            LoadSprite(localPath, target);
            //Debug.Log("Success while downloading sprite");
        }
    }

    private Texture2D CreateTextureFromBytes(byte[] spriteData)
    {
        Texture2D texture2D = new Texture2D(2, 2)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };
        
        texture2D.LoadImage(spriteData);
        return texture2D;
    }

    private Sprite CreateSpriteFromTexture2D(Texture2D texture2D)
    {
        return Sprite.Create(
            texture2D, 
            new Rect(0.0f, 0.0f, texture2D.width, texture2D.height),
            new Vector2(0.5f, 0.5f),
            100.0f, 
            0, SpriteMeshType.FullRect, 
            new Vector4(106f, 100f, 116f, 126f));   
    }
}
