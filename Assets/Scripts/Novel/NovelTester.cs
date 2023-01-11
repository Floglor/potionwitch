using UnityEngine;

namespace Novel
{
    public class NovelTester : MonoBehaviour
    {
        [SerializeField] private Dialogue _testDialogue;
        
        
        private void Start()
        {
            NovelController novelController = FindObjectOfType<NovelController>();
            novelController.StartDialogue(_testDialogue);
        }
        
    }
}
