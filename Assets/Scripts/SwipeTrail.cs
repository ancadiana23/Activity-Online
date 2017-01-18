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

    void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("new line");
            thisTrail = Instantiate(trailPrefab) as GameObject;
            thisTrail.transform.parent = GameObject.Find("Canvas").transform;
            mat = thisTrail.GetComponent<TrailRenderer>().material;
            mat.SetColor("_Color", new Color(1, 0, 1, 1));

            if (thisTrail == null)
                Debug.Log("e null");
            else
                Debug.Log("perfect");

            thisTrail.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray nRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(nRay, out rayDistance))
                startPos = nRay.GetPoint(rayDistance);
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("continue line");
            Ray nRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!thisTrail)
                Debug.Log("whaat");
            float rayDistance;
            if (objPlane.Raycast(nRay, out rayDistance))
                thisTrail.transform.position = nRay.GetPoint(rayDistance);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (thisTrail == null)
                Debug.Log("e null 2");
            else
                Debug.Log("perfect 2");

            if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
            {
                Destroy(thisTrail);
            }
        }
    }
}
