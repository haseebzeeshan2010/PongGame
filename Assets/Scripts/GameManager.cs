using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;

    [SerializeField] private TextMeshProUGUI TimerText;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip goSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ball.StartMatch();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginRound()
    {
        audioSource.clip = readySound;
        ball.ResetBall();
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            audioSource.Play();
            TimerText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        audioSource.clip = goSound;
        audioSource.Play();
        TimerText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        TimerText.text = "";
        ball.StartMatch();
    }
    
    
}
