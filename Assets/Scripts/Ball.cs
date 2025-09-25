using System.Text.RegularExpressions;
using Unity.MLAgents.SideChannels;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float bounceMultiplier = 1.1f;

    [SerializeField] private bool playerEnabled = true;
    [SerializeField] private float launchForce = 15f;

    [SerializeField] private float maxSpeed = 30f;

    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioClip paddleHitSound;
    [SerializeField] private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartMatch()
    {

        // targetRigidbody.linearVelocity = new Vector3(Random.Range(-launchForce, launchForce), 0, launchForce);

        if (playerEnabled)
        {
            ballRigidbody.linearVelocity = new Vector3(Random.Range(-launchForce, launchForce), 0, -launchForce);
            transform.localPosition = new Vector3(0, 0f, 12.25f);
        }
        else
        {
            // Set a random direction in the horizontal (x-z) plane with a fixed magnitude (launchForce)
            float angle = Random.Range(0f, 2f * Mathf.PI);
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            ballRigidbody.linearVelocity = direction * launchForce;
            transform.localPosition = new Vector3(Random.Range(-7f, 7f), 0f, 12.25f);
        }
        transform.localPosition = new Vector3(0f, 0f, transform.localPosition.z);

    }

    public void ResetBall()
    {
        Debug.Log("Resetting Ball");
        ballRigidbody.linearVelocity = Vector3.zero;
        transform.localPosition = new Vector3(0f, 0f, 12.25f);
    }

    // Update is called once per frame
    void Update()
    {
        // Cap the ball's speed to prevent it from going too fast
        if (ballRigidbody.linearVelocity.magnitude > maxSpeed)
        {
            ballRigidbody.linearVelocity = ballRigidbody.linearVelocity.normalized * maxSpeed;
        }

        // Ensure the ball maintains a minimum speed in the z direction
        if (Mathf.Abs(ballRigidbody.linearVelocity.z) < 3f)
        {
            Vector3 currentVelocity = ballRigidbody.linearVelocity;
            currentVelocity.z *= 1.2f; // Multiply the z velocity by 1.2
            ballRigidbody.linearVelocity = currentVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collisions with paddles
        if (collision.gameObject.TryGetComponent<PongAgent>(out PongAgent pongAgent))
        {
            audioSource.clip = paddleHitSound;
            audioSource.Play();
            // Increase the ball's speed when hitting a paddle
            ballRigidbody.linearVelocity *= bounceMultiplier;

            // Ensure the ball maintains a minimum speed in the z direction
            if (Mathf.Abs(ballRigidbody.linearVelocity.z) < 15f)
            {
                Vector3 currentVelocity = ballRigidbody.linearVelocity;
                currentVelocity.z *= 1.2f; // Multiply the z velocity by 1.2
                ballRigidbody.linearVelocity = currentVelocity;
            }
        }
        else
        {
            audioSource.clip = wallHitSound;
            audioSource.Play();
        }

        // Handle collisions with bounce walls
        if (collision.gameObject.TryGetComponent<BounceWall>(out BounceWall bounceWall) && Mathf.Abs(ballRigidbody.linearVelocity.z) < 10f)
        {
            ballRigidbody.AddForce(new Vector3(0, 0, -5f));
        }


        // Add a small random force when hitting the side walls to prevent vertical bounces
        if (collision.gameObject.TryGetComponent<BounceWall>(out BounceWall bounce))
        {
            ballRigidbody.AddForce(new Vector3(Random.Range(-3f, 3f), 0, 0));
        }
    }
}
