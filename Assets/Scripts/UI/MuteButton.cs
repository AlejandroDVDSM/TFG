using UnityEngine;

[RequireComponent(typeof(ImageAdapter))]
public class MuteButton : MonoBehaviour
{
    private readonly string _mute = "UI/Icons/Mute_Pushed.png";
    private readonly string _unmute = "UI/Icons/Volume_2_Pushed.png";

    private void Start()
    {
        SwitchSoundIcon();
    }

    public void SwitchSoundIcon()
    {
        ITarget target = GetComponent<ITarget>();
        Storage storage = FindObjectOfType<Storage>();
        storage.InitializeSprite(AudioListener.pause ? _mute : _unmute, target);        
    }
}
