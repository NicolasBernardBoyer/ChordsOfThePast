using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    int healthScore = 100;
    int wealthScore = 100;
    int faithScore = 100;
    Boolean goodEnding = false;


    //getters for all scores
    public int getHealthScore()
    {
        return healthScore;
    }

    public int getWealthScore()
    {
        return wealthScore;
    }

    public int getFaithScore()
    {
        return faithScore;
    }

    //setters for all scores
    public void setHealthScore(int healthScore)
    {
        this.healthScore = healthScore;
    }

    public void setWealthScore(int wealthScore)
    {
        this.wealthScore = wealthScore;
    }

    public void setFaithScore(int faithScore)
    {
        this.faithScore = faithScore;
    }

    public Boolean doWeGetGoodEnding(int healthScore, int wealthScore, int faithScore)
    {
        if ((healthScore < 0) && (wealthScore < 0) && (faithScore < 0))
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
    }

    public int resultsSimon(int amountOfFails, int simonTimerTime)
    {
        this.healthScore = 100 / amountOfFails;
        this.wealthScore = 100 / simonTimerTime;
        return healthScore;
    }

    public int resultsChooseHarp(int chosenHarpMultiplier, int amountOfChecks, int chooseHarpTimerTime)
    {
        this.healthScore = 100 * chosenHarpMultiplier;
        this.wealthScore = 100 / chooseHarpTimerTime;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doWeGetGoodEnding(healthScore, wealthScore, faithScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
