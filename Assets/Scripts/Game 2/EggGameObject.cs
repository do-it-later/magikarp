using UnityEngine;
using System.Collections;

public class EggGameObject : MonoBehaviour {

    public string objectName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var newPosition = transform.position;
        newPosition.y -= 0.18f;

        transform.position = newPosition;
	}
}
