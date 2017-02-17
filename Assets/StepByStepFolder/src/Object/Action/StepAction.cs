using UnityEngine;
using System.Collections;
using StepObject;

public class StepAction : MonoBehaviour {

	public bool stepShowAction 		= false;
	public bool stepDeleteAction 	= false;

	Step step;

	// Use this for initialization
	void Start () {
		step = Step.findObject (this.transform) as Step;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (stepShowAction)
			step.showActionUpdate ();
		if (stepDeleteAction)
			step.deleteActionUpdate ();
	}
}
