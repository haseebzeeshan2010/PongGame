using UnityEngine;
using TMPro;
using System;
public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro opponentScoreText;
    [SerializeField] private bool isPlayerScore;
    private int currentScore = 0;

    public static event Action<int> OnOpponentScoreChanged;
    public static event Action<int> OnPlayerScoreChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = int.Parse(opponentScoreText.text);
        OnPlayerScoreChanged?.Invoke(currentScore);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncrementOpponentScore()
    {
        currentScore++;
        opponentScoreText.text = currentScore.ToString();

        if (isPlayerScore)
        {
            // This is the player's score text, so we invoke the player score event
            OnPlayerScoreChanged?.Invoke(currentScore);
        }
        else
        {
            // This is the opponent's score text, so we invoke the opponent score event
            OnOpponentScoreChanged?.Invoke(currentScore);
        }
    }
}
