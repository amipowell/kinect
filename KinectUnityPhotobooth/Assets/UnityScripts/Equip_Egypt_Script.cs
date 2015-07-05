using UnityEngine;
using System.Collections;

public class Equip_Egypt_Script : MonoBehaviour {

	public GameObject[] Armour;
	public GameObject[] Egypt;
	
	void Start () {
	}

	public void EquipEgypt () {
		Armour = GameObject.FindGameObjectsWithTag ("Armour");

		for (int i = 0; i < Armour.Length; i++) {
			Armour[i].SetActive(false);
		}
		
		for (int i = 0; i < Egypt.Length; i++) {
			Egypt[i].SetActive(true);
		}
	}
}
