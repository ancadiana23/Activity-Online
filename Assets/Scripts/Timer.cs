using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    float timeLeft;
    public Text timerText;
    public Button startButton;

    // Use this for initialization
    public void StartTimer () {
		startButton.gameObject.SetActive(false);
        timeLeft = 60;
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
            timerText.text = string.Format("{0:0}", timeLeft);
            yield return new WaitForSeconds(0.2f);
        }
		startButton.gameObject.SetActive(true);
	}
}
