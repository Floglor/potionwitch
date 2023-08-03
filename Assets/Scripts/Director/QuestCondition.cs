using System;
using UnityEngine;

namespace Director
{
    [Serializable]
    public struct QuestCondition
    {
        [SerializeField] public string FirstVariableName;
        [SerializeField] public Operation Operation;
        [SerializeField] public string SecondVariableName;
    }
}