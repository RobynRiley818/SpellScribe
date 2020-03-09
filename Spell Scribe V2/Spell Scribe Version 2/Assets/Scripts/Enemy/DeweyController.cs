using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeweyController : MonoBehaviour
{
    public GameObject dewIdle;
    public GameObject dewAttack;
    public GameObject dewHurt;

    private Enemy dew;
    // Start is called before the first frame update
    void Start()
    {
        dew = FindObjectOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
    }

    private void CheckAnimation()
    {
        if(dew.health > 0)
        {
            if (StateManager.currentState == StateManager.GameState.EnemyTurn)
            {
                dewAttack.SetActive(true);
                dewHurt.SetActive(false);
                dewIdle.SetActive(false);
            }

            else if (StateManager.currentState == StateManager.GameState.SpellCast)
            {
                dewAttack.SetActive(false);
                dewHurt.SetActive(true);
                dewIdle.SetActive(false);
            }

            else
            {
                dewAttack.SetActive(false);
                dewHurt.SetActive(false);
                dewIdle.SetActive(true);
            }
        }

        else
        {
            dewAttack.SetActive(false);
            dewHurt.SetActive(false);
            dewIdle.SetActive(false);
        }


    }
}
