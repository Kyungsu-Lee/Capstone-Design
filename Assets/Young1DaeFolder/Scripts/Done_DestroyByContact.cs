using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject polluteExplosion;
	public int scoreValue,polluteValue;


    private Done_GameController gameController;
    private Combo comboScr;
    private GameObject comboObj;
    private GameObject guide;


    public static int comboCount = 0;

    void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");

        if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
           
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
        
	}

    public void getGuide(GameObject tempGuide)
    {
        guide = tempGuide;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
		if(other.tag == "Fire")
        {
            // Destroy(other.gameObject);
        

            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
            if (guide != null)
            {
                --Done_Mover.guideCount;
                Destroy(guide);
            }
            comboCount++;
            gameController.AddScore(comboCount);
            comboObj = other.gameObject;
            comboScr = comboObj.GetComponent<Combo>();
            comboScr.setText();

        }

        if (other.tag == "Pollute")
        {
            Instantiate(polluteExplosion, transform.position, Quaternion.Euler(-90,0,0));
            gameController.AddPollute(polluteValue);
            Destroy(gameObject);
        }

        //gameController.AddScore(scoreValue);

    }
}