using System;
using System.Linq;
using UnityEngine.Audio; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] Sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }        
        
        DontDestroyOnLoad(gameObject);
        SetSounds();
    }

    private void OnEnable()
    {
        Play("MainMenuTheme");
    }

    private void SetSounds()
    {
        foreach (var sound in Sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
        }
    }

    public void Play(string name)
    {
        var sound = Sounds.FirstOrDefault(sound => sound.Name.Equals(name));

        if (sound == null)
        {
            Debug.LogError($"AudioManager - Could not find a sound with the name '{name}'");
            return;
        }
        
        Debug.Log($"Playing the next sound: '{name}'");
        sound.source.Play();
    }
    
    public void Stop(string name)
    {
        
    }
}
