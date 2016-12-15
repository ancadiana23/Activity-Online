using UnityEngine;
using System.Collections;

public class PaintGM : MonoBehaviour {

    public Canvas canvas;
    public Transform baseDot;
    public KeyCode mouseLeft;
    public static string toolType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetKey (mouseLeft))
        {
            Transform newObject = (Transform) Instantiate(baseDot, new Vector3(mousePosition.x, mousePosition.y, 10), baseDot.rotation);
            newObject.transform.SetParent(canvas.transform, true);
            
        }*/
	}

	public void DrawPoint()
	{
		Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Transform newObject = (Transform)Instantiate(baseDot, new Vector3(mousePosition.x, mousePosition.y, 10), baseDot.rotation);
		newObject.transform.SetParent(canvas.transform, true);
	}
}
