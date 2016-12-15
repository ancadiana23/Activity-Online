using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour 
{
	Team[] teams;
	int currentTeam;
	int numTeams;

	// Use this for initialization
	void Start () 
	{
		numTeams = 2;
		teams = new Team[2];
		teams[0] = new Team ("Team 1", new Int32[] { 1, 3 });
		teams[1] = new Team ("Team 2", new Int32[] { 2, 4 });
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void newRound()
	{
		currentTeam = (currentTeam + 1) % numTeams;
		// TODO change dimond
		// TODO change player name

	}

}
