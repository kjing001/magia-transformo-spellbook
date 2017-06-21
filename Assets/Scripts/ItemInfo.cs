using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour {
	DisplayCanvas manager;
	Animator anim;
	public Text item_name;
	public Text item_story;

	static Dictionary<string,string> stories = new Dictionary<string, string> {
		{ "Fire Hat", "this is the fire hat story" },
		{ "Fire Cloak", "this is the fire cloak story" },
		{ "Water Hat", "this is the water hat story" },
		{ "Water Cloak", "this is the water cloak story" },
		{ "Earth Hat", "this is the earth hat story" },
		{ "Earth Cloak", "this is the earth cloak story" },
		{ "Air Hat", "this is the air hat story" },
		{ "Air Cloak", "this is the air cloak story" },
		{ "Dark Hat", "this is the dark hat story" },
		{ "Dark Cloak", "this is the dark cloak story" },
		{ "Energy Hat", "this is the energy hat story" },
		{ "Energy Cloak", "this is the energy cloak story" },

		{ "Fire Witch", "this is the fire witch story" },
		{ "Water Witch", "this is the water witch story" },
		{ "Earth Witch", "this is the earth witch story" },
		{ "Air Witch", "this is the air witch story" },
		{ "Dark Witch", "this is the dark witch story" },
		{ "Energy Witch", "this is the energy witch story" },
		{ "Steam Witch", "this is the steam witch story" },
		{ "Lava Witch", "this is the lava witch story" },
		{ "Fire Air Witch", "this is the fire air witch story" },
		{ "Dark Fire Witch", "this is the dark fire witch story" },
		{ "Fire Energy Witch", "this is the fire energy witch story" },
		{ "Mud Witch", "this is the mud witch story" },
		{ "Rain Witch", "this is the rain witch story" },
		{ "Dark Water Witch", "this is the dark water witch story" },
		{ "Water Energy Witch", "this is the water energy witch story" },
		{ "Dust Witch", "this is the dust witch story" },
		{ "Dark Earth Witch", "this is the dark earth witch story" },
		{ "Gem Witch", "this is the gem witch story" },
		{ "Tornado Witch", "this is the tornado witch story" },
		{ "Wind Witch", "this is the wind witch story" },
		{ "Dark Energy Witch", "this is the dark energy witch story" }
	};

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		manager = GameObject.FindGameObjectWithTag ("manager").GetComponent<DisplayCanvas>();
	}


	public void ShowWindow(string item) {
		if (item == "close") {
			anim.SetBool ("showBox", false);
		} else {
			if (item == "hat")
				item = manager.getHat();
			else if (item == "cloak")
				item = manager.getCloak();
			else if (item == "witch")
				item = manager.getWitch();
			
			anim.SetBool ("showBox", true);
			item_name.text = item;
			item_story.text = stories [item];
		}
	}

}
