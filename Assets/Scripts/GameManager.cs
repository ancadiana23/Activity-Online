using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	bool isCreateMode;
	public static GameManager instance = null;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Destroy(gameObject);
		}
	}

	void Start () {
		isCreateMode = false;
		DontDestroyOnLoad(gameObject);
	}
	
	public void enableCreateMode()
	{
		isCreateMode = true;
	}

	public bool getCreateMode()
	{
		return isCreateMode;
	}

	#region Photon Messages


	/// <summary>
	/// Called when the local player left the room. We need to load the launcher scene.
	/// </summary>
	public void OnLeftRoom()
	{
		SceneManager.LoadScene("CreateJoinGame");
	}


	#endregion


	#region Public Methods


	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}


	#endregion

	#region Private Methods


	void LoadArena()
	{
		if (!PhotonNetwork.isMasterClient)
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}
		Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
		PhotonNetwork.LoadLevel("Lobby");
	}


	#endregion
}
