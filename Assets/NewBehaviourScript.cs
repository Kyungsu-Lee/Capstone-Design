using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	WebCamTexture cams;
	GameObject oj;

	// Use this for initialization
	void Start () {
	
		cams = new WebCamTexture ();
		oj.GetComponent<Renderer> ().material.mainTexture = cams;
		cams.Play ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
