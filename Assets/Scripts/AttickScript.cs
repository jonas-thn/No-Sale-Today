using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttickScript : MonoBehaviour
{
    [SerializeField] UiFade uifade;
    [SerializeField] Transform kesselFlüssigkeit;
    [SerializeField] float risingSpeed;
    [SerializeField] GameObject pilz;
    [SerializeField] private GameObject pilzChild;
    [SerializeField] GameObject crystal;
    [SerializeField] GameObject directionalLight;
    [SerializeField] AudioSource brewing;

    private MeshRenderer crystalRenderer;
    private Animator crystalAnimator;
    private Animator pilzAnimator;

    AudioSource finishedAudio;

    bool goBack = false;

    bool gewonnen = false;

    bool oneTime = true;

    private void Awake()
    {
        crystalRenderer = crystal.GetComponent<MeshRenderer>();
        crystalAnimator = crystal.GetComponent<Animator>();
        pilzAnimator = pilz.GetComponent<Animator>();
        finishedAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(QualitySettings.GetQualityLevel() == 2)
        {
            directionalLight.SetActive(true);
        }
        else
        {
            directionalLight.SetActive(false);
        }


        StartCoroutine(uifade.FadeOut());
        StartCoroutine(Cooldown());

        if(!GameTimer.Instance.kesselLeer)
        {
            kesselFlüssigkeit.localPosition = new Vector3(kesselFlüssigkeit.localPosition.x, 1.65f, kesselFlüssigkeit.localPosition.z);
        }

        if(!GameTimer.Instance.pilz || !GameTimer.Instance.kristall || GameTimer.Instance.kesselLeer)
        {
            if (!GameTimer.Instance.pilz || !GameTimer.Instance.kristall || !GameTimer.Instance.flüssigkeitGlas)
            {
                //pass
            }
            else
            {
                gewonnen = true;
                return;
            }

            StartCoroutine(MainUI.Instance.ShowText(MainUI.Instance.missingIngredientsText));
        }
        else
        {
            gewonnen = true;
        }

        if(!gewonnen)
        {
            crystalRenderer.enabled = false;
            pilzChild.SetActive(false);
        }
        else
        {
            crystalRenderer.enabled = true;
            pilzChild.SetActive(true);
        }

        
    }

    private void Update()
    {
        if (!GameTimer.Instance.kesselLeer && !brewing.isPlaying)
        {
            brewing.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!gewonnen)
            {
                if (goBack)
                {
                    StartCoroutine(uifade.FadeIn());
                    StartCoroutine(GoBack());
                    goBack = false;
                } 
            }
        }

        if(GameTimer.Instance.flüssigkeitGlas && GameTimer.Instance.kesselLeer)
        {
            StartCoroutine(Füllen());
            GameTimer.Instance.flüssigkeitGlas = false;
            GameTimer.Instance.leeresGlas = false;
            GameTimer.Instance.kesselLeer = false;
        }

        if(gewonnen)
        {
            crystalAnimator.SetTrigger("fall");
            pilzAnimator.SetTrigger("fall");

            if(oneTime)
            {
                StartCoroutine(MainUI.Instance.ShowText(MainUI.Instance.potionBrewingText));

                StartCoroutine(Gewonnen());

                oneTime = false;
            }
        }
    }

    private void OnDestroy()
    {
        MainUI.Instance.missingIngredientsText.color = new Color(1,1,1,0);
        MainUI.Instance.potionBrewingText.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator GoBack()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main Scene");
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        goBack = true;
    }

    private IEnumerator Füllen()
    {
        GameTimer.Instance.kesselLeer = false;

        while (kesselFlüssigkeit.localPosition.y < 1.65)
        {
            kesselFlüssigkeit.Translate(Vector3.forward * Time.deltaTime * risingSpeed);
            yield return null;
        }
    }

    private IEnumerator Gewonnen()
    {
        int minuten = Mathf.RoundToInt(GameTimer.Instance.time / 60);
        int sekunden = Mathf.RoundToInt(GameTimer.Instance.time % 60);

        yield return new WaitForSeconds(2.5f);
        finishedAudio.Play();
        yield return new WaitForSeconds(2.5f);

        if(sekunden > 9)
        {
            MainUI.Instance.gewonnenPlatzhalter.gameObject.SetActive(true);
            MainUI.Instance.timeText.text = $"{minuten}:{sekunden}";
        }
        else
        {
            MainUI.Instance.gewonnenPlatzhalter.gameObject.SetActive(true);
            MainUI.Instance.timeText.text = $"{minuten}:0{sekunden}";
        }
        
    }

}
