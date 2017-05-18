using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DB_Handler : MonoBehaviour
{
	//get the display property equals to 1
	public string highscoreURL = "http://hanslen.me/Hybrid_Desktop/getOnlineText.php";
	public GameObject annotation;
	public GameObject title;
	public class onlineannotation{
		public int id;
		public string username;
		public string text;
		public string subject;
		public int show;
	}

	private onlineannotation myannotation = new onlineannotation();

	void Start()
	{
		annotation.GetComponent<Text> ().text = "Loading...";
		StartCoroutine(GetScores(50.0f));	
		

	}

	//get annotation
	IEnumerator GetScores(float waitTime)
	{
		while (true) {
			WWW hs_get = new WWW (highscoreURL);
			yield return hs_get;

			if (hs_get.error != null) {
				print ("There was an error getting the high score: " + hs_get.error);
			} else {
				myannotation = JsonUtility.FromJson<onlineannotation> (hs_get.text);
				annotation.GetComponent<Text> ().text = myannotation.text;
				title.GetComponent<Text> ().text = myannotation.subject;
			}
		}
	}

}