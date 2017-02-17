using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tests : MonoBehaviour {

	public GameObject obj;	//set this object as webCam
    
    private static bool flag;
  //  public Text t;
    void Awake()
    {
        Screen.SetResolution(2560, 1440, true);
        Screen.orientation = ScreenOrientation.Landscape;
    }


	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        //
        //Debug.Log (obj.GetComponent<ColorDetection> ().imageCheck (2, 40));
        //  t.text = obj.GetComponent<ColorDetection>().imageCheck(2, 40).ToString();
        if (obj.GetComponent<ColorDetection>().IsPlaying)
        {
            if (obj.GetComponent<ColorDetection>().imageCheck(2, 40) > 0.65f && !flag)
            {
                flag = true;
              //  obj.GetComponent<ColorDetection>().close();
                SceneManager.LoadScene("Young1Dae");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
              //  obj.GetComponent<ColorDetection>().close();
                Application.Quit();
            }
        }
    }

}