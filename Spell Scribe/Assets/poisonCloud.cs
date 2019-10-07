using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonCloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hold()
    {
        this.GetComponent<Animator>().Play("PoisonHold", -1);
        StartCoroutine(Linger());
    }


    IEnumerator Linger()
    {
        yield return new WaitForSeconds(10.0f);

        this.GetComponent<Animator>().Play("PoisonEnd", -1);

        this.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
           StartCoroutine( collision.gameObject.GetComponent<EnemiesInterface>().Poison());
        }
    }
}
