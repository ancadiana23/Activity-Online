using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour {

    public RawImage rawIm;
    private WebCamTexture webcamTexture = null;
    private RectTransform rect = null;
	public int width, height;
	// Use this for initialization
	void Start () {
        webcamTexture = new WebCamTexture();
        rawIm.texture = webcamTexture;
        rect = rawIm.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (webcamTexture.width > 100)
        {
			rect.sizeDelta = new Vector2(webcamTexture.width, webcamTexture.height);
			rect.localScale = new Vector3(width / (float)webcamTexture.width, height / (float)webcamTexture.height, 1);
		}
	}

	public void StartWebcam()
	{
		if (webcamTexture == null)
			return;
		rawIm.enabled = true;
		webcamTexture.Play();

	}

	public void StopWebcam()
	{
		if (webcamTexture == null)
			return;
		webcamTexture.Stop();
		rawIm.enabled = false;
	}
}
