using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMove : MonoBehaviour
{
    SpellManager spellManage;
    // Start is called before the first frame update
    void Start()
    {
        spellManage = FindObjectOfType<SpellManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.parent.transform.position;
        pos.y += 2 * Time.deltaTime;
        this.transform.parent.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            spellManage.SpellDamage();
            Destroy(this.transform.parent.gameObject);
        }
    }
}
