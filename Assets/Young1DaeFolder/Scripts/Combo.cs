using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Combo : MonoBehaviour {

        
    public Text combo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


   public void setText()
    {
        combo.text = Done_DestroyByContact.comboCount + " Combo!";
    }


}
 