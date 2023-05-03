using UnityEngine;

public class BoosterCollectionDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _boosterCardPrefab;
    
    public void DisplayBoostersCardsInShop(Booster[] boosters)
    {
        foreach (var booster in boosters)
        {
            GameObject boosterCard = Instantiate(_boosterCardPrefab, transform);
            boosterCard.GetComponent<BoosterCard>().InitializeBooster(booster);
        }        
    }
}
