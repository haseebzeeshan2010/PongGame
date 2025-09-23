using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshPro opponentScoreText;
    private int currentScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScore = int.Parse(opponentScoreText.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncrementOpponentScore()
    {
        currentScore++;
        opponentScoreText.text = currentScore.ToString();
    }
}
