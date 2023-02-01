using System;
using System.Collections.Generic;

namespace Novel
{
    [Serializable]
    public class Choice
    {
        public string Text;
        public Dialogue NextDialogue;
    }    
}