using Garden;
using UnityEngine;

namespace Director
{
    [CreateAssetMenu(fileName = "Seed Reward", menuName = "Quest Reward/Seed")]
    public class SeedReward : QuestCompletion
    {
        public GardenSeed Seed;
        
        public override void TriggerBehaviour()
        {
            FindObjectOfType<Inventory.Inventory>().SpawnItem(Seed);
        }
    }
}