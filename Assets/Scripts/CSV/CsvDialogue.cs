using System.Collections.Generic;
using Misc;
using Novel;
using Sirenix.OdinInspector;
using UnityEngine;
using yutokun;

namespace CSV
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "CSV Dialogue")]
    public class CsvDialogue : Dialogue
    {
        [SerializeField] private TextAsset _textAsset;

        [Button(ButtonSizes.Large)]
        private void FillUpFromTextAsset()
        {
            List<List<string>> linesList = CSVParser.LoadFromString(_textAsset.ToString());
            
            List<Line> lines = ConvertToLines(linesList);
            Lines = lines;
        }

        private List<Line> ConvertToLines(List<List<string>> linesList)
        {
            List<Line> resultLines = new List<Line>();
            //line 0 is for variable names in the chart
            for (int i = 1; i < linesList.Count; i++)
            {
                List<string> strLine = linesList[i];

                Line line = new Line();
                line.TargetCharacter = FindCharacter(strLine[CsvValues.CHARACTER_NAME]);
                line.Text = strLine[CsvValues.TEXT];
                line.CharacterSprite = FindSprite(strLine[CsvValues.SPRITE_NAME]);
                line.Position = CsvValues.GetPosition(strLine[CsvValues.POSITION]);
                
                resultLines.Add(line);
            }

            return resultLines;
            
        }

        private Sprite FindSprite(string spriteName)
        {
            return null;
        }

        private Character FindCharacter(string characterName)
        {
            return CharacterFinder.FindScriptableObjectByName(characterName);
        }
    }
}