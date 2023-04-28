using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    private Dictionary<string, IBoosterFactory> _factories = new()
    {
        {"Double Points", new BoosterDoublePointsFactory()},
        {"New Random Piece", new BoosterNewRandomPieceFactory()}
    };

    public void BuyBooster(string boosterName, int boosterCost)
    {
        var factory = _factories.FirstOrDefault(f => f.Key.Equals(boosterName)).Value;
        factory.ApplyBooster();
        FindObjectOfType<GoogleFit>().SubtractSteps(boosterCost);
        AudioManager.Instance.Play("BuyBooster");
    }
}