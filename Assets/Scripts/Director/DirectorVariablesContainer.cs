using System.Collections.Generic;
using System.Linq;
using QFSW.QC;
using UnityEngine;

namespace Director
{
    public class DirectorVariablesContainer : MonoBehaviour
    {
        [SerializeField] private List<CustomVariableBool> _customVariablesBool;
        [SerializeField] private List<CustomVariableInt> _customVariablesInt;


        [Command("ChangeBoolVar")]
        public void ChangeVariable(string variableName, bool variableBool)
        {
            for (int i = 0; i < _customVariablesBool.Count; i++)
            {
                if (!variableName.Equals(_customVariablesBool[i].VariableName)) continue;
                
                _customVariablesBool[i].BoolVariable = variableBool;
                return;
            }

            Debug.Log($"Didn't find a bool variable with the name {variableName}");
        }
        
        [Command("ChangeIntVar")]
        public void ChangeVariable(string variableName, int variableInt)
        {
            for (int i = 0; i < _customVariablesInt.Count; i++)
            {
                if (!variableName.Equals(_customVariablesInt[i].VariableName)) continue;
                
                _customVariablesInt[i].IntVariable = variableInt;
                return;
            }
            Debug.Log($"Didn't find an int variable with the name {variableName}");

        }
        public CustomVariableBool GetBoolVariableWithName(string varName)
        {
            foreach (CustomVariableBool customVariable in _customVariablesBool.Where(customVariable => varName.Equals(customVariable.VariableName)))
            {
                return customVariable;
            }

            return null;
        }
        
        public CustomVariableInt GetIntVariableWithName(string varName)
        {
            foreach (CustomVariableInt customVariable in _customVariablesInt.Where(customVariable => varName.Equals(customVariable.VariableName)))
            {
                return customVariable;
            }

            return null;
        }
    }
}