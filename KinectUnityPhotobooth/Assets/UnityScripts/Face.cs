using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.IO;

public class Face : MonoBehaviour {

	public GameObject[] Panels, FaceMenuButtons;
	public Renderer FaceObject;
	public GameObject FaceMenu, FaceLocationPanel;
	public int picPosX, picPosY, picWidth, picHeight;
	public Image Picture;
	public Text CountdownText;

	private string faceLocation;
	private Sprite PictureSprite;
	private Texture2D tex;
	private byte[] fileData;
	private int time;

	void Start (){
		faceLocation = "Assets/Resources/Face.png";
		if (!File.Exists (faceLocation)) {
			ShowFaceMenu ();
		}
	}

	public void ShowFaceMenu (){;
		SetArrayActive (Panels, false);
		FaceLocationPanel.SetActive (true);
		FaceMenu.SetActive (true);
	}

	public void Accept (){
		SetArrayActive (FaceMenuButtons, false);
		time = 3;
		Countdown ();
	}

	void Countdown(){
		if (time > 0) {
			CountdownText.text = time.ToString();
			time--;
			Invoke ("Countdown", 0.5f);
		} else {
			FaceLocationPanel.SetActive(false);
			CountdownText.text = "Smile!";
			Invoke ("TakePicture", 0.5f);
		}
	}
	public void TakePicture (){
		Application.CaptureScreenshot(faceLocation);
		Invoke ("CreateFace", 0.5f);
	}

	public void CreateFace (){

		if (File.Exists(faceLocation)) {
			fileData = File.ReadAllBytes(faceLocation);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}

		PictureSprite = Sprite.Create (tex, new Rect(picPosX, picPosY, picWidth, picHeight), new Vector2(0, 0));
		Picture.sprite = PictureSprite;

		var faceTexture = new Texture2D(picWidth, picHeight);
		var pixels = Picture.sprite.texture.GetPixels(picPosX, picPosY, picWidth, picHeight);
		faceTexture.SetPixels(pixels);
		faceTexture.Apply();

		FaceObject.material.mainTexture = faceTexture;
		Invoke("Back", 1);
	}

	public void Back (){
		FaceMenu.SetActive (false);
		CountdownText.text = "Please put your face in the rectangle.";
		SetArrayActive (FaceMenuButtons, true);
		SetArrayActive (Panels, true);
	}

	void SetArrayActive (GameObject[] objs, bool active){
		for (int i = 0; i < objs.Length; i++) {
			objs[i].SetActive (active);
		}
	}
}
