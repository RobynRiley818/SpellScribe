using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateSpellCards : MonoBehaviour
{
    public string[] wordOptions;
    public Vector2[] cardPositions;

    public List<GameObject> cards = new List<GameObject>();

    public GameObject spellcard;

    private void Start()
    {
        StateManager.currentState = StateManager.GameState.SpellSelect;
    }

    public void Draw()
    {
        if (StateManager.currentState == StateManager.GameState.SpellSelect)
        {
            foreach (GameObject i in cards)
            {
                Destroy(i);
            }
            cards.Clear();

            for (int i = 0; i < 5; i++)
            {
                string spell = wordOptions[Random.Range(0, 3)];

                GameObject ins = Instantiate(spellcard, FindObjectOfType<Canvas>().transform);

                ins.GetComponent<RectTransform>().localPosition = cardPositions[i];


                ins.GetComponentInChildren<TextMeshProUGUI>().text = spell;

                cards.Add(ins);
            }

            StateManager.currentState = StateManager.GameState.SpellSelect;
        }
    }
}
