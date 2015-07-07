using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Screenshot : MonoBehaviour {

	public GameObject[] Panels;
	public GameObject TweetMenu;
	public Image Picture;
	public Text CountdownText;

	private Sprite PictureSprite;
	private Texture2D tex;
	private byte[] fileData;
	private int time;

	void Start () {
		CountdownText.text = "";
	}

	public void OnMouseDown() {
		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive(false);
		}
		time = 3;
		Countdown ();
	}

	void Countdown(){
		if (time > 0) {
			CountdownText.text = time.ToString();
			time--;
			Invoke ("Countdown", 1);
		} else {
			CountdownText.text = "Smile!";
			Invoke ("TakePicture", 0.5f);
		}
	}

	void TakePicture() {
		CountdownText.text = "";
		Application.CaptureScreenshot("Assets/Resources/Screenshot.png");
		Invoke ("Menu", 0.5f);
	}

	void Menu() {
		if (File.Exists("Assets/Resources/Screenshot.png")) {
			fileData = File.ReadAllBytes("Assets/Resources/Screenshot.png");
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
