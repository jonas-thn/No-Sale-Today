using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasLogic : MonoBehaviour
{
    [SerializeField] InteractionDisplay fassDsplay;
    [SerializeField] GameObject glas;
    [SerializeField] InteractionDisplay glasDIsplay;
    [SerializeField] AudioSource glasAudioSource;
    AudioSource blubber;

    private void Awake()
    {
        blubber = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!GameTimer.Instance.glasDa)
        {
            glas.SetActive(false);
            glasDIsplay.gameObject.SetActive(false);
        }

        if (!GameTimer.Instance.fassDa)
        {
            fassDsplay.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        FillGlas();
        GrabGlas();
    }

    private void FillGlas()
    {
        if (fassDsplay.active && Input.GetMouseButtonDown(0) && GameTimer.Instance.leeresGlas)
        {
            blubber.Play();
            GameTimer.Instance.flüssigkeitGlas = true;
            fassDsplay.gameObject.SetActive(false);
            GameTimer.Instance.fassDa = false;
        }
    }

    private void GrabGlas()
    {
        if (glasDIsplay.active && Input.GetMouseButtonDown(0) && !GameTimer.Instance.schlüssel)
        {
            glasAudioSource.Play();
            GameTimer.Instance.leeresGlas = true;
            glas.SetActive(false);
            glasDIsplay.gameObject.SetActive(false);
            GameTimer.Instance.glasDa = false;
        }
    }
}
