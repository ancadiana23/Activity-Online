using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    float timeLeft;
	public float time = 60;
    public Text timerText;
    public Button startButton;
	public Button guessedButton;
	public GameSession gameSession;
	public GameObject tilesGroup;
	public GameObject pawns;
	public GameObject whiteboard;
	public Text explainText;
	public Webcam webcam;

	static int MIME = 0, TALK = 1, DRAW = 2;
	// Use this for initialization
	public void StartTimer () {
		if (!gameSession.WordChosen)
		{
			return;
		}
		guessedButton.gameObject.SetActive(true);
		startButton.gameObject.SetActive(false);
		tilesGroup.SetActive(false);
		pawns.SetActive(false);
		timerText.gameObject.SetActive(true);
		if (gameSession.Action == MIME)
		{
			webcam.StartWebcam();
		}
		else if (gameSession.Action == DRAW)
		{
			whiteboard.SetActive(true);
		}
		else if (gameSession.Action == TALK)
		{
			explainText.gameObject.SetActive(true);
		}
		timeLeft = time;
        StartCoroutine(updateCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
    }

    private IEnumerator updateCoroutine()
    {
        while (timeLeft > 0)
        {
			if (gameSession.WordGuessed)
			{
				break;
			}
            timerText.text = string.Format("{0:0}", timeLeft);
            yield return new WaitForSeconds(0.2f);
        }

		if (gameSession.Action == MIME)
		{
			webcam.StopWebcam();
		}
		else if (gameSession.Action == DRAW)
		{
			whiteboard.SetActive(false);
			Canvas canvas = whiteboard.GetComponentInParent<Canvas>();
			if (!canvas)
			{
				Debug.LogError("Couldn't get the parent canvas of the whiteboard!");
			}
			Transform[] children = canvas.GetComponentsInChildren<Transform>();
			for (int i = 0; i < children.Length; ++i)
			{
				if (children[i].CompareTag("Dot"))
				{
					Destroy(children[i].gameObject);
				}
			}
		}
		else if (gameSession.Action == TALK)
		{
			explainText.gameObject.SetActive(false);
		}
		guessedButton.gameObject.SetActive(false);
		startButton.gameObject.SetActive(true);
		tilesGroup.SetActive(true);
		pawns.SetActive(true);
		timerText.gameObject.SetActive(false);
		gameSession.newRound();
	}
}
