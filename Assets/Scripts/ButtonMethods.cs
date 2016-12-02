using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour {

	public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
