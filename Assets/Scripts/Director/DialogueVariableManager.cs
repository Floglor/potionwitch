using System;
using UnityEngine;

namespace Director
{
    public class DialogueVariableManager : MonoBehaviour
    {
        [SerializeField] private DirectorVariablesContainer _directorVariablesContainer;

        private void OnEnable()
        {
            if (_directorVariablesContainer == null)
                _directorVariablesContainer = GetComponent<DirectorVariablesContainer>();
        }

        public void ChangeVariable(string variableName, bool variableBool)
        {
            _directorVariablesContainer.ChangeVariable(variableName, variableBool);
        }
        
        public void ChangeVariable(string variableName, int variableInt)
        {
            _directorVariablesContainer.ChangeVariable(variableName, variableInt);
        }

        public bool CheckVariable(string firstVariable, string secondVariable, Operation operation)
        {
            if (_directorVariablesContainer.GetBoolVariableWithName(firstVariable) != null)
            {
                if (_directorVariablesContainer.GetBoolVariableWithName(secondVariable) == null)
                {
                    Debug.Log("Didn't find the variable");
                    return false;
                }

                CustomVariableBool first = _directorVariablesContainer.GetBoolVariableWithName(firstVariable);
                CustomVariableBool second = _directorVariablesContainer.GetBoolVariableWithName(secondVariable);
                
                return operation switch
                {
                    Operation.Equals => first.BoolVariable == second.BoolVariable,
                    Operation.And => first.BoolVariable && second.BoolVariable,
                    Operation.Or => first.BoolVariable || second.BoolVariable,
                    Operation.Not => first.BoolVariable != second.BoolVariable,
                    Operation.LessThan => false,
                    Operation.GreaterThan => false,
                    Operation.LessThanOrEqual => false,
                    Operation.GreaterThanOrEqual => false,
                    _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
                };
            }
            else
            {
                if (_directorVariablesContainer.GetBoolVariableWithName(firstVariable) == null)
                {
                    Debug.Log("Didn't find the variable");
                    return false;
                }
                
                CustomVariableInt first = _directorVariablesContainer.GetIntVariableWithName(firstVariable);
                CustomVariableInt second = _directorVariablesContainer.GetIntVariableWithName(secondVariable);
                
                return operation switch
                {
                    Operation.Equals => first.IntVariable == second.IntVariable,
                    Operation.And => false,
                    Operation.Or => false,
                    Operation.Not => first.IntVariable != second.IntVariable,
                    Operation.LessThan => first.IntVariable < second.IntVariable,
                    Operation.GreaterThan => first.IntVariable > second.IntVariable,
                    Operation.LessThanOrEqual => first.IntVariable <= second.IntVariable,
                    Operation.GreaterThanOrEqual => first.IntVariable >= second.IntVariable,
                    _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, null)
                };
            }
        }
    }
}