using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class JSONHelper : MonoBehaviour
{
    public string GetValue(string jsonToSerialize, string pathToValue)
    {
        JToken json = JObject.Parse(jsonToSerialize);
        return json.SelectToken(pathToValue)?.ToString();
    }

    public JToken GetToken(string jsonToSerialize, string pathToValue)
    {
        JToken json = JObject.Parse(jsonToSerialize);
        return json.SelectToken(pathToValue);
    }
    
    public string GetValueFromJson(string jsonName, string pathToValue)
    {
        TextAsset json = FindJson(jsonName);
        JToken jToken = JObject.Parse(json.text);
        var value = jToken.SelectToken(pathToValue);
        
        if (value != null)
        {
            return value.ToString();
        }

        Debug.LogError($"JSONHelper - Cannot recover a value for the path '{pathToValue}' in the json '{jsonName}'");
        return string.Empty;
    }

    private TextAsset FindJson(string jsonName)
    {
        TextAsset json = Resources.Load<TextAsset>(jsonName);

        if (json != null)
        {
            return json;
        }

        Debug.LogError($"JSONHelper - '{jsonName}' could not be found in Resources folder");
        return null;
    }

    public string CreateJsonFromObject(object obj)
    {
        if (obj == null)
        {
            Debug.LogError("JSONHelper - Couldn't create a JSON from the provided object");
            return string.Empty;
        }
        
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        Debug.Log($"JSONHelper - The next json has been created: \n{json}");
        return json;
    }
}
