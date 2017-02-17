using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepBackScript : MonoBehaviour {

    public Animator stepMain;
    // Use this for initialization
    public void BackStep()
    {
        ChangeScene.stepMain = true;
        SceneManager.LoadScene("StepMain");
        ///stepMain.SetBool("isBack",false);
        
       

    }
}
