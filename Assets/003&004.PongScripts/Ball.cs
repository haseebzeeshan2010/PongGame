using System.Text.RegularExpressions;
using Unity.MLAgents.SideChannels;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private float bounceMultiplier = 1.1f;

    [SerializeField] private float maxSpeed = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
