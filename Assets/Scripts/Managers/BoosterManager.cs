using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private GameObject _boosterCardPrefab;
    [SerializeField] private Booster[] _boosters;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadBoostersInShop();
    }

    private void LoadBoostersInShop()
    {
        foreach (var booster in _boosters)
        {
            GameObject boosterCard = Instantiate(_boosterCardPrefab, transform);
            boosterCard.GetComponent<BoosterCard>().LoadData(booster);

        }
        
    }
}
