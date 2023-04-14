using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    private Dictionary<string, IBoosterFactory> _factories = new()
    {
        {"Double Points", new BoosterDoublePointsFactory()},
        {"Spawn Pawn", new BoosterSpawnPawnFactory()}
    };

    public void BuyBooster(string boosterName)
    {
        var factory = _factories.FirstOrDefault(f => f.Key.Equals(boosterName)).Value;
        factory.ApplyBooster();
    }
}
