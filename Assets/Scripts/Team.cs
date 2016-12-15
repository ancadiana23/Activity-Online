using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : Object
{
	int[] players;
	int score;
	int currentPlayer;
	int numPlayers;
	string name;

	public Team(string name, int numPlayers, int[] players)
	{
		this.name = name;
		this.players = players;
		this.numPlayers = numPlayers;
		this.score = 0;
		this.currentPlayer = 0;
	}

	public void setRound(int score)
	{
		this.score += score;
		this.currentPlayer = (currentPlayer + 1) % this.numPlayers;
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