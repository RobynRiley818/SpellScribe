using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loading : MonoBehaviour
{

    public static Loading instance;
    private AsyncOperation operation;
    private bool isLoading;
    private float progress;
    public TextMeshProUGUI percent;

    private float minLoadTime = 1;
    private float currentTime = 0;

    Animator anim;

    private bool didFadeOut = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(this.gameObject);
            return;
        }

        Hide();
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (isLoading)
        {
            SetProgress(operation.progress);
            if (operation.isDone && !didFadeOut)
            {
                anim.SetTrigger("Hide");
                didFadeOut = true;

            }
            else
            {
                currentTime += Time.deltaTime;
                if (currentTime > minLoadTime)
                {
                    operation.allowSceneActivation = true;

                }
            }
        }

    }

    private void SetProgress(float progress)
    {
        percent.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }


    private void Hide()
    {
        operation = null;
        isLoading = false;
        gameObject.SetActive(false);
    }

    public void Show(AsyncOperation newOperation)
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Show");
        operation = newOperation;
        operation.allowSceneActivation = false;
        SetProgress(0f);
        isLoading = true;

        didFadeOut = false;

        currentTime = 0;

    }

}
