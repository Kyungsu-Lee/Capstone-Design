using UnityEngine;
using System.Collections;
using StepObject;

public class TouchEvent : MonoBehaviour {

	public GameObject obj;

	private Character character;

	bool flag = true;

	// Use this for initialization
	void Start () {
		character = new Character (obj);


		character.changeDirection (DIRECTION.RU);

		Character.Speed = 2;
	}
	
	// Update is called once per frame
	void Update () {
	/*
		if (flag) {
			if (character.Position.x < -10) {
				character.changeDirection (DIRECTION.RU);
			} else if (character.Position.x > 10) {
				character.changeDirection (DIRECTION.LD);
			}
		} else {
			if (character.Position.x < -10) {
				character.changeDirection (DIRECTION.RD);
			} else if (character.Position.x > 10) {
				character.changeDirection (DIRECTION.LU);
			}
		}
		*/
	}

	void OnMouseDown()
	{
	}


	void OnMouseUp()
	{
		/*
		if (character.Direction == DIRECTION.LD || character.Direction == DIRECTION.RU) {
			character.changeDirection (DIRECTION.LU);	
			flag = false;
		} else {
			character.changeDirection (DIRECTION.RU);
			flag = true;
		}

		Default.flag = true;*/
	}
}
