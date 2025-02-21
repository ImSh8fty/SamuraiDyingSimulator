using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float destroyTime = 5f; // Destroy bullet after X seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody component
        rb.linearVelocity = Vector2.left * speed; // Move bullet to the left

        Destroy(gameObject, destroyTime); // Destroy bullet after some time
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // If bullet hits Player
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Reduce player's health by 1 heart
            }

            GetComponent<Collider2D>().enabled = false; // Disable collider to prevent multiple hits
            Destroy(gameObject); // Destroy bullet on hit
        }
        else if (other.CompareTag("Wall")) // Destroy bullet if it hits a wall
        {
            Destroy(gameObject);
        }
    }
}
