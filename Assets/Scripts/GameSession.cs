using System;
using System.Collections.Generic;
using System.IO;
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
	public GameObject wordCard;
	public GameObject card3;
	public GameObject card4;
	public GameObject card5;

	public GameObject[] actionTiles;
	public GameObject startTile;
	public GameObject finishTile;
	public GameObject[] pawns;
	public Button startRoundButton;

    List<String> words_3_talk, words_3_draw, words_3_char;
    List<String> words_4_talk, words_4_draw, words_4_char;
    List<String> words_5_talk, words_5_draw, words_5_char;

    Team[] teams;
	int currentTeam;
	int numTeams;
	List<string> playerNames;
	int scoreReceived;
	bool wordChosen;
	bool wordGuessed;
	private int action;
	string[,,] words;
	static int MIME = 0, TALK = 1, DRAW = 2;
	int[] board = new int[] {TALK, DRAW, MIME, MIME, DRAW, MIME, DRAW, DRAW, TALK, TALK, MIME, DRAW, MIME, MIME, TALK, DRAW, TALK, TALK };
	// Use this for initialization

    void read_cards()
    {
        var reader_3 = new StreamReader(File.OpenRead(Directory.GetCurrentDirectory() + "\\3.csv"));
        var reader_4 = new StreamReader(File.OpenRead(Directory.GetCurrentDirectory() + "\\4.csv"));
        var reader_5 = new StreamReader(File.OpenRead(Directory.GetCurrentDirectory() + "\\5.csv"));

        words_3_talk = new List<String>();
        words_4_talk = new List<String>();
        words_5_talk = new List<String>();

        words_3_draw = new List<String>();
        words_4_draw = new List<String>();
        words_5_draw = new List<String>();

        words_3_char = new List<String>();
        words_4_char = new List<String>();
        words_5_char = new List<String>();

        while (!reader_3.EndOfStream)
        {
            var line = reader_3.ReadLine();
            var values = line.Split(',');

            words_3_talk.Add(values[0]);
            words_3_draw.Add(values[1]);
            words_3_char.Add(values[2]);
        }

        while (!reader_4.EndOfStream)
        {
            var line = reader_4.ReadLine();
            var values = line.Split(',');

            words_4_talk.Add(values[0]);
            words_4_draw.Add(values[1]);
            words_4_char.Add(values[2]);
        }

        while (!reader_5.EndOfStream)
        {
            var line = reader_5.ReadLine();
            var values = line.Split(',');

            words_5_talk.Add(values[0]);
            words_5_draw.Add(values[1]);
            words_5_char.Add(values[2]);
        }

        //for (int i = 0; i < words_3.Count; ++i)
        //    Debug.Log(words_3[i]);
        reader_3.Close();
        reader_4.Close();
        reader_5.Close();
    }

	public bool WordChosen
	{
		get { return wordChosen; }
	}

	public bool WordGuessed
	{
		get { return wordGuessed; }
	}

	public int Action
	{
		get { return action; }
	}

	void Start () 
	{
		wordChosen = false;
		startRoundButton.interactable = false;
		numTeams = 2;
		teams = new Team[2];
		scoreReceived = 0;
		playerNames = new List<string>();
		action = -1;
		for (int i = 0; i < numTeams; ++i)
		{
			pawns[i].SetActive(true);
		}

		for (int i = 0; i < 4; ++i)
		{
			playerNames.Add("Player " + (i + 1).ToString());
		}

		teams[0] = new Team ("Team 1", 2, new int[] { 0, 2 });
		teams[1] = new Team ("Team 2", 2, new int[] { 1, 3 });


        read_cards();
        /*
		words = new string[, ,] { {  {"Mittens", "Bowl Haircut" },
									{ "Dracula", "Dirty Dancing"},
									{ "Mermaid", "Collection"}
								 },
								 { {"Dora the Explorer", "Silence of the Lambs" },
									{ "Sombre", "Recursion" },
									{ "Ada Lovelace", "Inception"}
								 },
								 { {"Couch Potato", "Studio Apartment" },
									{ "Bowl Haircut", "Recursion" },
									{ "To infinity and beyond", "Charades"}
								 }
								};
                                */
		newRound();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void newRound()
	{
		scoreReceived = 0;
		wordGuessed = false;
		wordChosen = false;
		startRoundButton.interactable = false;
		card3.SetActive(true);
		card4.SetActive(true);
		card5.SetActive(true);
		wordCard.SetActive(false);

		currentTeam = (currentTeam + 1) % numTeams;
		// change diamond
		selectTeam(currentTeam);
		// change player name
		currentPlayerText.text = playerNames[teams[currentTeam].CurrentPlayer];
		teams[currentTeam].setRound(0);
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

	public void OnClickCard3()
	{
		string wordToGuess = ChooseWord(3);
		wordChosen = true;
		startRoundButton.interactable = true;
		scoreReceived = 3;
		ShowWord(wordToGuess);
	}

	public void OnClickCard4()
	{
		string wordToGuess = ChooseWord(4);
		wordChosen = true;
		startRoundButton.interactable = true;
		scoreReceived = 4;
		ShowWord(wordToGuess);
	}

	public void OnClickCard5()
	{
		string wordToGuess = ChooseWord(5);
		wordChosen = true;
		startRoundButton.interactable = true;
		scoreReceived = 5;
		ShowWord(wordToGuess);
	}

	public void OnGuessed()
	{
		wordGuessed = true;
		teams[currentTeam].Score += scoreReceived;
		MovePawn(currentTeam, teams[currentTeam].Score);
	}

	string ChooseWord(int difficulty)
	{
		string wordToGuess = "";
		int teamScore = teams[currentTeam].Score;

		if (teamScore == 0)
		{
			action = UnityEngine.Random.Range(0, 2);
		}
		else
		{
			action = board[teamScore - 1];
		}
        //wordToGuess = words[action, difficulty, UnityEngine.Random.Range(0, 1)];
        int index;

        switch(difficulty)
        {
            case 3:
                switch(action)
                {
                    case 0:
                        index = UnityEngine.Random.Range(0, words_3_talk.Count);
                        wordToGuess = words_3_talk[index];
                        words_3_talk.RemoveAt(index);
                        break;

                    case 1:
                        index = UnityEngine.Random.Range(0, words_3_draw.Count);
                        wordToGuess = words_3_draw[index];
                        words_3_draw.RemoveAt(index);
                        break;

                    case 2:
                        index = UnityEngine.Random.Range(0, words_3_char.Count);
                        wordToGuess = words_3_char[index];
                        words_3_char.RemoveAt(index);
                        break;
                }
                break;

            case 4:
                switch (action)
                {
                    case 0:
                        index = UnityEngine.Random.Range(0, words_4_talk.Count);
                        wordToGuess = words_4_talk[index];
                        words_4_talk.RemoveAt(index);
                        break;

                    case 1:
                        index = UnityEngine.Random.Range(0, words_4_draw.Count);
                        wordToGuess = words_4_draw[index];
                        words_4_draw.RemoveAt(index);
                        break;

                    case 2:
                        index = UnityEngine.Random.Range(0, words_4_char.Count);
                        wordToGuess = words_4_char[index];
                        words_4_char.RemoveAt(index);
                        break;
                }
                break;
            case 5:
                switch (action)
                {
                    case 0:
                        index = UnityEngine.Random.Range(0, words_5_talk.Count);
                        wordToGuess = words_5_talk[index];
                        words_5_talk.RemoveAt(index);
                        break;

                    case 1:
                        index = UnityEngine.Random.Range(0, words_5_draw.Count);
                        wordToGuess = words_5_draw[index];
                        words_5_draw.RemoveAt(index);
                        break;

                    case 2:
                        index = UnityEngine.Random.Range(0, words_5_char.Count);
                        wordToGuess = words_5_char[index];
                        words_5_char.RemoveAt(index);
                        break;
                }
                break;
        }
		return wordToGuess;
	}

	void ShowWord(string wordToGuess)
	{
		card3.SetActive(false);
		card4.SetActive(false);
		card5.SetActive(false);
		wordCard.SetActive(true);

		Text wordToGuessText = wordCard.GetComponentInChildren<Text>();
		if (!wordToGuessText)
		{
			Debug.LogError("No text attached to the word card.");
			return;
		}

		wordToGuessText.text = wordToGuess;
	}

	void MovePawn(int team, int score)
	{
		if (score - 1 > actionTiles.Length)
		{
			pawns[team].transform.position = finishTile.transform.position;
			return;
		}

		GameObject tile = actionTiles[score - 1];
		pawns[team].transform.position = tile.transform.position;
	}
}
