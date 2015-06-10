using UnityEngine;
using System.Collections;

public class ChangeBackground : MonoBehaviour {

	public Material[] textures;
	public Renderer rend;

	private int index = 0;
	
	void Start() {
		rend = GetComponent<Renderer>();
	}
	
	void Update() {
		if (textures.Length == 0)
			return;
		if (Input.GetButtonDown ("Fire1")) {
			index++;
			index = index % textures.Length;
			renderer.material = textures[index];
		}
	}
}