using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 5f;

    Rigidbody2D rb;
    float jumpForce = 7f;
    int jumpsRemaining;
    int maxJumps = 2;
    int score;
    bool hasWon = false;
    bool hasLost = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;
        score = 0;
        scoreText.text = "Score:0/3";
        winText.gameObject.SetActive(false); // Hide win text at the start
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (hasWon && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            RestartLevel();
        }
        if (hasLost && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            RestartLevel();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            LoseGame();
             // RestartLevel();
        }
        if (other.CompareTag("Goal")) // Assuming you need to collect at least 3 items to win
        {
            if (score >= 3)
            {
                WinGame();
            }
            else
            {
                scoreText.text = "Score:" + score + "/3 - Collect more items to win!";
            }
            // You can add code here to load the next level or show a victory screen
        }
        if (other.CompareTag("Enemy"))
        {
            LoseGame();
            // RestartLevel();
        }
        if (other.CompareTag("Collect"))
        {
            Destroy(other.gameObject); // Remove the collectible from the scene
            score++;
            scoreText.text = "Score:" + score + "/3"; // Update the score display
        }
        

    }
    void WinGame()
    {
        hasWon = true;

        Time.timeScale = 0f;

        winText.gameObject.SetActive(true);
        winText.text = "You Win!";

        scoreText.text = "Press Space to restart";
    }
    void LoseGame()
    {
        hasWon = false;
        hasLost = true;

        Time.timeScale = 0f;

        winText.gameObject.SetActive(true);
        winText.text = "You Lose!";

        scoreText.text = "Press Space to restart";
    }
    
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
