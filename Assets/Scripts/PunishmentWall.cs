using UnityEngine;

public class PunishmentWall : MonoBehaviour
{
    [SerializeField] private PongAgent punishedAgent;
    [SerializeField] private PongAgent rewardedAgent;
    [SerializeField] private ScoreText scoreText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Ball>(out Ball ball))
        {
            // Reward the other agent
            if (rewardedAgent != null)
            {
                rewardedAgent.AddReward(5.0f);
            }

            // Punish this agent
            if (punishedAgent != null)
            {
                punishedAgent.Punish();
            }

            // Increment opponent score
            if (scoreText != null)
            {
                scoreText.IncrementOpponentScore();
            }
        }
    }

    private void OnDisable()
    {
        // Clear references when the object is disabled (e.g., on scene reload)
        punishedAgent = null;
        rewardedAgent = null;
        scoreText = null;
    }
}