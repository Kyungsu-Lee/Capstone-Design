using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyResource;

public class ChangeScene : MonoBehaviour {

    public static bool stepMain;
    public static bool YIMain;

    private Animator ani;
    private Animator ani2;

	public GameObject obj;
	public GameObject text;

	SocketFunctions socket;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("STEP");
        if (obj != null)
        {
            ani = obj.GetComponent<Animator>();

            if (!stepMain)
            {
               
                ani.SetTrigger("stepStart");
                stepMain = true;
            }
           
            
        }

        GameObject obj2 = GameObject.FindGameObjectWithTag("YI");
        if (obj2 != null)
        {
            ani2 = obj2.GetComponent<Animator>();

            if (!YIMain)
            {
                ani2.SetTrigger("YIStart");
                YIMain = true;
            }

          
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    public void GotoStepMain(){
		SceneManager.LoadScene ("StepMain");
   
    }
	public void GotoStepScore(){
		SceneManager.LoadScene ("StepScore");
	}
    public void GotoYIScore()
    {
        SceneManager.LoadScene("01Score");
    }
    public void GotoStepDescription()
    {
        SceneManager.LoadScene("StepDescription");
    }
    public void GotoYIMain()
    {
        SceneManager.LoadScene("01Main");
    }
    public void GotoYIDescription()
    {
        SceneManager.LoadScene("01Description");
    }
    public void GotoMap()
    {
        SceneManager.LoadScene("Map");
    }
    public void GotoLogin()
    {
        SceneManager.LoadScene("Login");
    }
    public void GotoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void PlayStep()
    {
        SceneManager.LoadScene("StepByStep");
    }

    public void PlayYoung()
    {
        SceneManager.LoadScene("Mains");
    }

	public void GotoMapLogIn()
	{	
		socket = obj.GetComponent<SocketFunctions> ();
		socket.addIdAction  += GotoMap ;
		socket.checkId (text.GetComponent<Text> ().text);
		Resource.id = text.GetComponent<Text> ().text;
	}
}
