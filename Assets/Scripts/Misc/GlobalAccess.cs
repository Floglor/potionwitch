using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GlobalAccess : MonoBehaviour
{
    public static GlobalAccess Instance;
    public Inventory Inventory;

    private void Awake()
    {
        Instance = this;
    }
}
