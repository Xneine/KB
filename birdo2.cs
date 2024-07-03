// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class birdo2 : MonoBehaviour
// {
//     private QLearningAgent agent;
//     private GameState previousState;
//     private Action previousAction;

//     public string qTableFilePath = "qtable.dat"; // Path untuk menyimpan Q-table
//     public Rigidbody2D rb;
//     public float jumpForce;
//     public float highJumpForce;
//     public float gravityIncrease;

//     void Start()
//     {
//         agent = new QLearningAgent();
//         agent.LoadQTable(qTableFilePath); // Muat Q-table jika ada
//     }

//     void Update()
//     {
//         if (IsGameOver())
//         {
//             // Beri reward negatif besar untuk game over
//             float reward = -100f;

//             // Update Q-table dengan reward negatif
//             if (previousState != null)
//             {
//                 agent.UpdateQValue(previousState, previousAction, reward, null);
//             }

//             // Simpan Q-table
//             agent.SaveQTable(qTableFilePath);

//             // Reset permainan
//             ResetGame();
//         }
//         else
//         {
//             GameState currentState = GetCurrentState();
//             Action action = agent.GetBestAction(currentState);

//             // Lakukan aksi berdasarkan nilai Q terbaik
//             switch (action)
//             {
//                 case Action.Jump:
//                     Jump();
//                     break;
//                 case Action.HighJump:
//                     HighJump();
//                     break;
//                 case Action.IncreaseGravity:
//                     IncreaseGravity();
//                     break;
//             }

//             // Dapatkan reward berdasarkan status game
//             float reward = GetReward();

//             // Update nilai Q
//             if (previousState != null)
//             {
//                 agent.UpdateQValue(previousState, previousAction, reward, currentState);
//             }

//             previousState = currentState;
//             previousAction = action;
//         }
//     }

//     private GameState GetCurrentState()
//     {
//         float birdY = transform.position.y;
//         float birdVelocity = rb.velocity.y;
//         float nextObstacleX; 
//         float nextObstacleGapY; 

//         return new GameState(birdY, birdVelocity, nextObstacleX, nextObstacleGapY);
//     }

//     private float GetReward()
//     {
//         // Implementasi logika reward
//         // Misalnya, reward +1 untuk setiap frame bertahan hidup
//         return 1.0f;
//     }

//     private void Jump()
//     {
//         rb.velocity = Vector2.up * jumpForce;
//     }

//     private void HighJump()
//     {
//         rb.velocity = Vector2.up * highJumpForce;
//     }

//     private void IncreaseGravity()
//     {
//         rb.gravityScale += gravityIncrease;
//     }

//     private bool IsGameOver()
//     {
//         return true;
//         // Implementasi logika untuk mengecek apakah game over
//     }

//     private void ResetGame()
//     {
//         // Implementasi logika untuk mereset game ke kondisi awal
//     }
// }
