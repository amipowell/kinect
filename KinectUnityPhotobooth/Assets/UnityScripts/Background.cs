using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public Material[] materials;
	public Renderer background;
	
	public void DinoBackground () {
		background.material = materials[0];
	}

	public void EgyptBackground () {
		background.material = materials[1];
	}

	public void AquaBackground () {
		background.material = materials[2];
	}

	public void MammothBackground () {
		background.material = materials[3];
	}
}
