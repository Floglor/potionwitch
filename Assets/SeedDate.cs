using Garden;
using Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDate", menuName = "ItemDate")]
public class SeedDate : ScriptableObject
{
    public int StartingDate;
    public GardenSeed Item;
    public int DateDivident;
    public int Chance;
    public int MaxCount;

}