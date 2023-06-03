using System.Collections.Generic;
using Inventory;
using UnityEngine;


//TODO: Create filling randomizer
public class ShopItemRandomizer : MonoBehaviour
{
    public List<SeedDate> ItemDates;

    public List<IItem> UpdateShop(int date)
    {
        List<IItem> procItemList = new List<IItem>();

        foreach (SeedDate itemDate in ItemDates)
        {
            if (itemDate.StartingDate > date) continue;
            if (date % itemDate.DateDivident != 0) continue;

            bool chanceSucceed = false || Random.Range(0, 100) >= itemDate.Chance;

            if (!chanceSucceed) continue;
            
            int randomCount = Random.Range(1, itemDate.MaxCount);

            for (int i = 1; i <= randomCount; i++)
            {
                procItemList.Add(itemDate.Item);
            }
        }

        return procItemList;
    }
}