using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject enemy;
    public GameObject healthCanvas;
    public Image health;

    public float maxHp;

    public GameObject spn;

    public int lastHit;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetChild(1).gameObject;
        healthCanvas = transform.GetChild(0).gameObject;

        health = healthCanvas.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();

        maxHp = enemy.GetComponent<EnemiesInterface>().hitPoints;
        spn = GameObject.Find("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        healthCanvas.transform.position = enemy.transform.position;
        health.fillAmount = enemy.GetComponent<EnemiesInterface>().hitPoints / maxHp;

        if(health.fillAmount == 0 && !isDead)
        {
            isDead = true;
            StartCoroutine( enemy.GetComponent<EnemiesInterface>().Death(lastHit));
        }


    }
}
