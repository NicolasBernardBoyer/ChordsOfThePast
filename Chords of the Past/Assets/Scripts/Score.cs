using System;
using UnityEngine;

public class Score : MonoBehaviour //static means can be accessed by all other codes without making it a copy of it in the code. This is a singleton
{
    static int score = 100;
    static Score instance; //singleton :3


    //getters for all scores
    public static int getHealthScore()
    {
        return score;
    }

    /* public int getWealthScore()
     {
        return wealthScore;
    } */

    //public int getFaithScore()
    /*{
        return faithScore;
    }*/

    //setters for all scores
    public static void setScore(int givenScore)
    {
        score = givenScore;
    }

    public static int addScore(int givenScore)
    {
        score += givenScore;
        return score;
    }

    /*public void setWealthScore(int wealthScore)
    {
        this.wealthScore = wealthScore;
    } */

    /*public void setFaithScore(int faithScore)
    {
        this.faithScore = faithScore;
    } */

    /*public static Boolean doWeGetGoodEnding(int healthScore, int wealthScore)
    {
        if ((healthScore < 0) && (wealthScore < 0))
        {
            goodEnding = true;
            Debug.Log("Good Ending!");
            return goodEnding;
        }

        else
        {
            return goodEnding;
            Debug.Log("Bad Ending!");
        }
    } */




    /*public int resultsSimon(int amountOfFails, int simonTimerTime)
    {
        this.healthScore = 100;
        this.wealthScore = 100 / simonTimerTime;
        return healthScore;
    }

    public int resultsChooseHarp(bool chosenHarpRight, int chooseHarpTimerTime)
    {
        if (chosenHarpRight)
        {
            healthScore += 100;
        }
        if (chooseHarpTimerTime >= 180)
        {
            healthScore -= 100;
        }
        return healthScore;
    }

    public int resultsTuningGuitar(int tuningCompletion, int tuningGuitarTimerTime, int amountOfTries)
    {
        this.healthScore = 100 * tuningCompletion;
        this.wealthScore = 100 / tuningGuitarTimerTime;
        return healthScore;
    }

    public int resultsMusicBox(int musicBoxTimerTime,  int musicBoxCompletion)
    {
        this.healthScore = 100 * musicBoxCompletion;
        this.wealthScore = 100 / musicBoxTimerTime;
        return healthScore;
    }
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static void Start()
    {
    
    }

    // Update is called once per frame
    static void Update()
    {
        
    } 
}
