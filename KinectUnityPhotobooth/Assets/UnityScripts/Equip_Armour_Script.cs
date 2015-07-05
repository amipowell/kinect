using UnityEngine;
using System.Collections;

public class Equip_Armour_Script : MonoBehaviour {

	public GameObject[] Armour;
	public GameObject[] Egypt;
	
	void Start () {
	}

	public void EquipArmour () {
		Egypt = GameObject.FindGameObjectsWithTag ("Egypt");

		for (int i = 0; i < Egypt.Length; i++) {
			Egypt[i].SetActive(false);
		}

		for (int i = 0; i < Armour.Length; i++) {
			Armour[i].SetActive(true);
		}
	}
}
