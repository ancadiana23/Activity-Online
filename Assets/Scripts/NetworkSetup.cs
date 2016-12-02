using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.UI;

public class NetworkSetup : MonoBehaviour {
	LobbyManager lobbyManager;
	public InputField roomName;
	public InputField password;
	public InputField nickname;
	// Use this for initialization
	void Start () {
		lobbyManager = GetComponent<LobbyManager>();

		if (lobbyManager == null)
		{
			Debug.Log("Couldn't retrieve the network manager!");
			return;
		}

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCreate ()
	{
		lobbyManager.StartMatchMaker();
		lobbyManager.StartMatchMaker();
		lobbyManager.matchMaker.CreateMatch(
			roomName.text,
			(uint)lobbyManager.maxPlayers,
			true,
			"", "", "", 0, 0,
			lobbyManager.OnMatchCreate);
	}
}
