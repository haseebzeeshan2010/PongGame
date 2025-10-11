using UnityEngine;
using TMPro;
using System.Collections;
public class TutorialGameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;

    [SerializeField] private TextMeshProUGUI TimerText;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip readySound;
    [SerializeField] private AudioClip goSound;

    [SerializeField] private AudioSource winloseSound;

    [SerializeField] private int winningScore = 10;

    [SerializeField] private GameObject playerWin;
    [SerializeField] private GameObject opponentWin;

    [SerializeField] private FadeUI restartUI;
    [SerializeField] private FadeUI continueUI;

    public TaskManager TaskManager; // Reference to the TaskManager to notify when the action is complete

    private int playerScore = 0;
    private int opponentScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ball.StartMatch();

        // Subscribe to score events
        ScoreText.OnOpponentScoreChanged += HandleOpponentScoreChanged;
        ScoreText.OnPlayerScoreChanged += HandlePlayerScoreChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from score events to avoid lingering references
        ScoreText.OnOpponentScoreChanged -= HandleOpponentScoreChanged;
        ScoreText.OnPlayerScoreChanged -= HandlePlayerScoreChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginRound()
    {
        if (audioSource != null)
        {
            audioSource.clip = readySound;
        }

        ball.ResetBall();
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }

            TimerText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        if (audioSource != null)
        {
            audioSource.clip = goSound;
            audioSource.Play();
        }

        TimerText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        TimerText.text = "";
        ball.StartMatch();
    }

    private void HandleOpponentScoreChanged(int newScore)
    {
        opponentScore = newScore;

        if (winloseSound != null)
        {
            winloseSound.pitch = 0.8f;
            winloseSound.Play();
        }

        CheckWin();
    }

    private void HandlePlayerScoreChanged(int newScore)
    {
        playerScore = newScore;
        CheckWin();

        if (winloseSound != null)
        {
            winloseSound.pitch = 1f;
            winloseSound.Play();
        }
    }

    private void CheckWin()
    {
        if (playerScore >= winningScore)
        {
            Debug.Log("Player Wins!");
            playerWin.SetActive(true);
            continueUI.FadeIn();
            TaskManager?.CompleteTask(); // Notify the TaskManager that the task is complete
            // ResetScores();
            // BeginRound();
        }
        else if (opponentScore >= winningScore)
        {
            Debug.Log("Opponent Wins!");
            opponentWin.SetActive(true);
            restartUI.FadeIn();
            // ResetScores();
            // BeginRound();
        }
        else
        {
            BeginRound();
        }
    }
}
