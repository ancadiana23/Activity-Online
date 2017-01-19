using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour {

	public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

	public void OnCreateClick(string scene)
	{
		GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManager.enableCreateMode();
		ChangeScene(scene);
	}

	public void OnClickBackToMainMenu()
	{
		GameObject lobbyManager = GameObject.Find("LobbyManager");
		if (lobbyManager != null)
		{
			Destroy(lobbyManager);
		}

		ChangeScene("MainMenu");
	}

	public void OnClickReady()
	{
		GameObject networkManager = GameObject.Find("NetworkManager");
		if (!networkManager)
		{
			Debug.LogError("Cannot find NetworkManager!");
			return;
		}

		Network network = networkManager.transform.GetComponent<Network>();
		if (!networkManager)
		{
			Debug.LogError("Cannot find the Network script on the object!");
			return;
		}
		network.SetReady();
	}
}
