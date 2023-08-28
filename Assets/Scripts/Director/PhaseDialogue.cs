using Novel;

namespace Director
{
    public class PhaseDialogue
    {
        public Dialogue Dialogue;
        public QuestPhase Phase;

        public PhaseDialogue(Dialogue dialogue, QuestPhase phase)
        {
            Dialogue = dialogue;
            Phase = phase;
        }
    }
}