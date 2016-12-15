using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour 
{
	public Sprite redTeamSelected;
	public Sprite yellowTeamSelected;
	public Sprite greenTeamSelected;
	public Sprite blueTeamSelected;
	public Image teamsDiamondImage;
	public Text currentPlayerText;

	Team[] teams;
	int currentTeam;
	int numTeams;
	List<string> playerNames;

	string[][] words;
	int MIME = 0, TALK = 1, DRAW = 2;

	// Use this for initialization
	void Start () 
	{
		numTeams = 2;
		teams = new Team[2];

		playerNames = new List<string>();
		for (int i = 0; i < 4; ++i)
		{
			playerNames.Add("Player " + (i + 1).ToString());
		}

		
		newRound();
		teams[0] = new Team ("Team 1", 2, new int[] { 1, 3 });
		teams[1] = new Team ("Team 2", 2, new int[] { 2, 4 });
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

	public void newRound()
	{
		currentTeam = (currentTeam + 1) % numTeams;
		// change diamond
		selectTeam(currentTeam);
		// change player name
		currentPlayerText.text = playerNames[teams[currentTeam].CurrentPlayer];
	}

	void selectTeam(int team)
	{
		switch(team)
		{
			case 0:
				teamsDiamondImage.overrideSprite = redTeamSelected;
				break;
			case 1:
				teamsDiamondImage.overrideSprite = yellowTeamSelected;
				break;
			case 2:
				teamsDiamondImage.overrideSprite = greenTeamSelected;
				break;
			case 3:
				teamsDiamondImage.overrideSprite = blueTeamSelected;
				break;
			default:
				break;
		}
	}

}
