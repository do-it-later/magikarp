using UnityEngine;
using System.Collections;

public class CountingGamePlayer
{
    public int playerNumber;
    public int counter;
    public int roundsWon;

    public CountingGamePlayer()
    {
        counter = 0;
        roundsWon = 0;
    }

    public virtual void Update()
    {
        if(Input.GetKeyDown(InputHelper.instance.DidControllerButtonGetPressed(playerNumber + 1, InputHelper.Button.A)))
        {
            counter++;
        }
    }

    public virtual void StartNewRound()
    {
        counter = 0;
    }
}
