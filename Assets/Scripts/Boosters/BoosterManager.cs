using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    [SerializeField] private Booster[] _boosters;
    
    private Dictionary<string, IBoosterFactory> _factories = new();
    
    private void Start()
    {
        GetComponent<BoosterCollectionDisplay>().DisplayBoostersCardsInShop(_boosters);

        var factoriesComponents = GetComponents<IBoosterFactory>();
        for (int i = 0; i < factoriesComponents.Length; i++)
        {
            _factories.Add(_boosters[i].name, factoriesComponents[i]);
        }
    }

    public void BuyBooster(string boosterName, int boosterCost)
    {
        var factory = GetFactory(boosterName);
        factory.ApplyBooster();
        
        FindObjectOfType<GoogleFit>().SubtractSteps(boosterCost);
        AudioManager.Instance.Play("BuyBooster");
    }

    private IBoosterFactory GetFactory(string boosterName)
    {
        return _factories.FirstOrDefault(f => f.Key.Equals(boosterName)).Value;
    }
    
}