// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public float speed = 10f;
//     public bool isPlayerA = false;  // Use this to distinguish Paddle A and B
//     private Rigidbody2D rb;
//     private Vector2 playerMovement;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(isPlayerA){
//             PaddleAController(); // Controls for Player A (Arrow keys)
//         }
//         else{
//             PaddleBController(); // Controls for Player B (W and S keys)
//         }
//     }

//     // Controls for Paddle A (Up and Down arrow keys)
//     private void PaddleAController(){
//         float verticalInput = 0;

//         // Check for Player A input (Arrow keys)
//         if(Input.GetKey(KeyCode.UpArrow)){
//             verticalInput = 1;  // Move up
//         }else if(Input.GetKey(KeyCode.DownArrow)){
//             verticalInput = -1; // Move down
//         }

//         playerMovement = new Vector2(0, verticalInput);
//     }

//     // Controls for Paddle B (W and S keys)
//     private void PaddleBController(){
//         float verticalInput = 0;

//         // Check for Player B input (W and S keys)
//         if(Input.GetKey(KeyCode.W)){
//             verticalInput = 1;  // Move up
//         }else if(Input.GetKey(KeyCode.S)){
//             verticalInput = -1; // Move down
//         }

//         playerMovement = new Vector2(0, verticalInput);
//     }

//     // private void PaddleBController(){
//     //     if(circle.transform.position.y > transform.position.y + 0.5f){
//     //         playerMovement = new Vector2(0, 1);
//     //     }else if(circle.transform.position.y < transform.position.y - 0.5f){
//     //         playerMovement = new Vector2(0, -1);
//     //     }else {
//     //         playerMovement = new Vector2(0, 0);
//     //     }
//     // }

//     // Apply the movement to the paddle
//     private void FixedUpdate(){
//         rb.velocity = playerMovement * speed;
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerA = false;  // Use this to distinguish Paddle A and B
    public GameObject circle;
    private Rigidbody2D rb;
    private Vector2 playerMovement;
    private bool isPlayingWithAI = false; // New variable to check if we're playing with AI

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Determine the game mode based on PlayerPrefs
        if (PlayerPrefs.GetInt("GameMode") == 1 && !isPlayerA)
        {
            isPlayingWithAI = true;  // This paddle will be controlled by AI
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerA){
            PaddleAController();  // Controls for Player A (Arrow keys)
        }
        else{
            if (isPlayingWithAI)
            {
                AIController();  // Controls for AI
            }
            else
            {
                PaddleBController();  // Controls for Player B (W and S keys)
            }
        }
    }

    // Controls for Paddle A (Up and Down arrow keys)
    private void PaddleAController(){
        float verticalInput = 0;

        // Check for Player A input (Arrow keys)
        if(Input.GetKey(KeyCode.UpArrow)){
            verticalInput = 1;  // Move up
        }else if(Input.GetKey(KeyCode.DownArrow)){
            verticalInput = -1; // Move down
        }

        playerMovement = new Vector2(0, verticalInput);
    }

    // Controls for Paddle B (W and S keys)
    private void PaddleBController(){
        float verticalInput = 0;

        // Check for Player B input (W and S keys)
        if(Input.GetKey(KeyCode.W)){
            verticalInput = 1;  // Move up
        }else if(Input.GetKey(KeyCode.S)){
            verticalInput = -1; // Move down
        }

        playerMovement = new Vector2(0, verticalInput);
    }

    // AI for Paddle B
    private void AIController(){
        if(circle.transform.position.y > transform.position.y + 0.5f){
            playerMovement = new Vector2(0, 1);  // Move paddle up
        }else if(circle.transform.position.y < transform.position.y - 0.5f){
            playerMovement = new Vector2(0, -1); // Move paddle down
        }else {
            playerMovement = new Vector2(0, 0);  // Stay still
        }
    }

    // Apply the movement to the paddle
    private void FixedUpdate(){
        rb.velocity = playerMovement * speed;
    }
}
