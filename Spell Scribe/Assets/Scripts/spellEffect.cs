using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellEffect : MonoBehaviour
{

    public Animator effects;
    // Start is called before the first frame update
    void Start()
    {
        effects = this.GetComponent<Animator>();
        effects.Play(this.name, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void turnOff()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
