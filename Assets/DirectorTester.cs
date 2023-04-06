using System.Collections;
using System.Collections.Generic;
using Director;
using Sirenix.OdinInspector;
using UnityEngine;

public class DirectorTester : MonoBehaviour
{
    private DialogueQueueController _controller;
    
    void Start()
    {
        _controller = FindObjectOfType<DialogueQueueController>();
    }

    [Button]
    public void StartDialogue()
    {
        _controller.AdvanceQueue();
    }
}
