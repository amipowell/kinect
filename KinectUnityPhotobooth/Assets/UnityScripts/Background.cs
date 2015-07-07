using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	
	public Renderer background;
	
	public void ChangeBackground (Material material) {
		background.material = material;
	}
}