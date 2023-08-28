using Director;
using UnityEngine;
using UnityEngine.UI;

public class GuestNotification : MonoBehaviour
{
    private DialogueQueueController _dialogueQueueController;
    [SerializeField] private GameObject _notification;
    [SerializeField] private GameObject _notifButton;
    
    

    private void Awake()
    {
        _dialogueQueueController = FindObjectOfType<DialogueQueueController>();
        _dialogueQueueController.HasDialogueInQueue += SetNotificationObjects;
        _notifButton.GetComponent<Button>().onClick.AddListener((() =>
        {
            SetNotificationObjects(false);
            _dialogueQueueController.AdvanceQueue();
        }));
        gameObject.SetActive(false);
    }

    private void SetNotificationObjects(bool enable)
    {
        _notification.SetActive(enable);
        _notifButton.SetActive(enable);
    }
}
