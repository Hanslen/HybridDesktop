using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class bulletInData : MonoBehaviour {

	public string bulletWeb = "http://hanslen.me/Hybrid_Desktop/getTasks.php";
	public string pythonData = "http://hanslen.me:8080/introduction";
	public GameObject taskVal;
	public GameObject weatherVal;
	public GameObject tempVal;
	public GameObject dateVal;

	[System.Serializable]
	public class oneTask{
		public string id;
		public string username;
		public string task;
		public string category;
		public string time;
		public bool status;

	}

	void Start()
	{
		taskVal.GetComponent<Text> ().text = "Loading data....";
		StartCoroutine(getdata(50.0f));	

	}

	// this function will be used to update the bulletin board, update weather information and task list
	IEnumerator getdata(float waitTime)
	{
		while (true) {
			WWW hs_get = new WWW (bulletWeb);
			yield return hs_get;

			if (hs_get.error != null) {
				print ("There was an error getting data: " + hs_get.error);
			}
			string[] tempTasks = hs_get.text.Substring (1, hs_get.text.Length - 2).Split ('}');
			for (int i = 0; i < tempTasks.Length - 1; i++) {
				tempTasks [i] = tempTasks [i] + "}";
				if (i != 0) {
					tempTasks [i] = tempTasks [i].Substring (1, tempTasks [i].Length - 1);
				}

			}
			string outputTasks = "";
			for (int i = 0; i < tempTasks.Length - 1; i++) {
				oneTask temp = JsonUtility.FromJson<oneTask> (tempTasks [i]);
				if (temp.status == false) {
					outputTasks = outputTasks + (i+1).ToString() + ": " + temp.task + "\n";
				}
			}
			taskVal.GetComponent<Text> ().text = outputTasks;
			Debug.Log (outputTasks);


			//python server part
			WWW hs_get2 = new WWW (pythonData);
			yield return hs_get2;

			if (hs_get2.error != null) {
				print ("There was an error getting data: " + hs_get2.error);
			}
			string[] pythonDate = hs_get2.text.Split (' ');
			try {
				dateVal.GetComponent<Text> ().text = pythonDate [0] + System.DateTime.Now.ToString ("MM.dd");
				weatherVal.GetComponent<Text> ().text = pythonDate [1];
				tempVal.GetComponent<Text> ().text = pythonDate [2] + "℃";
			} catch (Exception e) {
				Debug.Log (e);
			}
		}
	}
}
