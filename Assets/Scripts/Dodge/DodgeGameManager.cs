using UnityEngine;
using System.Collections;

public class DodgeGameManager : MonoBehaviour
{
    public GameObject asteroidDispenser;

    void Start()
    {
        Dispenser dispenser = asteroidDispenser.GetComponent<AsteroidDispenser>();

        dispenser.objectName = "Asteroid";
        dispenser.topLeft = new Vector2(-7, 7.0f);
        dispenser.bottomRight = new Vector2(8, 7.0f);
        dispenser.minTime = 0.1f;
        dispenser.maxTime = 0.5f;

        StartCoroutine(Countdown.instance.StartCountdown(Play));
    }

    IEnumerator Play()
    {
        asteroidDispenser.GetComponent<AsteroidDispenser>().StartDispenser();

        yield return new WaitForSeconds(10.0f);

        asteroidDispenser.GetComponent<AsteroidDispenser>().StopDispenser();
    }
}
