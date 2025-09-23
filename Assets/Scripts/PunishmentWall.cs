using UnityEngine;

public class PunishmentWall : MonoBehaviour
{
    [SerializeField] private PongAgent punishedAgent;

    [SerializeField] private PongAgent rewardedAgent;
    [SerializeField] private ScoreText scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball3) && rewardedAgent != null) // Reward the other agent
        {
            rewardedAgent.AddReward(5.0f);
        }
        
        if (other.TryGetComponent<Ball>(out Ball ball)) // Punish this agent
        {
            punishedAgent.Punish();
        }
        if (other.TryGetComponent<Ball>(out Ball ball2) && scoreText != null) // Increment opponent score
        {
            scoreText.IncrementOpponentScore();
        }
    }
}
