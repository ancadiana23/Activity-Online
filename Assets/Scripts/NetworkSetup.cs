using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkSetup : MonoBehaviour {
	NetworkLobbyManager lobbyManager;
	public InputField roomName;
	public InputField password;
	public InputField nickname;
	// Use this for initialization
	void Start () {
		lobbyManager = GetComponent<NetworkLobbyManager>();

		if (lobbyManager == null)
		{
			Debug.Log("Couldn't retrieve the network manager!");
			return;
		}

		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCreate ()
	{
		lobbyManager.StartMatchMaker();
		lobbyManager.StartMatchMaker();
		lobbyManager.matchMaker.CreateMatch(
			roomName.text,
			(uint)lobbyManager.maxPlayers,
			true,
			password.text, "", "", 0, 0,
			lobbyManager.OnMatchCreate);

	}
}
