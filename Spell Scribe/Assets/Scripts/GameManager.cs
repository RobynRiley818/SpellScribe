using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField] public static GameObject outline;
	[SerializeField] float maxSize = 5;
	[SerializeField] float minSize = 0.1f;
    [SerializeField] float currSize = 0.5f;
	public static List<EdgeCollider2D> edgeColliders = new List<EdgeCollider2D>();
	List<CircleCollider2D> circleColliders = new List<CircleCollider2D>();
    public Button checker;
    public static GameObject word;

    public static GameObject ins;
    public static GameObject en;

    public static bool wordOutlines = false;

    public static bool exists;

    public static bool spellChosen = false;

    public static bool spellReady;

    [HideInInspector] public int spellDamage;

    [HideInInspector] public int spellType;

    [HideInInspector] public int spellRank;

     public GameObject spellEffect;

    public GameObject ice1, ice2, ice3, pois1, pois2, pois3, light1;

    public AudioSource spellSound, hitSound;

    public AudioSource menu, tut, game;

    public int totalScore, enemiesDefeated, enemiesEscaped, wavesCleared;

    public Text score, nonCursive;

    [HideInInspector] public GameObject norm, check, bad;

    public enum Difficulty { Easy, Normal, Hard };

    public static Difficulty current;

    public bool teaching;

    public bool canWrite;

    public GameObject[] easyWords, normalWords, hardWords;


    public void setDifficulty(Difficulty dif)
    {
        current = dif;
    }

    public static List<GameObject> enemies;

    // Use this for initialization
    void Start () 
	{
        Screen.orientation = ScreenOrientation.Portrait;
        exists = true;
        /*EdgeCollider2D[] lines = outline.GetComponentsInChildren<EdgeCollider2D>();
		CircleCollider2D[] circles = outline.GetComponentsInChildren<CircleCollider2D>();
		for (int i = 0; i < lines.Length; i++)
		{
			edgeColliders.Add(lines[i]);
		}
		for (int n = 0; n < circles.Length; n++)
		{
			circleColliders.Add(circles[n]);
		}*/
        //edgeColliders.AddRange(outline.GetComponentsInChildren<EdgeCollider2D>());
        //circleColliders.AddRange(outline.GetComponentsInChildren<CircleCollider2D>());
        //ins = word;

        DontDestroyOnLoad(this);
        Input.simulateMouseWithTouches = false;
    }

    public void setSize(float size)
    {
        if(size > minSize && size < maxSize)
        {
            currSize = size;
        }
    }

	public void ChangeColliderSize(string input)
	{
		float newSize = float.Parse(input);
		if (newSize > maxSize)
		{
			newSize = maxSize;
		}
		else if (newSize < minSize)
		{
			newSize = minSize;
		}
        currSize = newSize;

        for (int i = 0; i < edgeColliders.Count; i++)
        {
            if (edgeColliders[i] != null)
            {
                edgeColliders[i].edgeRadius = newSize;
            }
        }
		for (int n = 0; n < circleColliders.Count; n++)
		{
			circleColliders[n].radius = newSize;
		}

	}

    /*public void updateGame(GameObject enemy)
    {
        //If there is a word value stored, clear it and it's colliders from the manager
        if (word)
        {
            edgeColliders.Clear();
            Destroy(ins);
        }
        //updates a whole lotta variables based on the parameters of the enemy. The
        checker = DrawingManager.hold;
        en = enemy;
        word = enemy.GetComponent<EnemiesInterface>().word;
        ins = Instantiate(word);
        outline = ins.transform.Find("Outline").gameObject;
        //updates the drawing manager in the newly instantiated word
        outline.GetComponent<DrawingBounds>().drawingManager = GameObject.Find("Drawing Manager").GetComponent<DrawingManager>();
        //Updates the text parameter to be used with the casting button
        word.GetComponent<WordCheck>().text = FindObjectOfType<Text>();
        //Updates the call for the casting button
        checker.onClick.AddListener(ins.GetComponent<WordCheck>().Check);
        checker.onClick.AddListener(DrawingManager.Reset);
        //Updates the edgecollider list to be modified during gameplay
        edgeColliders.AddRange(outline.GetComponents<EdgeCollider2D>());

        ChangeColliderSize(currSize.ToString());

    }*/

    public void startSpell(GameObject spellWord, int rank, int damage, int type)
    {
        if (word)
        {
            edgeColliders.Clear();
            Destroy(ins);
        }
        word = spellWord;
        ins = Instantiate(word);
        ins.GetComponent<WordCheck>().enabled = true;

        spellRank = rank;
        spellDamage = damage;
        spellType = type;


        spellFilled = false;
        StartCoroutine(Chosen());

        outline = ins.transform.Find("Outline").gameObject;
        //updates the drawing manager in the newly instantiated word
        outline.GetComponent<DrawingBounds>().drawingManager = GameObject.Find("Drawing Manager").GetComponent<DrawingManager>();
        //Updates the text parameter to be used with the casting button
        word.GetComponent<WordCheck>().text = FindObjectOfType<Text>();

        //Updates the edgecollider list to be modified during gameplay
        edgeColliders.AddRange(outline.GetComponentsInChildren<EdgeCollider2D>());
        //norm.SetActive(true);

        ChangeColliderSize(currSize.ToString());

    }

    IEnumerator Chosen()
    {
        yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(.1f);

        spellChosen = true;

        canWrite = true;
    }

    public bool markers = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(4);
        }
        if(totalScore < 0)
        {
            totalScore = 0;
        }

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if (!menu.isPlaying)
            {
                menu.Play();
                game.Stop();
                tut.Stop();
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            if (!tut.isPlaying)
            {
                tut.Play();
                game.Stop();
                menu.Stop();
            }
        }
        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level 1") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0) || SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
        {
            /*if (spellEffect == null)
            { 
                spellEffect = FindObjectOfType<ParticleSystem>();
                spellEffect.transform.position = GameObject.Find("Player").transform.position;
            }*/
            if(nonCursive == null)
            {
                nonCursive = GameObject.Find("Word").GetComponent<Text>();
            }
            if(score == null)
            {
                score = GameObject.Find("Score").GetComponent<Text>();
            }
            if(filler == null)
            {
                filler = GameObject.Find("Filler");
            }
            if(ice2 == null)
            {
                ice2 = GameObject.Find("Ice 2");
                ice2.transform.position = GameObject.Find("Player").transform.position;
                ice2.GetComponent<SpriteRenderer>().enabled = false;
                ice2.GetComponent<Animator>().speed = 0;
            }
            if (ice3 == null)
            {
                ice3 = GameObject.Find("Ice 3");
                ice3.transform.position = GameObject.Find("Player").transform.position;
                ice3.GetComponent<SpriteRenderer>().enabled = false;
                ice3.GetComponent<Animator>().speed = 0;
            }
            if (pois1 == null)
            {
                pois1 = GameObject.Find("Poison 1");
                pois1.transform.position = GameObject.Find("Player").transform.position;
                pois1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (pois2 == null)
            {
                pois2 = GameObject.Find("Poison 2");
                pois2.transform.position = GameObject.Find("Player").transform.position;
                pois2.GetComponent<SpriteRenderer>().enabled = false;
                pois2.GetComponent<Animator>().speed = 0;
            }
            if (pois3 == null)
            {
                pois3 = GameObject.Find("Poison 3");
                pois3.transform.position = GameObject.Find("Player").transform.position;
                pois3.GetComponent<SpriteRenderer>().enabled = false;
                pois3.GetComponent<Animator>().speed = 0;
                pois3.GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (ice1 == null)
            {
                ice1 = GameObject.Find("Ice 1");
                ice1.transform.position = GameObject.Find("Player").transform.position;
                ice1.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (light1 == null)
            {
                light1 = GameObject.Find("Lightning");
                light1.transform.position = GameObject.Find("Player").transform.position;
                light1.GetComponent<SpriteRenderer>().enabled = false;
                light1.GetComponent<Animator>().speed = 0;
            }
            if (!markers)
            {
                /*norm = GameObject.Find("Norm");
                check = GameObject.Find("CheckMark");
                bad = GameObject.Find("No");
                bad.SetActive(false);
                check.SetActive(false);*/
                totalScore = 0;
                enemiesDefeated = 0;
                enemiesEscaped = 0;
                canWrite = false;
                spellReady = false;
                spellChosen = false;
                word = ins = null;
                filled = false;
                if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DragonScene") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("KnightScene") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GoblinScene"))
                {
                    game.Play();
                    tut.Stop();
                    menu.Stop();
                }
                else
                {
                    game.Stop();
                    tut.Play();
                    menu.Stop();
                }

                markers = true;
            }
        }
        else if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Level 1") || SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Tutorial"))
        {
            markers = false;
            canWrite = false;
        }
        
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Tutorial"))
        {
            teaching = true;
        }

        /*if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Level 1") || SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Tutorial 2"))
        {
            spellChosen = false;
            spellReady = false;
        }*/
        
        if(score != null)
        {
            if (score.text != "Score: " + totalScore)
            {
                score.text = "Score: " + totalScore;
            }
        }
        if(nonCursive != null && ins != null)
        {
            if(nonCursive.text != "Your Word Is " + ins.GetComponent<WordCheck>().nonCursive)
            {
                nonCursive.text = "Your Word Is <i>" + ins.GetComponent<WordCheck>().nonCursive + "</i>";
            }
        }
        

        //tracking input for the mouse/stylus
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousepos.x, mousepos.y);


       /* if (check != null)
        {
            if (spellReady && !check.activeSelf)
            {
                norm.SetActive(false);
                check.SetActive(true);
            }
            else if (!spellReady && check.activeSelf)
            {
                norm.SetActive(true);
                check.SetActive(false);
            }
        }*/
        RaycastHit2D enemy = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemies"));
        if (Input.GetMouseButtonDown(0))
        {


            //If the raycast fired when you click collides with an enemy, update the game
            if(enemy)
            {
                if (spellReady && !TutorialManager.blocking)
                {

                    FireSpell(enemy.collider.gameObject);
                    
                }
                else
                {
                    //StartCoroutine(NotReady());
                }
            }
        }

        if(spellReady && !spellFilled)
        {
            StartCoroutine(fillWord());
        }

        if (filling)
        {
            line.positionCount = lineIndex + 1;
            line.SetPosition(lineIndex, filler.transform.position);
            lineIndex++;
        }

        if(ins != null)
        {
            if( Input.GetMouseButtonUp(0) && ins.GetComponent<WordCheck>().ready && !TutorialManager.blocking)
            {
                
                ins.GetComponent<WordCheck>().Check();
            }
        }
        //If the enemy dies, update the game based on what no longer exists
        /*if(en == null)
        {
            edgeColliders.Clear();
            word = null;
            Destroy(ins);
            outline = null;
            checker.onClick.RemoveAllListeners();
        }*/
        
    }

    public GameObject fillLine;
    LineRenderer line;
    int lineIndex = 0;

    bool spellFilled;
    GameObject filler;
    bool filling;

    List<GameObject> lines = new List<GameObject>();
    public bool filled;

    public Color yellow;
    public Color purple;

    IEnumerator fillWord()
    {
        canWrite = false;
        foreach (GameObject i in DrawingManager.PreviousLines)
        {
            Destroy(i);
        }
        for (int i = 0; i <= DrawingManager.PreviousLines.Count; i++)
        {
            DrawingManager.PreviousLines.RemoveFirst();
            DrawingManager.PreviousLines.RemoveFirst();
        }


        spellFilled = true;
        foreach (EdgeCollider2D i in edgeColliders)
        {
            GameObject ins = Instantiate(fillLine);
            line = ins.GetComponent<LineRenderer>();
            switch (spellType)
            {
                case 1:
                    line.startColor = yellow;
                    line.endColor = yellow;
                    break;
                case 2:
                    line.startColor = purple;
                    line.endColor = purple;
                    break;
                case 3:
                    line.startColor = Color.cyan;
                    line.endColor = Color.cyan;
                    break;
            }
            lines.Add(ins);
            line.positionCount = 0;
            lineIndex = 0;
            filler.transform.position = i.transform.TransformPoint(i.points[0]);

            filling = true;
            int tracker = 0;
            do
            {
                while (filler.transform.position != i.gameObject.transform.TransformPoint(i.points[tracker]))
                {
                    filler.transform.position = Vector3.MoveTowards(filler.transform.position, i.gameObject.transform.TransformPoint(i.points[tracker]), 2.0f);

                    yield return null;
                }
                tracker++;
                yield return null;
            }
            while (tracker < i.pointCount && i != null);

            if(ins == null)
            {
                break;
            }

            filling = false;
        }

        filled = true;

    }



    /*IEnumerator NotReady()
    {
        norm.SetActive(false);
        bad.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        norm.SetActive(true);
        bad.SetActive(false);
    }*/

    public void ChangeWordOutline()
    {
        wordOutlines = !wordOutlines;
    }

    public void FireSpell(GameObject what)
    {
        //ParticleSystem.MainModule setter = spellEffect.main;
        StopCoroutine(fillWord());
        filling = false;
        filled = false;

        spellSound.Play();
        edgeColliders.Clear();
        word = null;
        Destroy(ins);
        outline = null;

        spellReady = false;
        spellChosen = false;

        spellFilled = false;



        if (spellType == 1)
        {
            //setter.startColor = Color.yellow;
            spellEffect = light1;
            StartCoroutine(lightningTravel(what));
        }
        if(spellType == 2)
        {
            //setter.startColor = Color.green;
            spellEffect = pois1;

            StartCoroutine(spellTravel(what));
        }
        if(spellType == 3)
        {
            //setter.startColor = Color.cyan;
            spellEffect = ice1;


            StartCoroutine(spellTravel(what));
        }

        //spellEffect.Play();

        for(int i = 0; i < lines.Count; i++)
        {
            Destroy(lines[i]);
        }
        lines.Clear();
    }

    IEnumerator spellTravel(GameObject what)
    {
        float step = 7.5f * Time.deltaTime;
        float distance = Vector2.Distance(spellEffect.transform.position, what.transform.position);
        spellEffect.GetComponent<SpriteRenderer>().enabled = true;

        while (distance > .3f)
        {
            distance = Vector2.Distance(spellEffect.transform.position, what.transform.position);
            spellEffect.transform.position = Vector2.MoveTowards(spellEffect.transform.position, what.transform.position, step);

            Vector3 difference = what.transform.position - spellEffect.transform.position;
            float look = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            spellEffect.transform.rotation = Quaternion.LookRotation(Vector3.forward, difference);

            //spellEffect.transform.eulerAngles = new Vector3(spellEffect.transform.eulerAngles.x+180, spellEffect.transform.eulerAngles.y, spellEffect.transform.eulerAngles.z);
            yield return null;
        }
        //spellEffect.Stop();

        hitSound.Play();
        what.GetComponent<EnemiesInterface>().damaged(spellDamage, spellType, spellRank);
        spellEffect.GetComponent<SpriteRenderer>().enabled = false;

        spellEffect.transform.position = GameObject.Find("Player").transform.position;
    }

    public IEnumerator lightningTravel(GameObject what)
    {
        Vector3 difference = what.transform.position - spellEffect.transform.position;
        float look = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        spellEffect.transform.rotation = Quaternion.LookRotation(Vector3.forward, -difference);

        if(light1.GetComponent<Animator>().speed == 0)
        {
            light1.GetComponent<Animator>().speed = 1;
        }
        float length =  Vector3.Distance(what.transform.position, GameObject.Find("Player").transform.position);
        spellEffect.GetComponent<SpriteRenderer>().size = new Vector2(spellEffect.GetComponent<SpriteRenderer>().size.x, length * 4);
        spellEffect.GetComponent<SpriteRenderer>().enabled = true;
        spellEffect.GetComponent<Animator>().Play("Lightning", -1, 0);
        what.GetComponent<EnemiesInterface>().damaged(spellDamage, spellType, spellRank);

        yield return new WaitForSeconds(.5f);
        spellEffect.GetComponent<SpriteRenderer>().enabled = false;
        yield return null;
    }

    public static void Reset()
    {
        word = null;
        edgeColliders.Clear();
        Destroy(ins);
        outline = null;
        spellChosen = false;
        spellReady = false;
    }


    public static void backOn()
    {
        ins.GetComponent<WordCheck>().Reset();
    }

}
