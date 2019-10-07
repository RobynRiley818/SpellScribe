using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public static int dead;
    public static int totDead;
    public float timer;
    private int iterate;
    public int pickup = 10;
    public int wave = 1;

    public List<GameObject> enemies = new List<GameObject>();

    public GameObject manager;

    bool waveOver = false;

    int waveEnemies;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        iterate = 0;
        //Start on Wave 1
        wave = 1;
        dead = 0;
        totDead = 0;

        manager = GameObject.Find("Game Manager(Clone)");
        StartCoroutine(Waves(1));
    }

    // Update is called once per frame
    void Update()
    {
        //Timer in seconds
        timer += Time.deltaTime;
        //Do wave if you are on current wave
        /*if (wave == 1)
        {
            wave1();
        }
        if (wave == 2)
        {
            wave2();
        }
        if (wave == 3)
        {
            wave3();
        }

        //Every wave is 5 enemies, so every 5 kills (or escapes) wave increases and the timer resets
        if (dead == waveEnemies)
        {
            dead = 0;
            wave++;
            timer = 0;
            iterate = 0;
        }*/

        //If the total number of enemies in the game are killed (or escaped), wait 3 seconds and go to end screen
        if (wave == 4 && timer >= 2)
        {

            manager.GetComponent<GameManager>().wavesCleared = wave - 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    //Obvious instantiation stuff
    void spawn1()
    {
        GameObject ins = Instantiate(enemy1, this.gameObject.transform);
        enemies.Add(ins);
    }

    void spawn2()
    {
        GameObject ins = Instantiate(enemy2, this.gameObject.transform);
        enemies.Add(ins);
    }

    void spawn3()
    {
        GameObject ins = Instantiate(enemy3, this.gameObject.transform);
        enemies.Add(ins);
    }

    void wave1()
    {
        switch (GameManager.current)
        {
            case GameManager.Difficulty.Easy:
                if (wave == 1)
                {
                    waveEnemies = 4;
                    //Enemy spawns after 1 second. It then iterates to prime the next one to spawn.
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 8 && iterate == 1)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 15 && iterate == 2)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 3)
                    {
                        spawn1();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Normal:

                if (wave == 1)
                {

                    waveEnemies = 5;
                    //Enemy spawns after 1 second. It then iterates to prime the next one to spawn.
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Hard:
                if (wave == 1)
                {
                    waveEnemies = 5;
                    //Enemy spawns after 1 second. It then iterates to prime the next one to spawn.
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
        }
    } 
    void wave2()
    {
        switch (GameManager.current)
        {
            case GameManager.Difficulty.Easy:
                if (wave == 2)
                {
                    waveEnemies = 4;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 8 && iterate == 1)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 15 && iterate == 2)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 3)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Normal:

                if (wave == 2)
                {

                    waveEnemies = 5;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Hard:

                if (wave == 2)
                {

                    waveEnemies = 5;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
        }
        
    }
    void wave3()
    {

        switch (GameManager.current)
        {
            case GameManager.Difficulty.Easy:
                if (wave == 3)
                {

                    waveEnemies = 5;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn1();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Normal:
                if (wave == 3)
                {

                    waveEnemies = 5;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
            case GameManager.Difficulty.Hard:
                if (wave == 3)
                {

                    waveEnemies = 5;
                    if (timer >= 1 && iterate == 0)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 6 && iterate == 1)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 11 && iterate == 2)
                    {
                        spawn2();
                        iterate++;
                    }
                    if (timer >= 16 && iterate == 3)
                    {
                        spawn3();
                        iterate++;
                    }
                    if (timer >= 21 && iterate == 4)
                    {
                        spawn2();
                        iterate++;
                    }
                }
                break;
        }
        
    }


    IEnumerator Waves(int waveNum)
    {
        enemies.Clear();
        yield return new WaitForSeconds(1.0f);
        dead = 0;
        switch (waveNum)
        {
            case 1:
                switch (GameManager.current)
                {
                    case GameManager.Difficulty.Easy:
                        waveEnemies = 5;
                        
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn2();
                        break;
                    case GameManager.Difficulty.Normal:
                        waveEnemies = 5;
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn3();
                        yield return new WaitForSeconds(6.0f);
                        spawn2();
                        yield return new WaitForSeconds(6.0f);
                        break;
                    case GameManager.Difficulty.Hard:
                        waveEnemies = 5;
                        spawn1();
                        yield return new WaitForSeconds(5.0f);
                        spawn1();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn2();
                        yield return new WaitForSeconds(5.0f);
                        break;
                }
                break;
            case 2:
                switch (GameManager.current)
                {
                    case GameManager.Difficulty.Easy:
                        waveEnemies = 5;
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn3();
                        yield return new WaitForSeconds(7.0f);
                        spawn2();
                        yield return new WaitForSeconds(7.0f);
                        break;
                    case GameManager.Difficulty.Normal:
                        waveEnemies = 5;
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn3();
                        yield return new WaitForSeconds(6.0f);
                        spawn3();
                        yield return new WaitForSeconds(6.0f);
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn2();
                        yield return new WaitForSeconds(6.0f);
                        break;
                    case GameManager.Difficulty.Hard:
                        waveEnemies = 5;
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn1();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn2();
                        yield return new WaitForSeconds(5.0f);
                        break;
                }
                break;
            case 3:
                switch (GameManager.current)
                {
                    case GameManager.Difficulty.Easy:
                        waveEnemies = 5;
                        spawn3();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        spawn2();
                        yield return new WaitForSeconds(7.0f);
                        spawn3();
                        yield return new WaitForSeconds(7.0f);
                        spawn1();
                        yield return new WaitForSeconds(7.0f);
                        break;
                    case GameManager.Difficulty.Normal:
                        waveEnemies = 5;
                        spawn2();
                        yield return new WaitForSeconds(6.0f);
                        spawn3();
                        yield return new WaitForSeconds(6.0f);
                        spawn1();
                        yield return new WaitForSeconds(6.0f);
                        spawn3();
                        yield return new WaitForSeconds(6.0f);
                        spawn2();
                        yield return new WaitForSeconds(6.0f);
                        break;
                    case GameManager.Difficulty.Hard:
                        waveEnemies = 5;
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn2();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn3();
                        yield return new WaitForSeconds(5.0f);
                        spawn2();
                        yield return new WaitForSeconds(5.0f);
                        break;
                }
                break;
        }
        bool Continue = false;

        while (!Continue)
        {
            foreach(GameObject i in enemies)
            {
                if(i == null)
                {
                    Continue = true;
                }
                else
                {
                    Continue = false;
                    break;
                }
            }
            yield return null;
        }

        if(waveNum < 3)
        {
            StartCoroutine(Waves(waveNum + 1));
        }
        else
        {
            yield return new WaitForSeconds(1.0f);
            manager.GetComponent<GameManager>().wavesCleared = waveNum;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void clear()
    {
        //enemies.TrimExcess();
    }
}
