using UnityEngine;
using UnityEngine.SceneManagement; // Needed to restart the game
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health (number of hearts)
    private int currentHealth; // Current health

    public SpriteRenderer[] heartSprites; // Drag heart GameObjects here
    public Sprite fullHeart;  // Assign full heart sprite
    public Sprite emptyHeart; // Assign empty heart sprite

    private Animator animator; // Reference to Animator
    private bool isInvincible = false; // Prevent multiple hits

    void Start()
    {
        currentHealth = maxHealth; // Set health to full at the start
        animator = GetComponent<Animator>(); // Get the Animator component
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0 || isInvincible) return; // Prevents multiple hits

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Get_Hit")) // Ensure animation doesn't restart
        {
            animator.SetTrigger("Hit"); // Play "Get Hit" animation
        }

        StartCoroutine(InvincibilityFrames()); // Start invincibility

        UpdateHearts();

        if (currentHealth <= 0)
        {
            StartCoroutine(RestartGame()); // Restart the game if health reaches 0
        }
    }

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f); // Time before player can get hit again
        isInvincible = false;
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            if (i < currentHealth)
                heartSprites[i].sprite = fullHeart; // Full heart if health remains
            else
                heartSprites[i].sprite = emptyHeart; // Empty heart if health lost
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f); // Optional delay before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads current scene
    }
}
