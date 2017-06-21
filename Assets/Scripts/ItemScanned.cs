using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScanned : MonoBehaviour {

	// gameobjects for various items
	public GameObject witch_name;
	public GameObject hat_image;
	public GameObject cloak_image;
	public GameObject hat_symbol;
	public GameObject cloak_symbol;

	DisplayCanvas manager;
    public Text showMsg;

	void OnEnable() {
		manager = GameObject.FindGameObjectWithTag ("manager").GetComponent<DisplayCanvas>();
	}
    private void Start()
    {
        ShowDefaultItems();
    }
    void Update() {
		
		ShowCostume(manager.getHat());
		ShowCostume(manager.getCloak());	
		SetWitchName(manager.getWitch());

        showMsg.text = manager.getMsg();
	}
    		
	public void ShowCostume(string costume_name) {
        // costume_name in the format "Element ItemType" (ex: "Fire Cloak")
        // call this method using value of string 'itemScanned' from WitchSelect
        if (costume_name == "Null Hat")
        {
            ShowDefaultHat();
        }
        else if (costume_name == "Null Cloak")
        {
            ShowDefaultCloak();
        }
        else
        {
            string[] costume = costume_name.Split(" "[0]);

            if (costume[1] == "Hat")
            {                
                hat_image.GetComponent<Image>().sprite = manager.hats[manager.ElementToInt(costume[0])];
                hat_symbol.GetComponent<Image>().sprite = manager.symbols[manager.ElementToInt(costume[0])];             
            }
            else if (costume[1] == "Cloak")
            {
                cloak_image.GetComponent<Image>().sprite = manager.cloaksM[manager.ElementToInt(costume[0])];
                cloak_symbol.GetComponent<Image>().sprite = manager.symbols[manager.ElementToInt(costume[0])];
            }
        }
            
	}

    public void SetWitchName(string s)
    {
        witch_name.GetComponent<Text>().text = s;
    }
    public void ShowDefaultHat()
    {
        hat_image.GetComponent<Image>().sprite = manager.default_hat;
        hat_symbol.GetComponent<Image>().sprite = manager.default_symbol;
    }
    public void ShowDefaultCloak()
    {
        cloak_image.GetComponent<Image>().sprite = manager.default_cloakM;   // cloakM
        cloak_symbol.GetComponent<Image>().sprite = manager.default_symbol;
    }
    public void ShowDefaultItems()
    {
        ShowDefaultHat();
        ShowDefaultCloak();
        SetWitchName("Null Witch");
    }
}
