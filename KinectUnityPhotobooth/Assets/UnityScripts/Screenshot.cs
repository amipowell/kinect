using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Screenshot : MonoBehaviour {

	public GameObject[] Panels;
	public GameObject TweetMenu, SendMenu, SuccessText, GestureInfo;
	public InputField UserEmail;
	public Image Picture;
	public Text CountdownText;
	public int picWidth, picHeight;

	private Sprite PictureSprite;
	private Texture2D tex;
	private byte[] fileData;
	private int time;

	private MailMessage email;
	private SmtpClient smtpServer;

	private string from, screenshotLocation;

	void Start () {
		CountdownText.text = "";
		from = "gab.bussieres@gmail.com";
		screenshotLocation = "Assets/Resources/Screenshot.png";

		email = new MailMessage();
		email.From = new MailAddress(from);
		email.Subject = "ROM Photobooth";
		email.Body = "This is a test mail from C# program";

		smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential(from, "FAKEpassword") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
	}

	public void OnMouseDown() {
		GestureInfo.SetActive (false);
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
		Application.CaptureScreenshot(screenshotLocation);
		Invoke ("Menu", 0.5f);
	}

	void Menu() {
		if (File.Exists(screenshotLocation)) {
			fileData = File.ReadAllBytes(screenshotLocation);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		PictureSprite = Sprite.Create (tex, new Rect(0, 0, picWidth, picHeight), new Vector2(0, 0));
		Picture.sprite = PictureSprite;
		TweetMenu.SetActive (true);
		GestureInfo.SetActive (true);
	}

	public void Email() {
		TweetMenu.SetActive (false);
		SuccessText.SetActive (false);
		SendMenu.SetActive (true);
	}

	public void Send() {
		SuccessText.SetActive (false);
		
		email.To.Add(UserEmail.text);
		email.Attachments.Add(new Attachment(screenshotLocation));
		
		smtpServer.Send(email);

		email.To.Clear ();
		email.Attachments.Clear ();

		SuccessText.SetActive (true);
	}
	
	public void Back() {
		UserEmail.text = "";
		SendMenu.SetActive (false);
		TweetMenu.SetActive (true);
	}
	
	public void Retake() {
		TweetMenu.SetActive (false);
		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].SetActive (true);
		}
	}
}
