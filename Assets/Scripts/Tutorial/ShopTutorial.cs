using UnityEngine;

public class ShopTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanelTutorialPrefab;
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private TutorialPanelPosition _tutorialPanel;
    
    private GameObject _shopPanelTutorialInstantiated;
    
    private void Start()
    {
        TutorialStateManager.OnTutorialStateChanged += InstantiateShop;
        TutorialStateManager.OnTutorialStateChanged += ShowShopButton;
        TutorialStateManager.OnTutorialStateChanged += CloseShop;
    }

    private void OnDestroy()
    {
        TutorialStateManager.OnTutorialStateChanged -= InstantiateShop;
        TutorialStateManager.OnTutorialStateChanged -= ShowShopButton;
        TutorialStateManager.OnTutorialStateChanged -= CloseShop;
    }

    private void ShowShopButton(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.ButtonShop) 
            return;
        
        _tutorialPanel.MiddlePosition();
    }

    private void InstantiateShop(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.Shop) 
            return;
        
        _tutorialPanel.BottomPosition();
        _shopPanelTutorialInstantiated = Instantiate(_shopPanelTutorialPrefab, _canvasTransform);
    }
    
    private void CloseShop(TutorialState tutorialState)
    {
        if (tutorialState != TutorialState.End) 
            return;
        
        Destroy(_shopPanelTutorialInstantiated);
    }
}
