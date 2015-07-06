using UnityEngine;
using System.Collections;

public class Equip : MonoBehaviour {

	public GameObject[] Armour, Egypt;

	private GameObject[][] EquipArray = new GameObject[2][];
	private GameObject[] ActiveEquip, PrevEquip;

	void Start () {
		EquipArray [0] = Armour;
		EquipArray [1] = Egypt;
		PrevEquip = Armour;
	}

	public void EquipArmour () {
		PrevEquip = ActiveEquip;
		ActiveEquip = Armour;
		EquipAvatar ();
	}

	public void EquipEgypt () {
		PrevEquip = ActiveEquip;
		ActiveEquip = Egypt;
		EquipAvatar ();
	}

	void EquipAvatar () {
		for (int i = 0; i < EquipArray.Length; i++) {
			if (EquipArray[i] == ActiveEquip){
				EquipObjects (i, true);
			} else if (EquipArray[i] == PrevEquip){
				EquipObjects (i, false);
			}
		}
	}

	void EquipObjects (int index, bool active) {
		for (int obj = 0; obj < EquipArray[index].Length; obj++) {
			EquipArray[index][obj].SetActive (active);
		}
	}
}