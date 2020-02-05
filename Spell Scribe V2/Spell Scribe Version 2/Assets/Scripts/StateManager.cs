using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private static StateManager instance;

    public enum GameState { Menu, SpellSelect, Writing, WordFill, SpellCast, EnemyTurn };

    public static GameState currentState;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
