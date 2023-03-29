using UnityEngine;
using Newtonsoft.Json.Linq;

public class JSONHelper : MonoBehaviour
{
    public string GetValueFromJson(string jsonName, string pathToValue)
    {
        TextAsset json = FindJson(jsonName);

        JToken jToken = JObject.Parse(json.text);
        var value = jToken.SelectToken(pathToValue)?.ToString();
        Debug.Log("CLIENT_ID: " + value);
        return value;
    }

    private TextAsset FindJson(string jsonName)
    {
        return Resources.Load<TextAsset>(jsonName);
    }
}
