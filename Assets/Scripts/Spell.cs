using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour {

	DisplayCanvas manager;

	public Image background_color;

    public GameObject arrived;
    public GameObject counter_cloakwise;
    public GameObject cloakwise;
    public GameObject spellbookIcon;
    public GameObject checkpoint;
    

    public GameObject hat_image;
	public GameObject cloak_image;
	public GameObject hat_symbol;
	public GameObject cloak_symbol;

    public GameObject spinLayer;
    public GameObject cLayer;
    public GameObject ccLayer;
    public GameObject bookLayer;


    // used to determine background colors based on player numbers
    static Dictionary<int,Color> background = new Dictionary<int, Color> {
		{1, Color.red},
		{2, Color.yellow},
		{3, Color.green},
	};

	// Use this for initialization
	void OnEnable () {
		manager = GameObject.FindGameObjectWithTag ("manager").GetComponent<DisplayCanvas>();

        background_color.color = background[manager.player_num];

        cLayer.SetActive(false);
        spinLayer.SetActive(false);
        ccLayer.SetActive(false);
        bookLayer.SetActive(false);
    }

    void OnDisable()
    {
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<DisplayCanvas>();

        background_color.color = background[manager.player_num];

        cLayer.SetActive(false);
        spinLayer.SetActive(false);
        ccLayer.SetActive(false);
        bookLayer.SetActive(false);

    }

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<DisplayCanvas>();
        background_color.color = background[manager.player_num];
        cLayer.SetActive(false);
        spinLayer.SetActive(false);
        ccLayer.SetActive(false);
        bookLayer.SetActive(false);
       
    }

    void Update() {
		
        ShowCostumes();

        ShowNavigations();
        
    }



    void ShowNavigations()
    {
        // show player icon and checkpoints
        string nav = manager.getNav();
        if (nav == "")
            return;
        if (nav.Substring(0,3) == "cur")
        {
            bookLayer.SetActive(true);
            spinLayer.SetActive(true);
            cLayer.SetActive(false);
            spinLayer.SetActive(false);
            ccLayer.SetActive(false);

            //curPos1disPos1
            string c = nav.Substring(6, 1);
            string d = nav.Substring(13, 1);
            
            if (c == "1")
                spellbookIcon.GetComponent<RectTransform>().localPosition = new Vector2(200, 100);
            else if (c == "2")
                spellbookIcon.GetComponent<RectTransform>().localPosition = new Vector2(-200, 100);
            else if (c == "3")
                spellbookIcon.GetComponent<RectTransform>().localPosition = new Vector2(200, -250);
            else if (c == "4")
                spellbookIcon.GetComponent<RectTransform>().localPosition = new Vector2(-200, -250);
            else // curPos: -1
                bookLayer.SetActive(false);

            if (d == "1")
                checkpoint.GetComponent<RectTransform>().localPosition = new Vector2(200, 100);
            else if (d == "2")
                checkpoint.GetComponent<RectTransform>().localPosition = new Vector2(-200, 100);
            else if (d == "3")
                checkpoint.GetComponent<RectTransform>().localPosition = new Vector2(200, -250);
            else if (d == "4")
                checkpoint.GetComponent<RectTransform>().localPosition = new Vector2(-200, -250);

            if (c == d)
            {
                StartCoroutine(checkpointArrived());
            }
        }
        else 
        {
            bookLayer.SetActive(false);
            spinLayer.SetActive(false);
        }

        if(nav == "Clockwise")
        {
            cLayer.SetActive(true);
            spinLayer.SetActive(false);
            ccLayer.SetActive(false);
        }
        else if (nav == "Counter-clockwise")
        {
            cLayer.SetActive(false);
            spinLayer.SetActive(false);
            ccLayer.SetActive(true);
        }
        else if (nav == "Arrived")
        {
            cLayer.SetActive(false);
            spinLayer.SetActive(true);
            ccLayer.SetActive(false);
            StartCoroutine(checkpointArrived());
        }
        else
        {
            cLayer.SetActive(false);
            spinLayer.SetActive(false);
            ccLayer.SetActive(false); 
        }
    }

    IEnumerator checkpointArrived()
    {
        yield return new WaitForSeconds(2);
    }

    public void ShowCostumes()
    {
        
        if (manager.getHat() != "Null Hat")
        {
            hat_image.GetComponent<Image>().sprite = manager.hats[manager.ElementToInt(manager.getHat() )];
            hat_symbol.GetComponent<Image>().sprite = manager.symbols[manager.ElementToInt(manager.getHat() )];
        }
        else
        {
            ShowDefaultHat();
        }
        if (manager.getCloak() != "Null Cloak")
        {
            cloak_image.GetComponent<Image>().sprite = manager.cloaks[manager.ElementToInt(manager.getCloak() )]; // m_cloaks
            cloak_symbol.GetComponent<Image>().sprite = manager.symbols[manager.ElementToInt(manager.getCloak() )];
        }
        else
        {
            ShowDefaultCloak();
        }
    }

    public void ShowDefaultHat()
    {
        hat_image.GetComponent<Image>().sprite = manager.default_hat;
        hat_symbol.GetComponent<Image>().sprite = manager.default_symbol;
    }
    public void ShowDefaultCloak()
    {
        cloak_image.GetComponent<Image>().sprite = manager.default_cloak;
        cloak_symbol.GetComponent<Image>().sprite = manager.default_symbol;
    }
    public void ShowDefaultItems()
    {
        ShowDefaultHat();
        ShowDefaultCloak();
    }
}
