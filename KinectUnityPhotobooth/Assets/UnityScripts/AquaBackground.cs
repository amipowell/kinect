using UnityEngine;
using System.Collections;

public class AquaBackground : MonoBehaviour {

	public Material material;
	public Renderer rend;
	
	public void ChangeBackground () {
		rend.material = material;
	}
}
