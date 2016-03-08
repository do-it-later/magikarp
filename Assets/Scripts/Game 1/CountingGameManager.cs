using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountingGameManager : MonoBehaviour
{
    public float roundDuration;
    public float endGameDuration;
    public int numberOfRounds;
    
    public List<GameObject> dispenserList;
    public List<bool> activeDispenser;

    private int currentRound;

    private List<int> objectsToCount;

    private List<CountingGamePlayer> players;

    void Start()
    {
        currentRound = 0;

        for(int i = 0; i < dispenserList.Count; ++i)
        {
            Dispenser dispenser = dispenserList[i].GetComponent<Dispenser>();

            dispenser.objectName = "Object " + (i + 1).ToString();
            dispenser.topLeft = new Vector2(8, -4.0f);
            dispenser.bottomRight = new Vector2(8, 0.5f);
            dispenser.minSpeed = 3.0f;
            dispenser.maxSpeed = 7.0f;
            dispenser.minTime = 0.5f;
            dispenser.maxTime = 1.5f;
        }

        objectsToCount = new List<int>();

        players = new List<CountingGamePlayer>();
        for(int i = 0; i < 4; ++i)
        {
            players.Add(new CountingGamePlayer());
        }

        StartCoroutine(Countdown.Instance.StartCountdown(Play));
    }

    void Update()
    {
        for(int i = 0; i < 4; ++i)
        {
            if(Input.GetKeyDown(InputHelper.instance.DidControllerButtonGetPressed(i + 1, InputHelper.Button.A)))
            {
                players[i].counter++;
            }
        }
    }

    IEnumerator Play()
    {
        // reset the players counters
        foreach(CountingGamePlayer player in players)
        {
            player.counter = 0;
        }

        // pick the dispensers to count based on the round number
        objectsToCount.Clear();
        int count = 0;
        while(count <= currentRound)
        {
            int choice = Random.Range(0, dispenserList.Count);

            if(!objectsToCount.Contains(choice))
            {
                objectsToCount.Add(choice);
                ++count;
            }
        }

        // reset the dispenser counters and start them
        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().ClearCount();
            dispenser.GetComponent<Dispenser>().StartDispenser();
        }

        yield return new WaitForSeconds(roundDuration);

        // stop all of the dispensers
        foreach(GameObject dispenser in dispenserList)
        {
            dispenser.GetComponent<Dispenser>().StopDispenser();
        }

        // display the dispenser counts
        int dispenserTotal = 0;
        foreach(int i in objectsToCount)
        {
            dispenserTotal += dispenserList[i].GetComponent<Dispenser>().GetCount();
        }
        Debug.Log("Total count: " + dispenserTotal);

        currentRound++;

        yield return new WaitForSeconds(endGameDuration);

        // check which player got the correct amount
        for(int i = 0; i < 4; ++i)
        {
            if(players[i].counter == dispenserTotal)
            {
                players[i].roundsWon++;
            }
            Debug.Log("Player " + i + ": " + players[i].counter);
        }

        // if there are still rounds remaining then start the countdown for the next round
        if(currentRound < numberOfRounds)
        {
            StartCoroutine(Countdown.Instance.StartCountdown(Play));
        }
        else
        {
            // handle the player with the highest score
        }
    }
}
