using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using MyResource;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GameObject gameOverImg;
    public GameObject menuImg;
    public GameObject asteroid;

    public Text scoreText;
    public Text polluteText;
    //  public Text restartText;
   // public Text gameOverText;
  //  public GameObject restartButton;

   // private bool gameOver;
    //  private bool restart;
    private int score;
    private GameObject reseter;
    public int initPollutePoint;

	public GameObject obj;
	SocketFunctions socket;
 

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        reseter = GameObject.FindGameObjectWithTag("Boundary");

      //  StartCoroutine(DataMgr.instance.SaveScore("abc", 123));
        //  gameOver = false;
        //      restart = false;
        //      restartText.text = "";
        //gameOverText.text = "";
        // restartButton.SetActive(false);
        score = 0;
        UpdateScore();
        UpdatePollute();
        StartCoroutine(SpawnWaves());

		socket = obj.GetComponent<SocketFunctions> ();
    }

    //  void Update ()
    //  {
    //      if (restart)
    //      {
    //          if (Input.GetKeyDown (KeyCode.R))
    //          {
    //              Application.LoadLevel (Application.loadedLevel);
    //          }
    //      }
    //  }

	private bool deadflag = false;

        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {    
                Application.Quit();
        }

		if(initPollutePoint <= 0 && !deadflag)
        {
            //gameOverImg.SetActive(true);
            //asteroid.SetActive(false);

			deadflag = true;
			socket.getGameScore (Resource.id, "shooter");
        }

		if (socket.CanGetScore) {
			int recordedScore = socket.score;

			int max = Mathf.Max (recordedScore, score);

			socket.updateGameScore (Resource.id, "shooter", max);
			deadflag = false;

			gameOverImg.SetActive(true);
			asteroid.SetActive(false);
		}
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject obj = Instantiate(hazard, spawnPosition, spawnRotation);

             

                if (obj != null)
                {
                    if (score > 200 && score < 400)
                        obj.GetComponent<Done_Mover>().speed = 0.13f;
                    if (score > 400 && score < 800)
                        obj.GetComponent<Done_Mover>().speed = 0.16f;
                    if (score > 800 && score < 1000)
                        obj.GetComponent<Done_Mover>().speed = 0.19f;
                    if (score > 1000 && score < 1500)
                        obj.GetComponent<Done_Mover>().speed = 0.22f;
                    if (score > 2000)
                        obj.GetComponent<Done_Mover>().speed = 0.25f;
                }

                if (Random.Range(0, 100) % 20 == 0)
                {
                    obj.GetComponent<Animator>().SetTrigger("Shine");
                    obj.GetComponent<Done_Mover>().speed = 0.3f;

                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

          //  if (gameOver)
            {
               // restartButton.SetActive(true);
                //              restartText.text = "Press 'R' for Restart";
                //              restart = true;
                //break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void AddPollute(int newPolluteValue)
    {
        initPollutePoint -= newPolluteValue;

        if(initPollutePoint >= 0)
             UpdatePollute();
    }

    void UpdateScore()
    {
        scoreText.text = "점수: " + score;
    }

    void UpdatePollute()
    {
        polluteText.text = "쾌적도: " + initPollutePoint;
    }

    public void MenuUp()
    {
      
        menuImg.SetActive(true);
    }

    public void Resume()
    {
       
        menuImg.SetActive(false);
    }

    public void RestartGame()
    {
        Done_Mover.guideCount = 0;
        reseter.GetComponent<Reseter>().Reset();
        Application.LoadLevel(Application.loadedLevel);
     
    }


    public void QuitGame()
    {
       
        Done_Mover.guideCount = 0;
        reseter.GetComponent<Reseter>().Reset();
        
        SceneManager.LoadScene("01Main");
        GameObject.FindGameObjectWithTag("CAMERA").GetComponent<CamA>().close();
    }
    public void MoveGame()
    {
        SceneManager.LoadScene("StepByStep");
    }
}