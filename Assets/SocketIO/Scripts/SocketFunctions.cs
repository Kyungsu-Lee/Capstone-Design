using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class SocketFunctions : MonoBehaviour {

	public delegate void Action ();

	public GameObject go;
	SocketIOComponent socket;

	public Action addIdAction;

	// Use this for initialization
	void Start () {
		go = GameObject.Find("SocketIOFinal");
		socket = go.GetComponent<SocketIOComponent> ();
		socket.Connect ();

		addIdAction += () => {
			Debug.Log("well");
		};
	}

	// Update is called once per frame
	void Update () {
		if (nameCheckDoFlag) {

			if (nameCheckFlag) {
				Debug.Log ("already has");
				addIdAction ();
				addIdAction = () => {
				};
			} else {
				addId (id);
			}

			nameCheckDoFlag = false;
		}
	}

	private bool nameCheckFlag 		= false;
	private bool nameCheckDoFlag 	= false;
	private string id = "";

	private void recieveName(SocketIOEvent e)
	{
		nameCheckFlag = e.data.GetField ("check").ToString ().Equals ("\"true\"");
		nameCheckDoFlag = true;
	}

	public void checkId(string id)
	{
		JSONObject jo = new JSONObject ();
		jo.AddField ("id", id);
		socket.Emit ("searchName", jo);
		socket.On ("nameCheck", recieveName);
		this.id = id;
	}

	private void addId(string id)
	{
		JSONObject jo = new JSONObject ();
		jo.AddField ("id", id);
		socket.Emit ("insertId", jo);
		Debug.Log ("id added");
		addIdAction ();
		addIdAction = () => {
		};
	}

	public void updateGameScore(string id, string gameName, int score)
	{
		JSONObject jo = new JSONObject ();
		jo.AddField ("id", id);
		jo.AddField ("GameName", gameName);
		jo.AddField ("score", score);
		socket.Emit ("updateScore", jo);
	}

	private bool getScoreFlag = false;
	private int tmp_score = 0;

	public void getGameScore(string id, string gameName)
	{
		JSONObject jo = new JSONObject ();
		jo.AddField ("id", id);
		jo.AddField ("GameName", gameName);
		socket.Emit ("getScore", jo);
		socket.On ("getScore", getScore);
	}

	private void getScore(SocketIOEvent e)
	{
		tmp_score = int.Parse (e.data.GetField ("score").ToString());
		getScoreFlag = true;
	}

	public bool CanGetScore {
		get {
			return getScoreFlag;
		}
	}

	public int score {
		get {
			if (getScoreFlag) {
				getScoreFlag = false;
				return tmp_score;
			} else
				return -1;
		}
	}

	private bool getRankingFlag = false;
	private int tmp_rank = 0;

	public void getRanking(string id, string gameName)
	{
		JSONObject jo = new JSONObject ();
		jo.AddField ("id", id);
		jo.AddField ("GameName", gameName);
		socket.Emit ("ranking", jo);
		socket.On ("ranking", getRank);
	}

	private void getRank(SocketIOEvent e)
	{
		if (!getRankingFlag) {
			tmp_rank = int.Parse (e.data.GetField ("ranking").ToString ());
			getRankingFlag = true;
		}
	}

	public bool CanGetRanking {
		get {
			return getRankingFlag;
		}
	}

	public int ranking {
		get {
			if (getRankingFlag) {
				getRankingFlag = false;
				return tmp_rank;
			} else
				return -1;
		}
	}

}
