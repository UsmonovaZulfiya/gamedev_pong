using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallController : MonoBehaviour
{
   public float initialSpeed = 10f;
    public float speedIncrease = 0.2f;
    public Text playerText;
    public Text opponentText;
    public Text winText;
    private int hitCounter;
    private Rigidbody2D rb;

    // Max score to win (default 5, will be updated from settings)
    private int maxScore;
    
    void Start()
    {
        // Load the ball speed and max score from PlayerPrefs
        initialSpeed = PlayerPrefs.GetFloat("BallSpeed", 10f); // Default speed is 10
        maxScore = PlayerPrefs.GetInt("MaxScore", 5);         // Default max score is 5

        rb = GetComponent<Rigidbody2D>();
        winText.text = "";  // Clear the win text initially
        Invoke("StartBall", 2f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void PlayerBounce(Transform obj)
    {
        hitCounter++;

        Vector2 ballPosition = transform.position;
        Vector2 playerPosition = obj.position;

        float xDirection;
        float yDirection;

        if (transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        yDirection = (ballPosition.y - playerPosition.y) / obj.GetComponent<Collider2D>().bounds.size.y;

        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }

        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "PaddleA" || other.gameObject.name == "PaddleB")
        {
            PlayerBounce(other.transform);
        }
    }

    private void RestartBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.position.x > 0)
        {
            UpdateScore(playerText);  // Player A scores
        }
        else if (transform.position.x < 0)
        {
            UpdateScore(opponentText);  // Player B scores
        }
    }

    private void UpdateScore(Text scoreText)
    {
        int currentScore = int.Parse(scoreText.text);
        currentScore++;
        scoreText.text = currentScore.ToString();

        // Check if a player has won
        if (currentScore >= maxScore)
        {
            GameOver(scoreText == playerText ? "Player A Wins!" : "Player B Wins!");
        }
        else
        {
            RestartBall();
        }
    }

    private void GameOver(string winnerMessage)
    {
        // Stop the game and display the winner message
        rb.velocity = Vector2.zero;
        winText.text = winnerMessage;  // Set the winner text
        // Disable the ball or paddles to stop further movement
        this.enabled = false; // Disable this script to stop the game
    }
}
