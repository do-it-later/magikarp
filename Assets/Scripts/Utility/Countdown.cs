using UnityEngine;
using System.Collections;

public class Countdown : Singleton<Countdown>
{
    public delegate IEnumerator PlayDelegate();

    public IEnumerator StartCountdown(PlayDelegate func)
    {
        // TODO: Show the countdown text from here
        Debug.Log("3");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("GO");

        StartCoroutine(func());
    }
}
