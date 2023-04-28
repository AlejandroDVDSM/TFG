using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    [HideInInspector]
    public AudioSource source;
    public AudioClip Clip;
    
    [Range(0f, 1f)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public bool Loop;

}
