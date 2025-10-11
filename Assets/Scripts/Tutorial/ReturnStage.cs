using Unity.VisualScripting;
using UnityEngine;

public class ReturnStage : MonoBehaviour
{
    public TutorialGameManager tutorialGameManager;
    public TaskManager TaskManager; // Reference to the TaskManager to notify when the action is complete

    private bool actionExecuted = false; // Flag to ensure the action is executed only once

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!actionExecuted)
        {
            ExecuteAction();
            actionExecuted = true; // Set the flag to true to prevent further execution
        }
    }

    private void ExecuteAction()
    {
        // Your one-time action logic here
        Debug.Log("Action executed!");

        TaskManager?.CompleteTask();
        
    }
    
}
