using UnityEngine;

[RequireComponent(typeof(ImageAdapter))]
public class MuteButton : MonoBehaviour
{
    private const string Mute = "UI/Icons/Mute_Pushed.png";
    private const string Unmute = "UI/Icons/Volume_2_Pushed.png";

    private void Start()
    {
        SwitchSoundIcon();
    }

    public void SwitchSoundIcon()
    {
        ITarget target = GetComponent<ITarget>();
        Storage storage = FindObjectOfType<Storage>();
        storage.InitializeSprite(AudioListener.pause ? Mute : Unmute, target);        
    }
}
