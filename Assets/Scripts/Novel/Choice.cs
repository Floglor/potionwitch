using System;

namespace Novel
{
    [Serializable]
    public class Choice
    {
        public string Text;
        public Dialogue NextDialogue;
    }    
}