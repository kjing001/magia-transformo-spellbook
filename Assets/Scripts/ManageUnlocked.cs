using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageUnlocked : MonoBehaviour {

	public GameObject[] spells;
	DisplayCanvas manager;

	void OnEnable() {
		manager = GameObject.FindGameObjectWithTag ("manager").GetComponent<DisplayCanvas>();

		foreach (GameObject spell in spells) {
			spell.GetComponent<SetSpellData> ().DisplaySpell (false);
		}

		foreach (int i in manager.unlocked_spells) {
			spells [i].GetComponent<SetSpellData> ().DisplaySpell (true);
		}

	}
}
