using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Step_GameManager : MonoBehaviour {

    public GameObject gameOverImg;
    public GameObject menuImg;
  

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MenuUp()
    {
        menuImg.SetActive(true);

        GameObject obj = GameObject.FindGameObjectWithTag("BG");
        obj.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Resume()
    {
      
        menuImg.SetActive(false);
        GameObject obj = GameObject.FindGameObjectWithTag("BG");
        obj.GetComponent<BoxCollider2D>().enabled = true;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
