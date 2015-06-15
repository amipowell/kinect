using UnityEngine;
using System.Collections;

public class EgyptBackground : MonoBehaviour {

	public Material material;
	public Renderer rend;
	
	public void ChangeBackground () {
		rend.material = material;
	}
}