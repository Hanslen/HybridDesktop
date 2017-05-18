using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;


public class Client : MonoBehaviour {

	System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
	private Thread oThread;

	//	for UI update
	private bool getnewinfo;
	private string tempMesg;
//	private GameObject testmove;
	public GameObject clockWise;
	public GameObject counterClockWise;
	public GameObject swipe;
	public Material red;
	public Material green;
	int status = 1;


	// Use this for initialization
	void Start () {
		getnewinfo = false;
		red = Resources.Load("red", typeof(Material)) as Material;
		green = Resources.Load("green", typeof(Material)) as Material;
//		testmove =GameObject.Find("testmove");
//		Debug.Log("CLient class:");
		clientSocket.Connect("10.154.144.189", 8888);
		oThread = new Thread (new ThreadStart (getInformation));
		oThread.Start ();
	}
		
	// Update is called once per frame
	void Update () {
		// update the UI by Leapmotion Message
		if (getnewinfo) {

			//clockwise gesture is making the PDF reader bigger
			if (tempMesg == "clockwise") {
//				testmove.transform.position = new Vector3 (-1, 0, 3); 
				Debug.Log ("Make the screen bigger~");
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var imagetransform = pdfreader.transform as RectTransform;
				var tempGetWidthHeight = pdfreader.GetComponent<RectTransform> ().rect;
				if (tempGetWidthHeight.width > 14) {
					GameObject msgforlost = GameObject.Find ("msg");
					msgforlost.GetComponent<Text> ().text = "PDF reader size reaches the Max.";
					Debug.Log ("Max.. So it cannot expand any more...");
				} else {
					float width = (float)tempGetWidthHeight.width + (float)0.07;
					float height = (float)tempGetWidthHeight.height + (float)0.04;
					imagetransform.sizeDelta = new Vector2 (width, height);
				}
//				imagetransform.sizeDelta = new Vector2 (7, 4);

			} else if (tempMesg == "counterclockwise") {
				//counter clockwise is making the PDF reader smaller
//				testmove.transform.position = new Vector3 (1, 0, 3); 
				Debug.Log ("Make the screen smaller~");
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var imagetransform = pdfreader.transform as RectTransform;
				//imagetransform.sizeDelta = new Vector2 (3, 2);
				var tempGetWidthHeight = pdfreader.GetComponent<RectTransform> ().rect;
				if (tempGetWidthHeight.width < 3) {
					GameObject msgforlost = GameObject.Find ("msg");
					msgforlost.GetComponent<Text> ().text = "PDF reader size reaches the Min.";
					Debug.Log ("Min.. So it cannot smaller any more...");
				} else {
					float width = (float)tempGetWidthHeight.width - (float)0.07;
					float height = (float)tempGetWidthHeight.height - (float)0.04;
					imagetransform.sizeDelta = new Vector2 (width, height);
				}
			} else if (tempMesg == "Previous") {
				//switching to the previous pdf
				Debug.Log ("Previous....");
				nextScript.switchPrevious ();
			} else if (tempMesg == "Next") {
				//switch to the next pdf
				Debug.Log ("Next...");
				nextScript.switchNext ();
			} else if (tempMesg == "updateinstruction") {
				//enable and disable gestures
				if (status == 1) {
					status = 2;	
					clockWise.GetComponent<Renderer> ().material = green;
					counterClockWise.GetComponent<Renderer> ().material = green;
					swipe.GetComponent<Renderer> ().material = red;
				} else {
					status = 1;
					clockWise.GetComponent<Renderer> ().material = red;
					counterClockWise.GetComponent<Renderer> ().material = red;
					swipe.GetComponent<Renderer> ().material = green;
				}
			} else if (tempMesg == "MoveBack") {
				//moving pdf to the back
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var help = pdfreader.transform.position;
				Debug.Log (help.x);
				pdfreader.transform.position = new Vector3 (help.x + 0.2f, help.y, help.z);
			} else if (tempMesg == "MoveFront") {
				//moving pdf to the front
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var help = pdfreader.transform.position;
				Debug.Log (help.x);
				pdfreader.transform.position = new Vector3 (help.x - 0.2f, help.y, help.z);
			} else if (tempMesg == "MoveLeft") {
				//moving pdf to the left
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var help = pdfreader.transform.position;
				Debug.Log (help.x);
				pdfreader.transform.position = new Vector3 (help.x, help.y, help.z + 0.2f);
			} else if (tempMesg == "MoveRight") {
				//moving pdf to the right
				GameObject pdfreader = GameObject.Find ("pdfReader");
				var help = pdfreader.transform.position;
				Debug.Log (help.x);
				pdfreader.transform.position = new Vector3 (help.x, help.y, help.z - 0.2f);
			} 
			else if(tempMesg[0] == 'X'){
				//detail information about the position of hand
				GameObject msgforlost = GameObject.Find ("msg");
				msgforlost.GetComponent<Text> ().text = tempMesg+"Z < -50: Back; Z > 110: Front; X < -80: Left; X > 80: Right";
			}
			else {
				//updateing progress
				GameObject msgforlost = GameObject.Find ("msg");
				msgforlost.GetComponent<Text> ().text = "You want to change gestures. Loading progess-"+tempMesg;
			}
			getnewinfo = false;
		}
	}

	//get the client information
	void getInformation(){
		while (true) {
			try {
					NetworkStream networkStream = clientSocket.GetStream ();
					byte[] bytesFrom = new byte[10025];
					networkStream.Read (bytesFrom, 0, (int)bytesFrom.Length);
					string dataFromClient = System.Text.Encoding.ASCII.GetString (bytesFrom);
					dataFromClient = dataFromClient.Substring (0, dataFromClient.IndexOf ("$"));
					if(tempMesg == "Next" || tempMesg == "Previous"){
						System.Threading.Thread.Sleep(1000);
						dataFromClient = "";
						tempMesg = "";
					}
					
					Debug.Log (" >> Data from Server - " + dataFromClient);
					string serverResponse = "Last Message from Server" + dataFromClient;
					getnewinfo = true;
					tempMesg = dataFromClient;
					
					Byte[] sendBytes = Encoding.ASCII.GetBytes (serverResponse);
					networkStream.Write (sendBytes, 0, sendBytes.Length);
					networkStream.Flush ();
					Debug.Log (" >> " + serverResponse);
//					GameObject msgforlost = GameObject.Find ("msg");
//					msgforlost.GetComponent<Text> ().text = "I recoginize that you are performming a "+tempMesg+" gesture.";
				

			} catch (Exception ex) {
				Debug.Log ("Exception error:" + ex.ToString ());
				oThread.Abort ();
				oThread.Join ();
			}
		}
	}
}
