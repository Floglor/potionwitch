using Alchemy;
using UnityEngine;

namespace Director
{
    [CreateAssetMenu(fileName = "Potion Reward", menuName = "Quest Reward/Potion")]
    public class PotionReward : QuestCompletion
    {
        public Potion Potion;
        
        public override void TriggerBehaviour()
        {
            FindObjectOfType<Inventory.Inventory>().SpawnItem(Potion);
        }
    }
}