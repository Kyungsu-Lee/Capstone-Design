using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class item{
	public string grade;
	public string userName;
	public string score;
}
public class CreateScrollList : MonoBehaviour {

	public GameObject sampleButton;
	public Transform contentPanel;
	public List<item> itemList;

	void Start()
	{
		PopulateList ();
	}

	void PopulateList()
	{
		foreach (var item in itemList) {
			GameObject newBtn = Instantiate (sampleButton) as GameObject;
			SampleBtn buttonScript = newBtn.GetComponent<SampleBtn> ();
			buttonScript.grade.text = item.grade;
			buttonScript.userName.text = item.userName;
			buttonScript.score.text = item.score;

			newBtn.transform.SetParent (contentPanel);
	}
}
}
 