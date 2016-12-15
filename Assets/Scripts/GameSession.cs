using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour 
{
	Team[] teams;
	int currentTeam;
	int numTeams;
	string[][] words;
	int MIME = 0, TALK = 1, DRAW = 2;

	// Use this for initialization
	void Start () 
	{
		numTeams = 2;
		teams = new Team[2];
		teams[0] = new Team ("Team 1", new int[] { 1, 3 });
		teams[1] = new Team ("Team 2", new int[] { 2, 4 });
		words = new string[][] {{"Mittens", "Bowl Haircut", "Dracula", 
								 "Dirty Dancing", "Mermaid", "Collection"},
			     				{"Dora the Explorer", "Silence of the Lambs", "Sombre",
								 "Recursion", "Ada Lovelace", "Inception"}, 
								{"Couch Potato", "Studio Apartment", "Bowl Haircut", 
								 "Recursion", "To infinity and beyond", "Charades"}};
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
