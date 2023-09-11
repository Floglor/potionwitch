using UnityEngine;

namespace Director
{
    [CreateAssetMenu(fileName = "Money Reward", menuName = "Quest Reward/Money")]
    public class MoneyReward : QuestCompletion
    {
        public int Amount;
        
        public override void TriggerBehaviour()
        {
            FindObjectOfType<PlayerStats>().AddMoney(Amount);
        }
    }
}