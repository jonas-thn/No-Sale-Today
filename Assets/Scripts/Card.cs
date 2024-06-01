using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] GameObject rand;
    [SerializeField] Transform stapel;
    [SerializeField] float moveSpeed = 0.1f;
    AudioSource clickSound;

    [Header("Number")]
    [SerializeField] int index;
    [SerializeField] HändlerLeft gewinnerCheck;

    Vector3 startPosition;
    bool isMoving = false;

    bool anfang = true;

    bool mausDrauf = false;

    private void Awake()
    {
        clickSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        rand.SetActive(false);
        startPosition = transform.position;
    }

    private void Update()
    {
        rand.SetActive(mausDrauf);
    }

    private void OnMouseEnter()
    {
        mausDrauf = true;
        clickSound.Play();

        if ((cameraSystem.currentCam == cam) && !isMoving && anfang)
        {
            transform.position = startPosition + Vector3.up * 0.1f;
        }
    }

    private void OnMouseExit()
    {
        mausDrauf = false;

        if (!isMoving && anfang)
        {
            transform.position = startPosition; 
        }
    }

    private void OnMouseDown()
    {
        if (anfang)
        {
            isMoving = true;

            StopAllCoroutines();
            StartCoroutine(MoveToStapel()); 
        }
        else
        {
            isMoving = true;

            StopAllCoroutines();
            StartCoroutine(MoveBack());
        }
    }

    private IEnumerator MoveToStapel()
    {
        while (Vector3.Distance(transform.position, stapel.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, stapel.position, moveSpeed);
            yield return null;
        }

        stapel.position += (Vector3.up * 0.05f);

        gewinnerCheck.numbers.Add(index);

        isMoving = false;
        anfang = false;
    }

    private IEnumerator MoveBack()
    {
        while (Vector3.Distance(transform.position, startPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed);
            yield return null;
        }

        stapel.position -= (Vector3.up * 0.05f);

        gewinnerCheck.numbers.Remove(index);

        isMoving = false;
        anfang = true;
    }
}
