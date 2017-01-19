using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : Photon.PunBehaviour {

	/// <summary>
	/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
	/// </summary>
	string _gameVersion = "1";
    string roomName;
    string roomPassword;
    GameObject nameList;
    public GameObject nameSlot;

	public GameObject[] PrefabsToInstantiate;   // set in inspector
	private PhotonVoiceRecorder rec;
	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
        SceneManager.activeSceneChanged += OnSceneChanged;
        DontDestroyOnLoad(this);
	}
	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
	/// </summary>
	void Awake()
	{
		// #NotImportant
		// Force Full LogLevel
		PhotonNetwork.logLevel = PhotonLogLevel.Full;

        //PhotonNetwork.OnEventCall += OnPhotonPlayerConnected;
		// #Critical
		// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
		PhotonNetwork.autoJoinLobby = false;


		// #Critical
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
	}

    void OnSceneChanged(Scene scene1, Scene scene2)
    {
        if (scene2.name.Equals("Lobby"))
        {
            nameList = GameObject.Find("NameList");
            if (nameList == null)
            {
                Debug.LogError("No name list found in the scene!");
                return;
            }
        }
        UpdatePlayerList();
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
	}

    public void UpdatePlayerList()
    {
        PhotonPlayer[] playerList = PhotonNetwork.playerList;
        Text[] names = nameList.transform.GetComponentsInChildren<Text>();
        if (names == null)
        {
            Debug.LogError("Couldn't get the list of player names");
            return;
        }

		for (int i = 0; i < playerList.Length; ++i)
        {
			bool exists = false;
			for (int j = 0; j < names.Length; ++j)
			{
				if (names[j].text.Equals(playerList[i].name))
				{
					exists = true;
					break;
				}
			}

			if (exists)
			{
				continue;
			}

            GameObject playerName = PhotonNetwork.Instantiate("PlayerText", Vector3.zero, Quaternion.identity, 0) as GameObject;
            Text nameText = playerName.transform.GetComponentInChildren<Text>();
            
            if (nameText == null)
            {
                Debug.LogError("Can't get the text of the button!");
                return;
            }
            nameText.text = playerList[i].name;
            playerName.transform.SetParent(nameList.transform, false);
        }
    }

    void LoadLobby()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.Log("PhotonNetwork: Loading the room");
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void SetRoomPassword(string password)
    {
        roomPassword = password;
        Debug.Log("pass: " + roomPassword);
    }
	public override void OnConnectedToMaster()
	{
		Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
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
            RoomOptions roomOpt = new RoomOptions();
            roomOpt.IsVisible = false;
            roomOpt.PublishUserId = true;
            PhotonNetwork.CreateRoom(roomName, roomOpt, null);
        }
        else
        {
            PhotonNetwork.JoinRoom(roomName);
		}		
    }
    
    public override void OnDisconnectedFromPhoton()
	{

		Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
	}

    public override void OnJoinedRoom()
    {
        Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

		Vector3 spawnPos = Vector3.zero;
		GameObject o = PhotonNetwork.Instantiate("ZomBunny 1", spawnPos, Quaternion.identity, 0);
		rec = o.GetComponent<PhotonVoiceRecorder>();
		rec.enabled = true;
		rec.Transmit = true;
		DontDestroyOnLoad (rec);
		if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected

            LoadLobby();
        }
	}


    public override void OnPhotonPlayerConnected(PhotonPlayer player)
    {

            Debug.Log("Players : ");
            PhotonPlayer[] playerList = PhotonNetwork.playerList;
            for (int i = 0; i < playerList.Length; ++i)
            {
                Debug.Log("Player: " + playerList[i].name);
            }
            UpdatePlayerList();
	}



    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("OnPhotonPlayerDisconnectedConnected() " + otherPlayer.name); // not seen if you're the player connecting
    }

}
