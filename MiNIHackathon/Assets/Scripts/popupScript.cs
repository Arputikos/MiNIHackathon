using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popupScript : MonoBehaviour
{

    public float maxTime = 1;
    public float time = 0;

    public int speed = 3;
	// Use this for initialization
	void Start () {
	    Init("Test", Color.green);
    }
	
	// Update is called once per frame
	void Update ()
	{
	   
        Vector3 pos = this.transform.position + new Vector3(0, speed * 1.0f * Time.deltaTime, 0);
	    this.transform.position = pos;
	    time += Time.deltaTime;
	    Debug.Log(time);
        if (time >= maxTime)
	    {
            DestroyPopup();
	    }
	}

     void DestroyPopup()
    {
       Destroy(this.gameObject);
    }

    public void Init(string text, Color color)
    {
        var textMesh = gameObject.GetComponentInChildren<TextMesh>();
        textMesh.text = text;
        textMesh.color = color;
    }
}
