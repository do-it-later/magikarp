using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;

    public delegate IEnumerator PlayDelegate();

    void Awake()
    {
        // Persistent singleton
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying duplicate Countdown.");
            Destroy(gameObject);
        }
    }

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
