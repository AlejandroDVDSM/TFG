using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestHelper : MonoBehaviour
{
    // GET
    public void SendGetRequest(string uri, System.Action<string> callback)
    {
        StartCoroutine(SendGetRequestCoroutine(uri, callback));
    }
    
    // Coroutine GET
    private IEnumerator SendGetRequestCoroutine(string uri, System.Action<string> callback = null)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error Response for GET: '{uri}' - Error: '{www.error}'");
        }
        else
        {
            Debug.Log($"Success Response for GET: '{uri}' - Response: '{www.downloadHandler.text}'");
            callback?.Invoke(www.downloadHandler.text);
        }
    }

    // GET + Header
    public void SendGetRequest(string uri, string headerValue, System.Action<string> callback)
    {
        StartCoroutine(SendGetRequestCoroutine(uri, headerValue, callback));
    }
    
    // Coroutine GET + Header
    private IEnumerator SendGetRequestCoroutine(string uri, string headerValue, System.Action<string> callback = null)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        www.SetRequestHeader("Authorization", headerValue);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error Response for GET: '{uri}' - Error: '{www.error}'");
        }
        else
        {
            Debug.Log($"Success Response for GET: '{uri}' - Response: '{www.downloadHandler.text}'");
            callback?.Invoke(www.downloadHandler.text);
        }
    }
    
    // POST
    public void SendPostRequest(string uri, WWWForm form, System.Action<string> onSuccess)
    {
        StartCoroutine(SendPostRequestCoroutine(uri, form, onSuccess));
    }

    // Coroutine POST
    private IEnumerator SendPostRequestCoroutine(string uri, WWWForm form, System.Action<string> onSuccess = null)
    {
        if (form != null)
        {
            UnityWebRequest www = UnityWebRequest.Post(uri, form);
            yield return www.SendWebRequest();
            
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error Response for POST: '{uri}' - Error: '{www.error}'");
            }
            else
            {
                Debug.Log($"Success Response for POST: '{uri}' - Response: '{www.downloadHandler.text}'");
                onSuccess?.Invoke(www.downloadHandler.text);
            }        
        }
        else
        {
            Debug.LogError("Error sending a POST request. Form is null");
        }
    }
    
    // POST + JSON
    public void SendPostRequest(string uri, string headerValue, string json, System.Action<string> onSuccess, System.Action<string> onFailed = null)
    {
        StartCoroutine(SendPostRequestCoroutine(uri, headerValue, json, onSuccess, onFailed));
    }
    
    // Coroutine POST + JSON
    private IEnumerator SendPostRequestCoroutine(string url, string headerValue, string json, System.Action<string> onSuccess = null, System.Action<string> onFailed = null)
    {
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Authorization", headerValue);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error Response for POST: '{url}' - Error: '{www.error}'");
            onFailed?.Invoke(www.responseCode.ToString());
        }
        else
        {
            Debug.Log($"Success Response for POST: '{url}' - Response: '{www.downloadHandler.text}'");
            onSuccess?.Invoke(www.downloadHandler.text);
        }
    }
}