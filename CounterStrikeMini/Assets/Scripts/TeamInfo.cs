using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamInfo : MonoBehaviour {

    private List<Player> players;
    private string name;

    public string getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    //public List<Player> getPlayers()
    //{
    //    return players;
    //}
}
