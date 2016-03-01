using UnityEngine;
using System.Collections;

public class Player
{
    private int controllerNumber;
    public int ControllerNumber { get { return controllerNumber; } }

    private int score;
    public int Score { get { return score;  } }

    private bool isHuman;
    public bool IsHuman
    {
        get { return isHuman; }
        set { isHuman = value; }
    }

    public Player(int controller)
    {
        controllerNumber = controller;
        score = 0;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void DecreaseScore()
    {
        score--;
    }
}
