using UnityEngine;

public class BoosterCollectionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _boosterCardPrefab;
    [SerializeField] private Booster[] _boosters;
    
    // Start is called before the first frame update
    void Start()
    {
        DisplayBoostersCardsInShop();
    }

    private void DisplayBoostersCardsInShop()
    {
        foreach (var booster in _boosters)
        {
            GameObject boosterCard = Instantiate(_boosterCardPrefab, transform);
            boosterCard.GetComponent<BoosterCard>().InitializeBooster(booster);
        }
    }
}
