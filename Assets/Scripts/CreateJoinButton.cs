using UnityEngine;
using UnityEngine.UI;

public class CreateJoinButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		Text buttonText = GetComponentInChildren<Text>();
		if (gameManager.getCreateMode())
		{
			buttonText.text = "Create";
		}
		else
		{
			buttonText.text = "Join";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
