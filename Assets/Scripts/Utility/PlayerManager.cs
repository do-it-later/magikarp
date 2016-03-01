using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private List<Player> playerList = new List<Player>();
    public bool debugMode;

    void Awake()
    {
        // Persistent singleton
        if( instance == null )
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying duplicate PlayerManager.");
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        // Add the 4 players
        for (int i = 1; i <= 4; ++i)
        {
            Player p = new Player(i);
            playerList.Add(p);
        }

        if(debugMode)
        {
            SetHumanPlayer(1);
        }
	}
	
	public bool SetHumanPlayer(int controller)
    {
        var player = FindPlayer(controller);

        // Only the first 4 controllers should be found
        if (FindPlayer(controller) == null)
            return false;

        player.IsHuman = true;
        return true;
    }

    public bool UnsetHumanPlayer(int controller)
    {
        var player = FindPlayer(controller);

        // Only the first 4 controllers should be found
        if (FindPlayer(controller) == null)
            return false;

        player.IsHuman = false;
        return true;
    }

    public Player FindPlayer(int controller)
    {
        foreach (Player p in playerList)
        {
            if (p.ControllerNumber == controller)
            {
                return p;
            }
        }

        return null;
    }
}
