using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{

    public GameObject trailPrefab;
    GameObject thisTrail;
    Vector3 startPos;
    Plane objPlane;
    Material mat;
    bool red_color, green_color, blue_color;

    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
        red_color = true;
        green_color = false;
        blue_color = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            thisTrail = Instantiate(trailPrefab) as GameObject;
            thisTrail.transform.parent = GameObject.Find("Canvas").transform;
            mat = thisTrail.GetComponent<TrailRenderer>().material;

            thisTrail.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (red_color)
            {
                //mat.SetColor("_Color", new Color(2.0f / 255, 107.0f / 255, 85.0f / 255, 1));
                mat.SetColor("_Color", new Color(1, 0, 0, 1));
            }
            else if (green_color)
                mat.SetColor("_Color", new Color(0, 1, 0, 1));
            else
                mat.SetColor("_Color", new Color(0, 0, 1, 1));


            Ray nRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(nRay, out rayDistance))
                startPos = nRay.GetPoint(rayDistance);
        }
        else if (Input.GetMouseButton(0))
        {
            Ray nRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;
            if (objPlane.Raycast(nRay, out rayDistance))
                thisTrail.transform.position = nRay.GetPoint(rayDistance);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
            {
                Destroy(thisTrail);
            }
        }
        else if (Input.GetKeyDown("r"))
        {
            red_color = true;
            green_color = false;
            blue_color = false;
        }
        else if (Input.GetKeyDown("g"))
        {
            red_color = false;
            green_color = true;
            blue_color = false;
        }
        else if (Input.GetKeyDown("b"))
        {
            red_color = false;
            green_color = false;
            blue_color = true;
        }
        else if (Input.GetKeyDown("c"))
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            if (!canvas)
            {
                Debug.LogError("Couldn't get the parent canvas of the whiteboard!");
            }
            Transform[] children = canvas.GetComponentsInChildren<Transform>();
            for (int i = 0; i < children.Length; ++i)
            {
                if (children[i].CompareTag("Line"))
                {
                    Destroy(children[i].gameObject);
                }
            }
        }
    }
}
