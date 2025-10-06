using UnityEngine;

public class InputStage : MonoBehaviour
{
    private bool actionExecuted = false; // Flag to ensure the action is executed only once

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = -Input.GetAxisRaw("Horizontal");
        // Check for input (e.g., space key) and if the action hasn't been executed yet
        if (horizontalInput == 0 && !actionExecuted)
        {
            ExecuteAction();
            actionExecuted = true; // Set the flag to true to prevent further execution
        }
    }

    private void ExecuteAction()
    {
        // Your one-time action logic here
        Debug.Log("Action executed!");
    }
}