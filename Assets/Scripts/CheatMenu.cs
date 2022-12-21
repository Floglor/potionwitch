using System;
using Alchemy;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Ingredient _ingredient;

    private void Start()
    {
        _inventory.AddItem(_ingredient);
    }
}