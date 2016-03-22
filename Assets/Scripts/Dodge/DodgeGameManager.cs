using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DodgeGameManager : MonoBehaviour
{
    public GameObject asteroidDispenser;

    public List<GameObject> players;

    void Start()
    {
        Dispenser dispenser = asteroidDispenser.GetComponent<AsteroidDispenser>();

        dispenser.objectName = "Asteroid";
        dispenser.topLeft = new Vector2(-7, 7.0f);
        dispenser.bottomRight = new Vector2(8, 7.0f);
        dispenser.minTime = 0.05f;
        dispenser.maxTime = 0.2f;

        for(int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<Spaceship>().playerNumber = i + 1;
        }

        StartCoroutine(Countdown.instance.StartCountdown(Play));
    }

    void Update()
    {
        int deadCount = 0;
        foreach(GameObject player in players)
        {
            if(player.GetComponent<Spaceship>().IsDead())
            {
                ++deadCount;
            }
        }
        if(deadCount == players.Count)
        {
            StopCoroutine(Play());
            asteroidDispenser.GetComponent<AsteroidDispenser>().StopDispenser();
        }
    }

    IEnumerator Play()
    {
        asteroidDispenser.GetComponent<AsteroidDispenser>().StartDispenser();

        yield return new WaitForSeconds(10.0f);

        asteroidDispenser.GetComponent<AsteroidDispenser>().StopDispenser();
    }
}
