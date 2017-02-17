using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YIMenu : MonoBehaviour {

    public GameObject account;

	// Use this for initialization
	void Start () {
       GameObject account = GameObject.FindGameObjectWithTag("YIAccount");
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActiveAccount() {
        account.gameObject.SetActive(true);
    }
    public void InactiveAccount()
    {
        account.gameObject.SetActive(false);
    }
}
