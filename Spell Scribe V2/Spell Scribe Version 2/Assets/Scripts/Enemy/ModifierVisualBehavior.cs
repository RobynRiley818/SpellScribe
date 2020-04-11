using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierVisualBehavior : MonoBehaviour
{
    public ModifierHolder[] mods;
    private GameObject tempObject;

    public GameObject burnPrefab;
    public GameObject stunPrefab;
    public enum EnemyModTypes { burn, stun };

    public void AddModd(EnemyModTypes em, int num)
    {
        if(em == EnemyModTypes.burn)
        {
            for (int i = 0; i < mods.Length; i++)
            {
                if (mods[i].modifer != null)
                {
                    if (mods[i].modifer.GetComponent<Burning>())
                    {
                        mods[i].modifer.GetComponent<Burning>().effectNum += num;
                        mods[i].SetChild();
                        return;
                    }
                }
            }
        }

        else if(em == EnemyModTypes.stun)
        {
            for (int i = 0; i < mods.Length; i++)
            {
                if (mods[i].modifer != null)
                {
                    if (mods[i].modifer.GetComponent<Stunned>())
                    {
                        mods[i].modifer.GetComponent<Stunned>().effectNum += num;
                        mods[i].SetChild();
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < mods.Length; i++)
        {
            if (mods[i].modifer == null)
            {
                switch (em)
                {
                    case EnemyModTypes.burn:
                        tempObject = Instantiate(burnPrefab, mods[i].transform, false);
                        break;
                    case EnemyModTypes.stun:
                        tempObject = Instantiate(stunPrefab, mods[i].transform, false);
                        break;

                }

                mods[i].modifer = tempObject;
                mods[i].SetChild();
                tempObject.transform.position = Vector2.zero;
                tempObject.GetComponent<EnemyModifier>().effectNum = num;
                break;
            }
        }
    }

    public void ReOrderModd() { Invoke("ReOrderHelper", .1f);}

    private void ReOrderHelper()
    {
        int removePosition = 0;

        for (int i = 0; i < mods.Length; i++)
        {
            if (mods[i].modifer == null)
            {
                removePosition = i;
                break;
            }
        }

        for (int i = removePosition; i <= mods.Length - 1; i++)
        {
            if (mods[i + 1].modifer != null)
            {
                tempObject = mods[i + 1].modifer;
                mods[i].modifer = tempObject;
                tempObject.transform.parent = mods[i].gameObject.transform;
                mods[i].SetChild();
            }

            else
            {
                mods[i].modifer = null;
                mods[i].NoChild();
                break;
            }
        }
    }
}
