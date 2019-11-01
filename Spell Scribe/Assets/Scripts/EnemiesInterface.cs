using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInterface : MonoBehaviour {

    //The time the enemy takes to reach the player, in seconds
    public float speed;
    public float pace;
    

    [Header( "The movement nodes the enemy will follow")]
    public Queue<GameObject> path;
    public GameObject start;

    public GameObject next;

    public GameObject poisonPool;
    public GameObject icePool;
    public GameObject lightning;

    public float hitPoints;

    [Header("1 = Lightning, 2 = Poison, 3 = Ice")]
    public int weakness;

    public bool normal, speedy, tank;

    private bool poisoned;

    [Header("How much HP poison drains each second")]
    public float poisonTicks = 0.6f;

    [Header("Seconds that status effects last")]
    public int poisonTime = 15;
    public int freezeTime = 10;

    [Header("Radius of Lightning Chain and Enemies hit")]
    public float rad;
    int chainDamage;


    [Header("Points rewarded on Defeat")]
    public int reward;


    public GameObject poisonSingle, poisonMass, iceSingle, iceMass;

    public GameObject spawner;

    private void Start()
    {
        manager = GameObject.Find("Game Manager(Clone)");
        path = new Queue<GameObject>();
        start = GameObject.Find("MovementNode");
        fillQueue(start);
        lightning = GameObject.Find("Lightning");
        poisonSingle = GameObject.Find("Poison 2");
        iceSingle = GameObject.Find("Ice 2");
        poisonMass = GameObject.Find("Poison 3");
        iceMass = GameObject.Find("Ice 3");

        spawner = GameObject.Find("Spawner");
    }


    public void fillQueue(GameObject current)
    {
        path.Enqueue(current);
        if(normal || tank)
        {
            if (current.GetComponent<DirectionNode>().getNext("Walk") != null)
            {
                fillQueue(current.GetComponent<DirectionNode>().getNext("Walk"));
            } 
            else
            {
                pace = speed / path.Count;

                next = path.ToArray()[0];
                path.Dequeue();
            }
        }
        if (speedy)
        {
            if (current.GetComponent<DirectionNode>().getNext("Fly") != null)
            {
                fillQueue(current.GetComponent<DirectionNode>().getNext("Fly"));
            }
            else
            {
                pace = speed / path.Count;
                next = path.ToArray()[0];
                    path.Dequeue();
            }
        }


        freezePace = pace / 2;
        fullPace = pace;

    }

    private void Update()
    {
        //if (!isHit)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, next.transform.position, pace * Time.deltaTime);
        //}
        //if (speedy)
        //{
        //    transform.up = (next.transform.position - transform.position);
        //}
        //else
        //{
        //    transform.up = -(next.transform.position - transform.position);
        //}
        //if (transform.position.Equals(next.transform.position))
        //{
        //    if (path.Count == 0)
        //    {
        //        if (manager.GetComponent<GameManager>().totalScore > 0)
        //        {
        //            manager.GetComponent<GameManager>().totalScore -= reward;
        //        }
        //        manager.GetComponent<GameManager>().enemiesEscaped++;
        //        Dies();
        //        Destroy(transform.parent.gameObject);

        //    }
        //    else
        //    {
        //        next = path.ToArray()[0];
        //        path.Dequeue();
        //    }
        //}

        if (poisoned)
        {
            hitPoints -= poisonTicks * Time.deltaTime;
        }
    }

    public void damaged(int amount, int type, int rank)
    {

        /*if (spawner != null)
        {
            for (int i = spawner.GetComponent<Spawner>().enemies.Count - 1; i > -1; i--)
            {
                if (spawner.GetComponent<Spawner>().enemies[i] == null)
                    spawner.GetComponent<Spawner>().enemies.RemoveAt(i);
            }
        }*/
        if (rank == 3)
        {
            //Blake Fill in whatever needs to be done when the spell hits here
            //Lightning Special
            if (type == 1)
            {
                chainDamage = amount / 2;
                //RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, Mathf.Infinity, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemies"), -.5f);
                for (int i = 0; i < spawner.GetComponent<Spawner>().enemies.Count; i++)
                {
                    if (spawner.GetComponent<Spawner>().enemies[i] != null)
                    {
                        StartCoroutine(lightningTravel(spawner.GetComponent<Spawner>().enemies[i].transform.GetChild(1).gameObject, chainDamage, type, 1));
                        //spawner.GetComponent<Spawner>().enemies[i].transform.GetChild(1).gameObject.GetComponent<EnemiesInterface>().damaged(chainDamage, 1, 1);
                    }
                }

            }
            //Poison Special
            if (type == 2)
            {
                poisonMass.transform.position = this.transform.position;
                poisonMass.GetComponent<SpriteRenderer>().enabled = true;
                if (poisonMass.GetComponent<Animator>().speed == 0)
                {

                    poisonMass.GetComponent<Animator>().speed = 1;
                }
                poisonMass.GetComponent<Animator>().Play("Poison3", -1, 0);

                poisonMass.GetComponent<PolygonCollider2D>().enabled = true;
                StartCoroutine(Poison());
            }
            //Ice Special
            if (type == 3)
            {
                iceMass.transform.position = this.transform.position;
                iceMass.GetComponent<SpriteRenderer>().enabled = true;
                if (iceMass.GetComponent<Animator>().speed == 0)
                {

                    iceMass.GetComponent<Animator>().speed = 1;
                }
                iceMass.GetComponent<Animator>().Play("Ice3", -1, 0);

                foreach(GameObject i in spawner.GetComponent<Spawner>().enemies)
                {
                    if(i != null)
                    {
                       StartCoroutine( i.GetComponentInChildren<EnemiesInterface>().Freeze());
                    }
                }
                StartCoroutine(Freeze());
            }
        }
        if(rank == 2)
        {
            //Lightning Special
            if(type == 1)
            {
                chainDamage = amount / 2;
                StartCoroutine(Lightning());
            }
            //Poison Special
            if(type == 2)
            {
                poisonSingle.transform.position = this.transform.position;
                poisonSingle.GetComponent<SpriteRenderer>().enabled = true;
                if(poisonSingle.GetComponent<Animator>().speed == 0)
                {

                    poisonSingle.GetComponent<Animator>().speed = 1;
                }
                poisonSingle.GetComponent<Animator>().Play("Poison2", -1, 0);
                StartCoroutine(Poison());
            }
            //Ice Special
            if(type == 3)
            {
                iceSingle.transform.position = this.transform.position;
                iceSingle.GetComponent<SpriteRenderer>().enabled = true;
                if (iceSingle.GetComponent<Animator>().speed == 0)
                {

                    iceSingle.GetComponent<Animator>().speed = 1;
                }
                iceSingle.GetComponent<Animator>().Play("Poison2", -1, 0);
                StartCoroutine(Freeze());
            }
        }
        if (type == weakness)
        {
            hitPoints -= (amount * 2);
        }
        else
        {
            hitPoints -= amount;
        }

        transform.parent.GetComponent<HealthBar>().lastHit = type;

        StartCoroutine(Hit());
    }

    bool isHit;

    IEnumerator Hit()
    {
        isHit = true;
        yield return new WaitForSeconds(1);
        isHit = false;

    }

    float freezePace;
    float fullPace;

    public IEnumerator Freeze()
    {
        //Instantiating a particle system or color shift here for visuals would be nice
        pace = freezePace;
        this.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(.5f);
        iceMass.transform.SetParent(iceMass.transform);

        yield return new WaitForSeconds(freezeTime - .5f);

        this.GetComponent<SpriteRenderer>().color = Color.white;
        pace = fullPace;
        
    }

    public IEnumerator Poison()
    {
        //Same visual effects as Freeze really
        poisoned = true;
        this.GetComponent<SpriteRenderer>().color = Color.green;

        if(hitPoints <= 0)
        {
            StartCoroutine(Death(2));
        }
        yield return new WaitForSeconds(poisonTime);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        poisoned = false;
    }

    IEnumerator Lightning()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
        RaycastHit2D[] hit = Physics2D.CircleCastAll(this.transform.position, rad, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemies"), -.5f);

        float closest = Mathf.Infinity;
        GameObject closeEn = null;
        float distance;

        for(int i = 0; i < hit.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position, hit[i].collider.gameObject.transform.position);
            if(distance < closest)
            {
                closest = distance;
                closeEn = hit[i].collider.gameObject;
            }
        }

        if (closeEn != null)
        {
            StartCoroutine(lightningTravel(closeEn, chainDamage, 1, 1));
        }
        yield return null;
    }


    public IEnumerator lightningTravel(GameObject what, int amount, int type, int rank)
    {

        GameObject ins = Instantiate(lightning, this.gameObject.transform.position, Quaternion.identity);
        Vector3 difference = what.transform.position - ins.transform.position;
        float look = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        ins.transform.rotation = Quaternion.LookRotation(Vector3.forward, -difference);

        float length = Vector3.Distance(what.transform.position, this.gameObject.transform.position);
        ins.GetComponent<SpriteRenderer>().size = new Vector2(ins.GetComponent<SpriteRenderer>().size.x, length * 4);
        ins.GetComponent<SpriteRenderer>().enabled = true;
        ins.GetComponent<Animator>().Play("Lightning", -1, 0);
        what.GetComponent<EnemiesInterface>().damaged(amount, type, rank);

        yield return new WaitForSeconds(.5f);
        Destroy(ins);


        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ice Pool")
        {
            StartCoroutine(Freeze());
        }
        if (collision.gameObject.tag == "Poison Pool")
        {
            StartCoroutine(Poison());
        }
    }


    public AudioSource deathSound;
    GameObject manager;

    public IEnumerator Death(int type)
    {

        manager.GetComponent<GameManager>().totalScore += reward;
        manager.GetComponent<GameManager>().enemiesDefeated++;
        //Dies();
        switch (type)
        {
            case 1:
                GetComponent<Animator>().Play("Lightning", -1, 0);
                break;
            case 2:
                GetComponent<Animator>().Play("Dead", -1, 0);
                break;
            case 3:
                GetComponent<Animator>().Play("Ice", -1, 0);
                break;
        }

        yield return new WaitUntil(() => GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"));

        /*if (spawner != null)
        {
            for (int i = spawner.GetComponent<Spawner>().enemies.Count - 1; i > -1; i--)
            {
                if (spawner.GetComponent<Spawner>().enemies[i] == null)
                {
                    spawner.GetComponent<Spawner>().enemies.RemoveAt(i);

                }
            }
        }*/

        Destroy(transform.parent.gameObject);
        Dies();


        yield return null;
    }

    public void Dies()
    {


        //spawner.GetComponent<Spawner>().enemies.TrimExcess();

        Spawner.dead += 1;
        Spawner.totDead += 1;


        

    }
}
