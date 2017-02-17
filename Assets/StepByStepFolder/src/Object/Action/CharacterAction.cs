using UnityEngine;
using System.Collections;
using StepObject;

public class CharacterAction : MonoBehaviour {

	public bool characterMoving		= false;
	public bool characterMovingRU 	= false;
	public bool characterMovingRD 	= false;
	public bool characterMovingLU 	= false;
	public bool characterMovingLD 	= false;

	public bool characterFall 		= false;
	public bool characterGoal 		= false;

	public bool characterToWall 	= false;

	public bool flag 				= true;

	private Character character;

	// Use this for initialization
	void Start () {
		character = Character.findObject (this.transform) as Character;
	}
	
	// Update is called once per frame
	void Update () {

		if (flag) {
//		character.characterMove ();

			if (characterMovingRU)
				character.characterMoveToRU ();
			if (characterMovingLD)
				character.characterMoveToLD ();
			if (characterMovingLU)
				character.characterMoveToLU ();
			if (characterMovingRD)
				character.characterMoveToRD ();
			if (characterToWall)
				character.characterToWall ();
		}
	}

}
