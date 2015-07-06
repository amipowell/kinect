using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Screenshot : MonoBehaviour {

	public GameObject[] Panels;
	public GameObject TweetMenu;
	public Sprite PictureSprite;
	public Image Picture;
	public Texture2D tex;
	public byte[] fileData;

	public void OnMouseDown() {
		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive(false);
		}
		Invoke ("TakePicture", 3);
	}

	void TakePicture() {
		Application.CaptureScreenshot("C:/Users/Gabriel/Pictures/Kinect/Screenshot.png");
		Invoke ("Menu", 1);
	}

	void Menu() {
		if (File.Exists("C:/Users/Gabriel/Pictures/Kinect/Screenshot.png")) {
			fileData = File.ReadAllBytes("C:/Users/Gabriel/Pictures/Kinect/Screenshot.png");
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		PictureSprite = Sprite.Create (tex, new Rect(0, 0, 1352, 675), new Vector2(0, 0));
		Picture.sprite = PictureSprite;
		TweetMenu.SetActive (true);
	}

	public void Tweet() {

	}

	public void Retake() {
		TweetMenu.SetActive (false);
		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive (true);
		}
	}
}
