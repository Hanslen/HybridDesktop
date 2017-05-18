using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;
using Image=UnityEngine.UI.Image;
using System.Collections.Generic;
using System.Linq;
public class nextScript : MonoBehaviour, IVirtualButtonEventHandler {
	public List<Sprite> mleStorage = new List<Sprite>();
	public List<Sprite> pecStorage = new List<Sprite>();
	public static List<List<Sprite>> pdfStorage = new List<List<Sprite>> ();
	GameObject nextButton;
//	GameObject backButton;
	GameObject switchPdfButton;
	public static Image pdfImg = null;
	public static int pdfindex = 0;
	public static int index = 0;

	public GameObject course1;
	public GameObject course2;
	public static List<GameObject> pdfTitle = new List<GameObject> ();


//	MLE
	public Sprite image1;
	public Sprite image2;
	public Sprite image3;
	public Sprite image4;
	public Sprite image5;
	public Sprite image6;
	public Sprite image7;
	public Sprite image8;
	public Sprite image9;
	public Sprite image10;
	public Sprite image11;
	public Sprite image12;
	public Sprite image13;
	public Sprite image14;
	public Sprite image15;
	public Sprite image16;

//	PEC
	public Sprite pecimage1;
	public Sprite pecimage2;
	public Sprite pecimage3;
	public Sprite pecimage4;
	public Sprite pecimage5;
	public Sprite pecimage6;
	public Sprite pecimage7;
	public Sprite pecimage8;
	public Sprite pecimage9;
	public Sprite pecimage10;




	// Use this for initialization
	void Start () {
		course1.GetComponent<Text> ().text = "->" + course1.GetComponent<Text> ().text;
		nextButton = GameObject.Find ("nextButton");
//		backButton = GameObject.Find ("backButton");
		switchPdfButton = GameObject.Find ("switchPdfButton");
		nextButton.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
//		backButton.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
		switchPdfButton.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
		mleStorage.Add (image1);
		mleStorage.Add (image2);
		mleStorage.Add (image3);
		mleStorage.Add (image4);
		mleStorage.Add (image5);
		mleStorage.Add (image6);
		mleStorage.Add (image7);
		mleStorage.Add (image8);
		mleStorage.Add (image9);
		mleStorage.Add (image10);
		mleStorage.Add (image11);
		mleStorage.Add (image12);
		mleStorage.Add (image13);
		mleStorage.Add (image14);
		mleStorage.Add (image15);
		mleStorage.Add (image16);
		pdfStorage.Add (mleStorage);

		pecStorage.Add (pecimage1);
		pecStorage.Add (pecimage2);
		pecStorage.Add (pecimage3);
		pecStorage.Add (pecimage4);
		pecStorage.Add (pecimage5);
		pecStorage.Add (pecimage6);
		pecStorage.Add (pecimage7);
		pecStorage.Add (pecimage8);
		pecStorage.Add (pecimage9);
		pecStorage.Add (pecimage10);
		pdfStorage.Add (pecStorage);

		pdfTitle.Add (course1);
		pdfTitle.Add (course2);

	}
		
	//rewrite virtual button press
	public void OnButtonPressed (VirtualButtonAbstractBehaviour vb){
		Debug.Log ("Pressed..");
		switch (vb.VirtualButtonName) {
		case "nextButton":
			Debug.Log ("Pressed..Nextbutton");
			switchNext();
			break;
		case "backButton":
			Debug.Log ("Pressed..BackButton");
			switchPrevious ();
			break;
		case "switchPdfButton":
			Debug.Log ("Pressed..SwitchButton");
			if (pdfindex < 1) {
				pdfindex++;

			} else {
				pdfindex = 0;
			}
			index = 0;


			pdfTitle [pdfindex].GetComponent<Text> ().text = "->" + pdfTitle [pdfindex].GetComponent<Text> ().text;	
			if (pdfindex == 0) {
				pdfTitle [1].GetComponent<Text> ().text = pdfTitle [1].GetComponent<Text> ().text.Substring(2);	//update the last one
			}else{
				pdfTitle [pdfindex-1].GetComponent<Text> ().text = pdfTitle [pdfindex-1].GetComponent<Text> ().text.Substring(2);	//update the last one

			}

			pdfImg = GameObject.Find ("pdfReader").GetComponent<Image> ();
			pdfImg.sprite = pdfStorage [pdfindex].ElementAt (index);		break;
			default:throw new UnityException("Button not supported: " + vb.VirtualButtonName);
				break;
		}
	}

	//switch the pdf to the previous slide
	public static void switchPrevious(){
		if (index>0) {
			index--;
		} else {
			index = pdfStorage [pdfindex].Count-1;
		}
		pdfImg = GameObject.Find ("pdfReader").GetComponent<Image> ();
//		Debug.Log (pdfindex);
//		Debug.Log (pdfStorage.Count);
		Debug.Log (index); 
		pdfImg.sprite = pdfStorage[pdfindex].ElementAt(index);
	}

	//switch the pdf to the next slide
	public static void switchNext(){
		if (pdfStorage [pdfindex].Count-1 > index) {
			index++;
		} else {
			index = 0;
		}
		pdfImg = GameObject.Find ("pdfReader").GetComponent<Image> ();
//		Debug.Log (pdfindex);
//		Debug.Log (pdfStorage.Count);
		Debug.Log (index);
		pdfImg.sprite = pdfStorage[pdfindex].ElementAt(index);
	}

	public void OnButtonReleased (VirtualButtonAbstractBehaviour vb){

		Debug.Log ("Release!");
	}
}
