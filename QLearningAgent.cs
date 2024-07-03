using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class QLearningAgent
{
    private Dictionary<GameState, Dictionary<Action, float>> qTable;
    private float learningRate = 0.1f;
    private float discountFactor = 0.99f;

    public QLearningAgent()
    {
        qTable = new Dictionary<GameState, Dictionary<Action, float>>();
    }

    public void UpdateQValue(GameState state, Action action, float reward, GameState nextState)
    {
        if (!qTable.ContainsKey(state))
        {
            qTable[state] = new Dictionary<Action, float>
            {
                { Action.Jump, 0 },
                { Action.HighJump, 0 },
                { Action.IncreaseGravity, 0 }
            };
        }

        float oldQValue = qTable[state][action];
        float maxNextQValue = GetMaxQValue(nextState);
        float newQValue = oldQValue + learningRate * (reward + discountFactor * maxNextQValue - oldQValue);
        qTable[state][action] = newQValue;
    }

    private float GetMaxQValue(GameState state)
    {
        if (!qTable.ContainsKey(state)) return 0;
        return Mathf.Max(qTable[state][Action.Jump], qTable[state][Action.HighJump], qTable[state][Action.IncreaseGravity]);
    }

    public Action GetBestAction(GameState state)
    {
        if (!qTable.ContainsKey(state))
        {
            return Action.Jump; // Default action
        }
        Dictionary<Action, float> actions = qTable[state];
        Action bestAction = Action.Jump;
        float maxQValue = float.MinValue;
        foreach (var action in actions)
        {
            if (action.Value > maxQValue)
            {
                bestAction = action.Key;
                maxQValue = action.Value;
            }
        }
        return bestAction;
    }

    public void SaveQTable(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, qTable);
        }
    }

    public void LoadQTable(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                qTable = (Dictionary<GameState, Dictionary<Action, float>>)formatter.Deserialize(fs);
            }
        }
    }
}

public enum Action
{
    Jump,
    HighJump,
    IncreaseGravity
}

[System.Serializable]
public class GameState
{
    public float BirdY { get; set; }
    public float BirdVelocity { get; set; }
    public float NextObstacleX { get; set; }
    public float NextObstacleGapY { get; set; }

    public GameState(float birdY, float birdVelocity, float nextObstacleX, float nextObstacleGapY)
    {
        BirdY = birdY;
        BirdVelocity = birdVelocity;
        NextObstacleX = nextObstacleX;
        NextObstacleGapY = nextObstacleGapY;
    }

    public override bool Equals(object obj)
    {
        if (obj is GameState other)
        {
            return BirdY == other.BirdY && BirdVelocity == other.BirdVelocity &&
                   NextObstacleX == other.NextObstacleX && NextObstacleGapY == other.NextObstacleGapY;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (BirdY, BirdVelocity, NextObstacleX, NextObstacleGapY).GetHashCode();
    }
}
