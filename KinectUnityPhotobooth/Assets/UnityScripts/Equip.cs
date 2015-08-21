using UnityEngine;
using System.Collections;

public class Equip : MonoBehaviour {

	public string[] Tags;

	private GameObject[][] EquipArray = new GameObject[2][];
	private string ActiveTag, PrevTag;

	void Start () {
		for (int i = 0; i < Tags.Length; i++) {
			EquipArray[i] = GameObject.FindGameObjectsWithTag(Tags[i]);
		}
		ActiveTag = "Egypt";
		ChangeEquip ("Mexico");
	}

	public void ChangeEquip (string Tag) {
		PrevTag = ActiveTag;
		ActiveTag = Tag;

		for (int i = 0; i < Tags.Length; i++) {
			if (Tags[i] == Tag){
				EquipObjects (i, true);
			} else if (Tags[i] == PrevTag){
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