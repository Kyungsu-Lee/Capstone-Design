using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public Animator cAnim;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void MoveYI()
    {
        cAnim.SetTrigger("yongyil");
        StartCoroutine("WaitingYI");
    }
    public void MoveNY()
    {
        cAnim.SetTrigger("NY");
        //StartCoroutine("Waiting");
    }
    public void MoveSM()
    {
        cAnim.SetTrigger("Sm");
        //StartCoroutine("Waiting");
    }
    public void MoveHGU()
    {
        cAnim.SetTrigger("HGU");
        //StartCoroutine("Waiting");
    }
    public void MoveGRP()
    {
        cAnim.SetTrigger("90");
        StartCoroutine("WaitingGRP");
    }
    IEnumerator WaitingGRP() {
        yield return new WaitForSeconds(2f);
        ///modify to game Scene
        SceneManager.LoadScene("StepMain");
    }
    IEnumerator WaitingYI()
    {
        yield return new WaitForSeconds(2f);
        ///modify to game Scene
        SceneManager.LoadScene("01Main");
    }

}
