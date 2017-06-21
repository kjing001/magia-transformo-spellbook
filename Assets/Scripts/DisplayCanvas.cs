using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core; // receive WiFi msg
using System;
using EasyWiFi.ClientBackchannels;


public class DisplayCanvas : MonoBehaviour {
	public GameObject spells_canvas;  // canvas_num=1
	public GameObject witch_canvas;   // canvas_num=2
	public GameObject casting_canvas; // canvas_num=3
	public GameObject result_canvas;  // canvas_num=4


    public int player_num = 1;
    static public string hat;
    static public string cloak;
    static public string witch;

    static public string msg = "clear";
    static public string nav = "no msg";
    static public string gameState;
    static public string spellText; // receive from another control name

    static public int spell;

    public List<int> unlocked_spells;

    bool onetime1 = true, onetime2 = true, onetime3 = true, onetime4 = true;

    string lastState;

	public Sprite[] hats;
	public Sprite[] cloaks;
    public Sprite[] cloaksM;
    public Sprite[] symbols;
	public Sprite default_hat;
	public Sprite default_cloak;
    public Sprite default_cloakM;
    public Sprite default_symbol;
    
    public Text showMsgCanvas1;
    public Text showMsgCanvas2;
    public Text showMsgCanvas3;
    public Text showPlayerNumCanvas1;
    public Text showPlayerNumCanvas2;
 

    static Dictionary<string, string> witchLookup = new Dictionary<string, string>() {
        {"FF", "Fire Witch"},       {"WW", "Water Witch"},      {"EE", "Earth Witch"},
        {"AA", "Air Witch"},        {"DD", "Dark Witch"},       {"NN", "Energy Witch"},
        {"FW", "Steam Witch"},
        {"FE", "Lava Witch"},
        {"FA", "Fire Air Witch"},
        {"FD", "Dark Fire Witch"},
        {"FN", "Fire Energy Witch"},
        {"WE", "Mud Witch"},
        {"WA", "Rain Witch"},
        {"WD", "Dark Water Witch"},
        {"WN", "Water Energy Witch"},
        {"EA", "Dust Witch"},
        {"ED", "Dark Earth Witch"},
        {"EN", "Gem Witch"},
        {"AD", "Tornado Witch"},
        {"AN", "Wind Witch"},
        {"DN", "Dark Energy Witch"}
    };


    private void Awake()
    {
        spell = -1;
        hat = "Null Hat";
        cloak = "Null Cloak";
        witch = "Null Witch";
        spellText = "";
        msg = "clear";
        nav = "clear";
    }
    // Use this for initialization
    void Start () {
        spell = -1;
        hat = "Null Hat";
        cloak = "Null Cloak";
        witch = "Null Witch";
        spellText = "";
        msg = "clear";
        nav = "clear";
        lastState = "store its value to check if the gameState is the same in next frame";
        SwitchCanvas(1);
        //StartCoroutine (Example ());
        showPlayerNumCanvas1.text = player_num.ToString();
        showPlayerNumCanvas2.text = player_num.ToString();
        
    }

    void Update()
    {
        showMsgCanvas1.text = "MSG: " + msg;
        showMsgCanvas2.text = "MSG: " + msg;
        //showMsgCanvas3.text = "NAV: " + nav;
        
        if (msg != "clear")
        {
            if ((msg.Contains("Hat")) || (msg.Contains("Cloak")))
            {
                if (msg == "Null Hat")
                {
                    // for removing hat image in ItemScanned
                    setHat(msg);
                    // nullify witcher identity
                    setWitch("Null Witch");
                }
                else if (msg == "Null Cloak")
                {
                    // for removing cloak image in ItemInfo
                    setCloak(msg);
                    // nullify witcher identity
                    setWitch("Null Witch");
                }
                else
                    OnItemScanned(msg);
            }

            else
            {
                if (spell == -1)
                {
                    // check if the msg is a spell
                    // set int spell to a corresponding value
                    // spell is sent from the server after everything is finalized
                    
                    SpellToInt(msg);
                    //SpellToInt("CONGRATULATIONS YOUR CAULDRON SET OFF THE FIRE ALARM");
                }

                if(msg == "reset")
                {
                    OnReset();
                }
            }
        }
        
        // build or change character according to hat and cloak
        determineWitch();

        // switch from server command
        if(lastState != gameState)
        {
            if (gameState == "spell list")
            {
                SwitchCanvas(1);
            }
            else if (gameState == "scan item")
            {
                SwitchCanvas(2);
            }
            else if (gameState == "spell casting")
            {
                SwitchCanvas(3);
            }
            else if (gameState == "result")
            {
                SwitchCanvas(4);
            }
        }

        //        DontDestroyOnLoad(transform.gameObject);

    }
   

    // character
    public void receiveMsgPlayer(StringBackchannelType stringBackchannel)
    {
        msg = stringBackchannel.STRING_VALUE;
    }
    // navgation
    public void receiveNav(StringBackchannelType stringBackchannel)
    {
        nav = stringBackchannel.STRING_VALUE;
    }
    // spell /// useless
    public void receiveSpell(StringBackchannelType stringBackchannel)
    {
        spellText = stringBackchannel.STRING_VALUE;
    }
    
    // game state
    public void receiveGameState(StringBackchannelType stringBackchannel)
    {
        gameState = stringBackchannel.STRING_VALUE;        
    }
    public void determineWitch()
    {
        // update witch values 
        if (getHat() == "Null Hat" || getCloak() == "Null Cloak")
            witch = "Null Witch";
        else if (witchLookup.ContainsKey(hat.Substring(0, 1) + cloak.Substring(0, 1)))
            witch = witchLookup[hat.Substring(0, 1) + cloak.Substring(0, 1)];
        else
            witch = witchLookup[cloak.Substring(0, 1) + hat.Substring(0, 1)];                            
    }

    public string getHat()
    {
        return hat;
    }
    public string getCloak()
    {
        return cloak;
    }
    public string getWitch()
    {
        return witch;
    }
    public void setHat(string s)
    {
        hat = s;
    }
    public void setCloak(string s)
    {
        cloak = s;
    }
    public void setWitch(string s)
    {
        witch = s;
    }
    
    public void SwitchCanvas(int canvas_num) {
        
        if (canvas_num == 1)
        {
            spells_canvas.SetActive(true);
            witch_canvas.SetActive(false);
            casting_canvas.SetActive(false);
            result_canvas.SetActive(false);

            lastState = "spell list";
        }
        else if (canvas_num == 2)
        {
            witch_canvas.SetActive(true);
            spells_canvas.SetActive(false);            
            casting_canvas.SetActive(false);
            result_canvas.SetActive(false);

            lastState = "scan item";
        }
        else if (canvas_num == 3)
        {
            casting_canvas.SetActive(true);
            spells_canvas.SetActive(false);
            witch_canvas.SetActive(false);                
            result_canvas.SetActive(false);

            lastState = "spell casting";
        }
        else if (canvas_num == 4)
        {
            result_canvas.SetActive(true);
            spells_canvas.SetActive(false);
            witch_canvas.SetActive(false);
            casting_canvas.SetActive(false);

            lastState = "result";
        }     

    }
    /*
    public void HideCanvas(CanvasGroup cg)
    {

            cg.alpha = 0;
            cg.interactable = false;
        
    }
    public void ShowCanvas(CanvasGroup cg)
    {
        cg.interactable = true;
        cg.alpha = 1;
    }*/

    public void OnItemScanned(string item) {
		string[] costume = item.Split (" "[0]);
		if (costume [1] == "Hat")
			setHat(item);
		else if (costume [1] == "Cloak")
			setCloak(item);

        //Debug.Log (string.Format ("Hat:{0}, Cloak:{1}, Witch:{2}", hat, cloak, witch));
        if (onetime2)
        {
            SwitchCanvas(2);
            onetime2 = false;
        }
            
	}

    public void OnReset()
    {
        hat = "Null Hat";

        cloak = "Null Cloak";
        witch = "Null Witch";
        onetime2 = true; // switch to canvas 2 when a costume is first selected
    }


	public int ElementToInt(string element) {
        if (element.Contains("Fire"))
            return 0;
        else if (element.Contains("Water")) 
			return 1;
		else if (element.Contains("Earth"))
			return 2;
		else if (element.Contains("Air"))
			return 3;
		else if (element.Contains("Dark"))
			return 4;
		else if (element.Contains("N-ergy") || element.Contains("Energy"))
			return 5;
		else 
			return 6;  // "Null"
	}

	public void SpellToInt(string spell_text) {
        // call with result of Spell method in CastSpell (key in spell dictionary)
        if (spell_text == "no msg")
        {
            spellText = spell_text;
        }

        else if (spell_text == "Summon Ghost")
        {
            spell = 1;
            spellText = spell_text;
        }

        else if (spell_text == "Summon Dragon")
        {
            spell = 2;
            spellText = spell_text;
        }

        else if (spell_text == "Good Luck")
        {
            spell = 3;
            spellText = spell_text;
        }

        else if (spell_text == "Bad Luck")
        {
            spell = 4;
            spellText = spell_text;
        }

        else if (spell_text == "Flight")
        {
            spell = 5;
            spellText = spell_text;
        }

        else if (spell_text == "$$$")
        {
            spell = 6;
            spellText = spell_text;
        }

        else if (spell_text == "Your Code Works")
        {
            spell = 7;
            spellText = spell_text;
        }

        else if (spell_text == "Perfect Coffee")
        {
            spell = 8;
            spellText = spell_text;
        }

        else if (spell_text == "Zombie Apocalypse")
        {
            spell = 9;
            spellText = spell_text;
        }
        else if (spell_text == "Apocalypse")
        {
            spell = 10;
            spellText = spell_text;
        }
        else if (spell_text == "Transforms Drinker into a Newt\n(you get better)")
        {
            spell = 11;
            spellText = spell_text;
        }
        else if (spell_text == "Makes Drinker Weigh Same as a Duck")
        {
            spell = 12;
            spellText = spell_text;
        }
        else if (spell_text == "Makes Drinker Weigh Not Same as a Duck")
        {
            spell = 13;
            spellText = spell_text;
        }
        else if (spell_text == "INFINITE RUBBER DUCKS")
        {
            spell = 14;
            spellText = spell_text;
        }
        else if (spell_text == "Fireworks")
        {
            spell = 15;
            spellText = spell_text;
        }
        else if (spell_text == "Truth Serum")
        {
            spell = 16;
            spellText = spell_text;
        }
        else if (spell_text == "Productivity 500%")
        {
            spell = 17;
            spellText = spell_text;
        }
        else if (spell_text == "CONGRATULATIONS YOUR CAULDRON SET OFF THE FIRE ALARM")
        {
            spell = 18;
            spellText = spell_text;
        }

        else if (spell_text == "Summon Cat Familiar")
        {
            spell = 19;
            spellText = spell_text;
        }

        else if (spell_text == "Nothing happens")
        {
            spell = 20;
            spellText = spell_text;
        }
        else
        {
            spell = -1;
            spellText = "no spell received";
        }

    }
    
    // get and set
    public string getMsg()
    {
        return msg;
    }
    public void setMsg(string s)
    {
        msg = s;
    }
    public string getNav()
    {
        return nav;
    }
    public void setNav(string s)
    {
        nav = s;
    }
    public string getSpellText()
    {
        return spellText;
    }
    public void setSpellText(string s)
    {
        spellText = s;
    }
    public int getSpell()
    {
        return spell;
    }
    public void setSpell(int s)
    {
        spell = s;
    }
    public string getGameState()
    {
        return gameState;
    }
    public void setGameState(string s)
    {
        gameState = s;
    }



    /*
       IEnumerator Example()
       {
           print(Time.time);
           yield return new WaitForSeconds(5);
           OnItemScanned ("Fire Hat");

           yield return new WaitForSeconds(2);
           OnItemScanned ("Water Cloak");
           OnItemScanned ("Steam");
           print(Time.time);


           yield return new WaitForSeconds (2);
           OnItemScanned ("No Hat");
           print (Time.time);

           yield return new WaitForSeconds (5);
           SpellToInt ("INFINITE RUBBER DUCKS");
           SwitchCanvas (4);
           print (Time.time);

           yield return 0;
       }    */
}



