using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the bullet prefab in Inspector
    public Transform firePoint; // Assign FirePoint (bullet spawn position)
    public float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f;

    private Animator animator; // Reference to Animator
    public float bulletDelay = 0.2f; // Delay before the bullet fires

    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        if (Time.time >= nextFireTime) // Fire repeatedly based on fireRate
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set the next fire time
        }
    }

    void Shoot()
    {
        animator.SetTrigger("Shoot"); // Play shooting animation
        Invoke("SpawnBullet", bulletDelay); // Wait, then spawn bullet
    }

    void SpawnBullet()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Spawn bullet from FirePoint
    }
}
