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
}
