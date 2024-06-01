using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Truhe : MonoBehaviour
{
    [SerializeField] Transform crystal;
    [SerializeField] float crystalSped = 1;
    [SerializeField] InteractionDisplay display;

    Animator animator;
    AudioSource glimmer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        glimmer = GetComponent<AudioSource>();  
    }

    private void Start()
    {
        if(!GameTimer.Instance.truheDa)
        {
            display.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        crystal.transform.Rotate(0, 0, crystalSped * Time.deltaTime);

        if (display.active && Input.GetMouseButtonDown(0) && GameTimer.Instance.schlüssel)
        {
            StartCoroutine(OpenChest());
            display.gameObject.SetActive(false);
        }
    }

    public IEnumerator OpenChest()
    {
        animator.SetTrigger("open");
        glimmer.Play();

        yield return new WaitForSeconds(1);
        GameTimer.Instance.schlüssel = false;
        yield return new WaitForSeconds(1);
        GameTimer.Instance.kristall = true;
        GameTimer.Instance.truheDa = false;
    }
}
