using System.Collections;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public static event System.Action TaskComplete;


    public void CompleteTask()
    {
        Debug.Log("Task completed!");

        TaskComplete?.Invoke();
    }
}
