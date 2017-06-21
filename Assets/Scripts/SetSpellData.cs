using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpellData : MonoBehaviour {

	// hats and cloaks that make up the given spell
	public Sprite[] hats;
	public Sprite[] cloaks;
	public GameObject[] hatObjects;
	public GameObject[] cloakObjects;
	public GameObject spell_image;

	public Sprite default_hat;
	public Sprite default_cloak;

	
	public void DisplaySpell(bool display) {
		if (display) {
			for (int i = 0; i < 3; i++) {
				hatObjects [i].GetComponent<Image> ().sprite = hats [i];
				cloakObjects [i].GetComponent<Image> ().sprite = cloaks [i];
			}
			spell_image.SetActive (true);
		} else {
			for (int i = 0; i < 3; i++) {
				hatObjects [i].GetComponent<Image> ().sprite = default_hat;
				cloakObjects [i].GetComponent<Image> ().sprite = default_cloak;
			}
			spell_image.SetActive (false);
		}

	}
}
