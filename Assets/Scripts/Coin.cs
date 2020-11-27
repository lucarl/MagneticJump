using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject scoreCalc;

    public int score;
    public AudioSource coinSound;
    
    private void Update()
    {
        //Make coin spin around itself
       transform.Rotate(0.0f, 0.8f, 0.0f, Space.Self);
    }
    
    //Add play sound, add points and disable coin on player collision 
    private void OnCollisionEnter(Collision other)
    {
        PlayCoinSound();
        DisableCoin();
        AddPoints();
    }

    private void PlayCoinSound()
    {
        coinSound.mute = false;
        coinSound.Play(0);
    }
    
    private void DisableCoin()
    {
        transform.gameObject.SetActiveRecursively(false);
    }

    private void AddPoints()
    {
        scoreCalc.GetComponent<ScoreCalculator>().addPoints(score);
    }
}
