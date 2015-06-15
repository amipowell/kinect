using UnityEngine;
using System.Collections;

public class ChangeBackground : MonoBehaviour {

	public Material material;
	public Renderer rend;
	
	public void ChangeBackgroundTo () {
		rend.material = material;
	}
}