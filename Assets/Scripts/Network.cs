using UnityEngine;

public class Network : Photon.PunBehaviour {

	/// <summary>
	/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
	/// </summary>
	string _gameVersion = "1";
    string roomName;
    string roomPassword;
	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
	}
	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
	/// </summary>
	void Awake()
	{
		// #NotImportant
		// Force Full LogLevel
		PhotonNetwork.logLevel = PhotonLogLevel.Full;


		// #Critical
		// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
		PhotonNetwork.autoJoinLobby = false;


		// #Critical
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
	}

	/// <summary>
	/// Start the connection process. 
	/// - If already connected, we attempt joining a random room
	/// - if not yet connected, Connect this application instance to Photon Cloud Network
	/// </summary>
	public void Connect()
	{
		// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
		if (!PhotonNetwork.connected)
		{
            PhotonNetwork.ConnectUsingSettings(_gameVersion);
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
		}
        GameObject gameObj = GameObject.Find("GameManager");
        if (!gameObj)
        {
            Debug.LogError("GameManager not found in scene!");
            return;
        }
        GameManager gameManager = gameObj.GetComponent<GameManager>();
        if (!gameManager)
        {
            Debug.LogError("Couldn't get the game manager!");
        }
        if (gameManager.getCreateMode())
        {
            //RoomOptions roomOpt = RoomOptions();
            PhotonNetwork.CreateRoom(roomName);
        }

	}

    public void SetRoomName(string name)
    {
        roomName = name;
        Debug.Log(roomName);
    }

    public void SetRoomPassword(string password)
    {
        roomPassword = password;
        Debug.Log("pass: " + roomPassword);
    }
	public override void OnConnectedToMaster()
	{
		Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");

	}

	public override void OnDisconnectedFromPhoton()
	{

		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
	}
}
