using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellEffect : MonoBehaviour {

	public Text spell_text;
	Animator animator;
	DisplayCanvas manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<DisplayCanvas>();
    }

    void OnEnable() {

        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<DisplayCanvas>();

        spell_text.text = manager.getSpellText();  // spell text is received from server, and is string

        animator = GetComponent<Animator> ();

		ShowEffect ();
        		
	}
    /*
    private void Update()
    {
        if (manager.getNav() == "reset animation")
            ResetAnimation();
    }*/

    public void ShowEffect() {
        // wings=5, rubberduck=14, firealarm=18
        int s = manager.getSpell();
        if (s != -1)
            animator.SetInteger("spell_num", s);  // spell is an int
        
        spell_text.text = manager.getSpellText();  // spell text is received from server, and is string
        
		if (s != -1 && (!manager.unlocked_spells.Contains(s)) )
			manager.unlocked_spells.Add (s);
	}

    public void ResetAnimation()
    {
        manager.setSpell(-1);
        animator.SetInteger("spell_num", -1);        
    }

    void OnDisable()
    {
        ResetAnimation();
    }
}
