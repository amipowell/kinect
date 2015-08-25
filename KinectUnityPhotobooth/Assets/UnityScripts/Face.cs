using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.IO;

public class Face : MonoBehaviour {

	public Renderer rendSquare, rendCircle;
	public GameObject FaceMenu;
	public int picWidth, picHeight;
	public Image Picture;

	private string faceLocation;
	private Sprite PictureSprite;
	private Texture2D tex;
	private byte[] fileData;

	void Start (){
		faceLocation = "Assets/Resources/Face.png";
	}

	public void ShowFaceMenu (){;
		FaceMenu.SetActive (true);
	}

	public void Accept (){
		Application.CaptureScreenshot(faceLocation);

		if (File.Exists(faceLocation)) {
			fileData = File.ReadAllBytes(faceLocation);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}

		PictureSprite = Sprite.Create (tex, new Rect(615, 300, picWidth, picHeight), new Vector2(0, 0));
		Picture.sprite = PictureSprite;

		var faceTexture = new Texture2D(picWidth, picHeight);
		var pixels = Picture.sprite.texture.GetPixels(615, 300, picWidth, picHeight);
		faceTexture.SetPixels(pixels);
		faceTexture.Apply();

		rendSquare.material.mainTexture = faceTexture;
		rendCircle.material.mainTexture = faceTexture;
		Invoke("Back", 1);
	}

	public void Back (){
		FaceMenu.SetActive (false);
	}
}
