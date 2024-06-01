using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    [Header("List")]
    [SerializeField] Image listDisplay;
    [SerializeField] Image listInv;
    [SerializeField] Image list_E;

    [Header("Other")]
    [SerializeField] Image crystalInv;
    [SerializeField] Image pilzInv;
    public Text mainSceneText;
    public Text missingIngredientsText;
    public Text potionBrewingText;
    public Text lochText;
    public Text tasseWarnung;
    public Text schlüsselWarnung;

    public GameObject gewonnenPlatzhalter;
    public Text timeText;
    AudioSource sheet;

    Canvas canvas;
    bool showList = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canvas = GetComponent<Canvas>();
        sheet = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu Scene")
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }

        if(GameTimer.Instance.pilz)
        {
            pilzInv.enabled = true;
        }
        else
        {
            pilzInv.enabled = false;
        }

        if (GameTimer.Instance.kristall)
        {
            crystalInv.enabled = true;
        }
        else
        {
            crystalInv.enabled = false;
        }

        if (GameTimer.Instance.rezept)
        {
            listInv.enabled = true;
            list_E.enabled = true;
        }
        else
        {
            listInv.enabled = false;
            list_E.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && GameTimer.Instance.rezept)
        {
            showList = !showList;
            sheet.Play();
        }

        if (showList && GameTimer.Instance.rezept)
        {
            listDisplay.enabled = true;
        }
        else
        {
            listDisplay.enabled = false;
        }
    }

    public IEnumerator ShowText(Text text)
    {
        float timer = 5;

        while(timer > 0)
        {
            timer -= Time.deltaTime;

            text.color = new Color(1, 1, 1, timer / 5);

            yield return 0;
        }
    }
}
