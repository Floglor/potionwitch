﻿using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using yutokun;


namespace CSV
{
    public class CsvTester : MonoBehaviour
       {
           public string path;
   
           [Button]
           private void Test()
           {
               List<List<string>> result = CSVParser.LoadFromPath(path);
   
               foreach (List<string> list in result)
               {
                   foreach (string s in list)
                   {
                       Debug.Log(s);
                   }
               }
           }
       }
}