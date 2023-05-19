using Novel;

namespace CSV
{
    public static class CsvValues
    {
        public const int CHARACTER_NAME = 0;
        public const int TEXT = 1;
        public const int POSITION = 2;
        public const int SPRITE_NAME = 3;

        public const string CENTER_NAME = "Center";
        public const string LEFT_NAME = "Left";
        public const string RIGHT_NAME = "Right";

        public static DialoguePosition GetPosition(string positionName)
        {
            return positionName switch
            {
                CENTER_NAME => DialoguePosition.Center,
                RIGHT_NAME => DialoguePosition.Right,
                LEFT_NAME => DialoguePosition.Left,
                _ => DialoguePosition.Center
            };
        }
    }
}