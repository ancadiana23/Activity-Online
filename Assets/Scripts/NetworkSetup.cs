using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking.Match;

public class NetworkSetup : NetworkLobbyManager {

	public InputField matchNameInput;
	public InputField passwordInput;
	public InputField nicknameInput;

	public Button createJoinButton;
	bool isCreateMode;
	void Start () {
		DontDestroyOnLoad(gameObject);

		matchNameInput.onEndEdit.RemoveAllListeners();
		matchNameInput.onEndEdit.AddListener(onEndEditGameName);

		isCreateMode = GameObject.Find("GameManager").GetComponent<GameManager>().getCreateMode();
		if (isCreateMode)
		{
			createJoinButton.onClick.AddListener(OnClickCreateMatchmakingGame);
		}
		else
		{
			createJoinButton.onClick.AddListener(OnClickJoinMatch);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void OnClickCreateMatchmakingGame()
	{
		StartMatchMaker();
		matchMaker.CreateMatch(
			matchNameInput.text,
			(uint)maxPlayers,
			true,
			passwordInput.text,
			"", "", 0, 0,
			OnMatchCreate);

		//lobbyManager.DisplayIsConnecting();
	}

	public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchCreate(success, extendedInfo, matchInfo);
		Debug.Log("Success: " + extendedInfo);
	}

	void onEndEditGameName(string text)
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			OnClickCreateMatchmakingGame();
		}
	}


	public void OnClickJoinMatch()
	{
		StartMatchMaker();
		matchMaker.ListMatches(0, 10, matchNameInput.text, false, 0, 0, OnMatchList);
	}

	override public void  OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		for (int i = 0; i < matches.Count; ++i)
		{
			if (matchNameInput.text.Equals(matches[i].name))
			{
				JoinMatch(matches[i].networkId);
				return;
			}
		}
		Debug.Log("No match with this name found!");
	}

	void JoinMatch(NetworkID networkID)
	{
		matchMaker.JoinMatch(networkID, passwordInput.text, "", "", 0, 0, OnMatchJoined);
		//lobbyManager.DisplayIsConnecting();
	}

	public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		base.OnMatchJoined(success, extendedInfo, matchInfo);
		Debug.Log("match joined");

	}
}
