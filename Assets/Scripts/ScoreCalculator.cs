using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{

    public Text scoreText;
    public Text pointText;

    private int points;
    
    // Define start points
    void Start()
    {
        points = 0;
        UpdatePoints();
    }

    // Set the text variable in the game with current score value
    void UpdatePoints()
    {
        pointText.text = points.ToString();
    }

    // Add points based on how much a coin is worth & update the counter
    public void addPoints(int newPoints)
    {
        points += newPoints;
        UpdatePoints();
    }
}
