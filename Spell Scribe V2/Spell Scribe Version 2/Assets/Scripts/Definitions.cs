using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definitions : MonoBehaviour
{
    public string GetDef(string word)
    {
        switch (word)
        {
            case "Be":
                return "To Be Or Not To be";
            case "To":
                return "To TO TOOOO???? TO";
            case "Hello":
                return "Hello World";
            default:
                return "Can Not Find " + word;
        }
    }

    public string GetEffect(string effect, int num)
    {
        switch (effect)
        {
            case "Burn":
                return "Applies " + num + " Burn To the Enemy. On the Start of the Enemies they will take damage equal to their burn. Then their burn ammmount goes down by one";
            case "Heal":
                return "Restores " + num + " of your health.";
            case "Stun":
                return "Apples " + num + " Stun to the Enemy. If an enemy's stun ammount reeaches 5 they will lose 5 stun and skip their turn";
            default:
                return "Error: Could Not Find Effect";
        }
    }
}
