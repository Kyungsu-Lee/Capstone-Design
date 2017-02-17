using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using StepObject;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SocketIO;

public class Test : MonoBehaviour {

    static int cleared = 1;
    static bool master;

	public GameObject go_character;
	public GameObject go_lSteps;
	public GameObject go_rSteps;
	public GameObject game_camera;
    public GameObject[] blocks;
    public Sprite[] colors; 

	public GameObject lWall;
	public GameObject rWall;

	public GameObject clear;

    public Text stageText;

    public AudioSource aSrc;
    public AudioClip wall, success;

    Steps steps;
	Steps new_steps;
	Character character;

	int stage = 0;
	int currentStage = 1;

	StringReader sr;
	string[] component;
	string line;

	public GameObject obj;
	SocketFunctions socket;

	// Use this for initialization
	void Start () {

		socket = obj.GetComponent<SocketFunctions> ();

        for(int i = 0; i < 20; i++)
        {
            if(master)
                blocks[i].GetComponent<SpriteRenderer>().sprite = colors[19];
            else
              blocks[i].GetComponent<SpriteRenderer>().sprite = colors[cleared - 1];
        }

        //	sr = new StreamReader(Application.dataPath + "/Resources/" + "stage.txt");
        stageText.text = cleared.ToString();

		TextAsset a = Resources.Load("stage") as TextAsset;
		sr = new StringReader (a.text);
		line = sr.ReadLine ();
		stage = int.Parse (line);

		line = sr.ReadLine ();
		if (line.Equals(""))
			line = sr.ReadLine ();
		component = line.Split (new char[]{ ' ', '\t' });

		if (component [0].Equals ("L")) {
			steps = new LSteps (Instantiate (go_lSteps), Instantiate (lWall), Instantiate (lWall));
		} else {
			steps = new RSteps (Instantiate (go_rSteps), Instantiate (rWall), Instantiate (rWall));
		}


		steps.Position = new Vector2 (float.Parse(component[1]), float.Parse(component[2]));
		steps.bringWalls (int.Parse(component[3]), int.Parse(component[4]));

		character = new Character (Instantiate (go_character));
		character.Position = new Vector2 (0, 0.5f);
       // Character.Speed = 0.1f;

		line = sr.ReadLine ();
		component = line.Split (new char[]{ ' ', '\t' });

		if (component [0].Equals ("R"))
			new_steps = new RSteps (Instantiate (go_rSteps), Instantiate (rWall), Instantiate (rWall));
		else
			new_steps = new LSteps (Instantiate (go_lSteps), Instantiate (lWall), Instantiate (lWall));
		
		new_steps.Position = new Vector2 (float.Parse(component[1]), float.Parse(component[2]));
		new_steps.order = 0;
		steps.order = new_steps.order + 1;

		character.follow = new
			MathU.Line
			(
				new Vector3(steps.LWallPosition.x, steps.LWallPosition.y, 0),
				new Vector3(steps.RWallPosition.x, steps.RWallPosition.y, 0)
			);
		character.characterMoveToWall (steps.RWallPosition);

	}

	bool flag = true;
	bool change = false;
	bool cameraFlag = false;

    public bool onButton = false;

	float height = 0;
	const float HEIGHT = 0.15f;

	bool characterDie       = false;
	bool clearflag          = false;
	bool clearRotateFlag    = true;
    bool soundFlag          = true;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!onButton)
        {

            if (!stageend)
            {
             
                if (flag)
                {
                
                    if (Vector2.Distance(character.Position, steps.LWallPosition) < 0.8f)
                    {
                        if (soundFlag)
                            {
                            aSrc.clip = wall;
                            aSrc.Play();
                        }

                        character.VectorDirection = steps.RWallPosition;
                        character.Rotation = new Vector3(0, 180, 0);
                        character.Gravity = false;

                        if (change)
                        {

                            if (component[0].Equals("L"))
                                new_steps = new LSteps(Instantiate(go_lSteps), Instantiate(lWall), Instantiate(lWall));
                            else
                                new_steps = new RSteps(Instantiate(go_rSteps), Instantiate(rWall), Instantiate(rWall));
                            new_steps.Position = new Vector2(float.Parse(component[1]), float.Parse(component[2]));

                            change = false;
                            steps.hideR(false);

                            new_steps.order = 0;
                            steps.order = new_steps.order + 1;

                            character.follow = new
                            MathU.Line(
                                new Vector3(steps.LWallPosition.x, steps.LWallPosition.y, 0),
                                new Vector3(steps.RWallPosition.x, steps.RWallPosition.y, 0)
                            );
                        }

                    }
                    else if (Vector2.Distance(character.Position, steps.RWallPosition) < 0.4f)
                    {
                        if (soundFlag)
                        {
                            aSrc.clip = wall;
                            aSrc.Play();
                        }


                        character.Rotation = new Vector3(0, -180, 0);
                        character.VectorDirection = steps.LWallPosition;
                        //character.Gravity = true;
                    }
                }

                if (!flag && !clearflag)
                {
                    steps.delete(out flag, out change, out soundFlag);
                }

                if (change)
                {
                    steps = new_steps;
                }

                if (cameraFlag)
                {
                    if (height < HEIGHT)
                    {
                        height += 0.01f;
                        this.game_camera.GetComponent<Transform>().Translate(Vector3.up * height);
                        this.GetComponent<BoxCollider2D>().offset = new Vector2(this.GetComponent<BoxCollider2D>().offset.x, this.GetComponent<BoxCollider2D>().offset.y + height- 0.02f) ;
                    }
                    else
                    {
                        height = 0;
                        cameraFlag = false;
                    }
                }

                if (characterDie)
                { // char die
                    character.Gravity = true;
                    character.stop();
                    flag = false;
                }

                if (characterDie && character.Position.y < game_camera.GetComponent<Transform>().position.y - 4.0f)
                {
					socket.updateGameScore (MyResource.Resource.id, "stepbystep", cleared);

                    character.Destroy();
                    characterDie = false;
                    cleared = 1;
                    master = false;
                    Character.Speed = 0.07f;
                    SceneManager.LoadScene("StepByStep");
                }
            
                if (clearflag)
                { // clear
   
                    clearRotateFlag = !clearRotateFlag;
 
                    /*
                         if (clearRotateFlag)
                             clear.GetComponent<Transform>().Rotate(new Vector3(0, 0, 10));
                         else
                             clear.GetComponent<Transform>().Rotate(new Vector3(0, 0, -10));
                             */
                }
            }
        }

    }




    void OnMouseDown()
	{
		if (flag) {

			flag = false;
			new_steps.bringWallR (int.Parse (component [4])); 
			new_steps.bringWallL (int.Parse (component [3]));
			new_steps.hideR (true);

			try{
			line = sr.ReadLine ();
			component = line.Split (new char[]{ ' ', '\t' });
			}
			catch(Exception e) {
				characterdie ();
			}


			MathU.Line c_line = new
			MathU.Line (
				                   new Vector3 (new_steps.LWallPosition.x, new_steps.LWallPosition.y, 0),
				                   new Vector3 (character.Position.x, character.Position.y, 0)
			                   );

			MathU.Line v_line = new MathU.Line (
				                   new Vector3 (new_steps.RWallPosition.x, new_steps.RWallPosition.y, 0),
				                   new Vector3 (new_steps.LWallPosition.x, new_steps.LWallPosition.y, 0)
			                   );
		
			if (Mathf.Abs (Vector3.Angle (c_line.directionVector, v_line.directionVector)) > 175) { //success
                aSrc.clip = success;
                aSrc.Play();

                character.follow = new
			MathU.Line (
					new Vector3 (new_steps.LWallPosition.x, new_steps.LWallPosition.y, 0),
					new Vector3 (character.Position.x, character.Position.y, 0)
				);
				character.characterMoveToWall (steps.RWallPosition);
			//	Character.Speed += 0.01f;
				cameraFlag = true;


				if (++currentStage == stage) {
					//Character.Speed = 0.1f;
					Invoke ("clearMethod", 1f);
				}

			} else {
				Vector3 v = character.V3Position + v_line.directionVector;
				character.follow = new 
					MathU.Line 
					(
						character.Position,
						v
					);
				character.characterMoveToWall (v);
				//Character.Speed *=2f;
				Invoke ("characterdie", 0.07f);
			}
		}
	}

	void characterdie()
	{
		characterDie = true;
	}

	void clearMethod()
	{
        IEnumerator coroutine;

        clearflag = true;
        //clear.GetComponent<Transform> ().position = new Vector3 (game_camera.GetComponent<Transform> ().position.x, game_camera.GetComponent<Transform> ().position.y, 0);
        clear.SetActive(true);
		character.Destroy ();
		characterDie = false;
		character.stop ();
		flag = false;
		steps.Destroy ();
		new_steps.Destroy ();
        if (cleared == 15)
        {
            cleared = 0;
            master = true;
        }
        cleared++;
     
        Character.Speed += 0.005f;
        stageText.text = cleared.ToString();


        Invoke ("unclearMethod", 2.0f);
 
        StartCoroutine(sleepClear());
    
    }

	bool stageend = false;

    IEnumerator sleepClear()
    {
        yield return new WaitForSeconds(2.0f);
        
        SceneManager.LoadScene("StepByStep");
    }

	void unclearMethod()
	{
		clearflag = false;
		flag = false;
		stageend = true;
		clear.SetActive (false);
	}

    public void RestartGame()
    {
        character.Destroy();
        characterDie = false;
        cleared = 1;
        SceneManager.LoadScene("StepByStep");
    }

    public void QuitGame()
    {
        character.Destroy();
        characterDie = false;
        cleared = 1;
        SceneManager.LoadScene("StepMain");
    }

    public void MoveGame()
    {
        character.Destroy();
        characterDie = false;
        cleared = 1;
        SceneManager.LoadScene("Young1Dae");
    }

}
