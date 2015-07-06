using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {

	public GameObject[] Panels;

	public void OnMouseDown() {

		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive(false);
		}

		Invoke ("TakePicture", 2);
	}

	void TakePicture() {
		Application.CaptureScreenshot("C:/Users/Gabriel/Pictures/Kinect/Screenshot.png");
		Invoke ("SetPanelsActive", 2);
	}

	void SetPanelsActive() {
		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive(true);
		}
	}
}
