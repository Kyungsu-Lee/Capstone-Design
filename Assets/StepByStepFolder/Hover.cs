using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

    public GameObject std;

    bool over = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnMouseEnter()
    {
        print("on");
        std.GetComponent<Test>().onButton = true;
        
    }
}
