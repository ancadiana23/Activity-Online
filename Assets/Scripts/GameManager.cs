using UnityEngine;

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
}
