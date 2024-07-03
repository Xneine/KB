using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdo : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    // public CircleCollider2D myCircle;
    public float flapStrength;
    public logicScript logic;
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        gameObject.name = "xneine";
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -36.5 || transform.position.y < -15 || transform.position.y > 15){
            isDead();
        }
        if (Input.GetKeyDown(KeyCode.Space) && isAlive) { 
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
        else if (Input.GetKey(KeyCode.J) && isAlive) { 
            myRigidbody.gravityScale = 5;
            Debug.Log("Fall");
        }
        else if (Input.GetKeyDown(KeyCode.K) && isAlive) { 
            myRigidbody.velocity = Vector2.up * flapStrength * 2;
        }
        else{
            myRigidbody.gravityScale = 2;
        }
        // if (Input.GetKey(KeyCode.H) && isAlive)
        // {
        //     myCircle.enabled = false;
        // }
        // else
        // {
        //     myCircle.enabled = true;
        // }
        }
    private void OnCollisionEnter2D(Collision2D collision){
        isDead();
    }
    private void isDead(){
        logic.gameOver();
        isAlive = false;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.MLAgents;
// using Unity.MLAgents.Actuators;
// using Unity.MLAgents.Sensors;

// public class BirdAgent : Agent
// {
//     public Rigidbody2D myRigidbody;
//     public float flapStrength;
//     public logicScript logic;
//     public bool isAlive = true;

//     public override void Initialize()
//     {
//         logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
//         myRigidbody = GetComponent<Rigidbody2D>();
//         gameObject.name = "xneine";
//     }

//     public override void OnEpisodeBegin()
//     {
//         if (!isAlive)
//         {
//             logic.restart();
//         }
//         myRigidbody.velocity = Vector2.zero;
//         transform.position = new Vector3(-5, 0, 0);
//         isAlive = true;
//     }

//     public override void CollectObservations(VectorSensor sensor)
//     {
//         sensor.AddObservation(transform.position.y);
//         sensor.AddObservation(myRigidbody.velocity.y);
//         // Tambahkan observasi lingkungan seperti jarak ke pipa terdekat jika diperlukan
//     }

//     public override void OnActionReceived(ActionBuffers actions)
//     {
//         if (actions.DiscreteActions[0] == 1 && isAlive)
//         {
//             myRigidbody.velocity = Vector2.up * flapStrength;
//         }

//         // Reward dan penalti bisa diatur di sini
//         if (transform.position.y < -15 || transform.position.y > 15)
//         {
//             AddReward(-1f);
//             EndEpisode();
//         }
//     }

//     public override void Heuristic(in ActionBuffers actionsOut)
//     {
//         var discreteActionsOut = actionsOut.DiscreteActions;
//         discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (isAlive)
//         {
//             AddReward(-1f);
//             isAlive = false;
//             EndEpisode();
//         }
//     }
// }
