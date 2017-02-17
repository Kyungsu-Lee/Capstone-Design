using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YIBackScript : MonoBehaviour {

    //public Animator yiMain;
    // Use this for initialization
    public void YIBackStep()
    {
        ChangeScene.YIMain = true;
        SceneManager.LoadScene("01Main");
    }
}
