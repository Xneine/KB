using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BirdAgent : Agent
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public logicScript logic;
    public bool isAlive = true;

    public override void Initialize()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<logicScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the environment and the agent
        transform.position = new Vector3(0, 0, 0);
        myRigidbody.velocity = Vector2.zero;
        logic.playerScore = 0;
        logic.scoreText.text = logic.playerScore.ToString();
        isAlive = true;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations about the agent and the environment
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(myRigidbody.velocity.y);
        // Add more observations if necessary
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!isAlive) return;

        if (actions.DiscreteActions[0] == 1)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
        else if (actions.DiscreteActions[0] == 2)
        {
            myRigidbody.gravityScale = 5;
        }
        else if (actions.DiscreteActions[0] == 3)
        {
            myRigidbody.velocity = Vector2.up * flapStrength * 2;
        }
        else
        {
            myRigidbody.gravityScale = 2;
        }

        // Rewards and penalties
        if (transform.position.x < -36.5 || transform.position.y < -15 || transform.position.y > 15)
        {
            SetReward(-1f);
            EndEpisode();
        }

        // Add more rewards or penalties as necessary
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Define heuristic actions for testing without training
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            discreteActionsOut[0] = 2;
        }
        else if (Input.GetKey(KeyCode.K))
        {
            discreteActionsOut[0] = 3;
        }
        else
        {
            discreteActionsOut[0] = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive)
        {
            SetReward(-1f);
            isAlive = false;
            EndEpisode();
        }
    }
}
