using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlowMo()
    {
        // Slow down time to 0.5x speed
        Time.timeScale = 0.5f;
    }
}
