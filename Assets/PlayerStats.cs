using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _money;

    private void Start()
    {
        AddMoney(100);
    }

    public void AddMoney(int sum)
    {
        _money += sum;
    }

    public bool TryBuying(int cost)
    {
        if (_money < cost) return false;
        
        _money -= cost;
        return true;
    }
}