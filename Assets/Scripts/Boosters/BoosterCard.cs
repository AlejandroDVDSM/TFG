using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoosterCard : MonoBehaviour
{
    private Booster _booster;
    [SerializeField] private Button _button;
    
    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("TutorialScene"))
        {
            DisableButtonIfTutorial();
            return;
        }
        
        _button.onClick.AddListener(CheckIfCanBuy);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(CheckIfCanBuy);
    }
    
    public void InitializeBooster(Booster booster)
    {
        _booster = booster;
        name = booster.BoosterName;
        GetComponent<BoosterCardDisplay>().DisplayData(_booster);
    }
    
    private void CheckIfCanBuy()
    {
        Debug.Log($"Trying to buy '{name}'");
        int steps = int.Parse(PlayerPrefs.GetString("steps"));
        
        if (steps >= _booster.Cost)
            GetComponentInParent<BoosterManager>().BuyBooster(name, _booster.Cost);
        else
        {
            AudioManager.Instance.Play("Error");
            Debug.Log("no money no booster my friend");
        }
    }

    private void DisableButtonIfTutorial()
    {
        Debug.Log("Disabling button as we are in the tutorial");
        _button.interactable = false;
    }
}
