using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountingGameManager : MonoBehaviour
{
    public static CountingGameManager instance;

    public float roundDuration;
    public float endGameDuration;
    public int numberOfRounds;
    
    public List<GameObject> dispenserList;
    public List<bool> activeDispenser;

    public List<int> objectsToCount;

    private int currentRound;

    private List<CountingGamePlayer> players;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying gameObject");
            Destroy(gameObject);
        }
    }

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
            // check if the player is an ai or human
            players.Add(new CountingGameAI());
        }

        StartCoroutine(Countdown.instance.StartCountdown(Play));
    }

    void Update()
    {
        foreach(var player in players)
        {
            player.Update();
        }
    }

    IEnumerator Play()
    {
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

        // reset the players counters
        foreach(CountingGamePlayer player in players)
        {
            player.StartNewRound();
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
            StartCoroutine(Countdown.instance.StartCountdown(Play));
        }
        else
        {
            // handle the player with the highest score
        }
    }
}
