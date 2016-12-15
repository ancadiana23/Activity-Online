using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
	int[] players;
	int score;
	int currentPlayer;
	string name;

	public Team(string name, int[] players)
	{
		this.name = name;
		this.players = players;
		this.score = 0;
		this.currentPlayer = 0;
	}

	public int CurrentPlayer
	{
		get { return players [currentPlayer];}
	}

	public int Score
	{
		get { return score; }
		set { score = value; }
	}
}
