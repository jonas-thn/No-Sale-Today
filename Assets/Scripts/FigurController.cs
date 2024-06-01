using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigurController : MonoBehaviour
{
    [SerializeField] CameraSystem cameraSystem;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] float speed;
    [SerializeField] GameObject pilz;
    [SerializeField] Animator animator;
    [SerializeField] Transform hand1;
    [SerializeField] Transform hand2;
    [SerializeField] float abstand = 0.1f;
    [SerializeField] Transform head;
    [SerializeField] InteractionDisplay display;
    [SerializeField] float walkDIstance = 9;
    [SerializeField] SpriteRenderer dodge;

    Vector2 localPos = Vector2.zero;
    float leftRight;

    bool startGame = true;
    bool isDead = false;

    private void Start()
    {
        if(GameTimer.Instance.händlerRechtsFertig)
        {
            display.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        MovePlayer();
        TestDead();

        if (startGame && cameraSystem.currentCam == cam && cameraSystem.canMove)
        {
            StartCoroutine(StartGame());
            StartCoroutine(ShowDodgeText());
        }

        if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && cameraSystem.canMove && cameraSystem.currentCam == cam)
        {
            StopAllCoroutines();
            animator.SetTrigger("dead");
            startGame = true;
        }

    }

    private void TestDead()
    {
        if (!isDead)
        {
            if ((Vector3.Distance(head.position, hand1.position) < abstand) || (Vector3.Distance(head.position, hand2.position) < abstand))
            {
                dodge.enabled = false;

                isDead = true;
                StopAllCoroutines();
                animator.SetTrigger("dead");
                cameraSystem.HändlerRightBack();
                startGame = true;
            } 
        }
    }

    private IEnumerator StartGame()
    {
        isDead = false;
        startGame = false;
        animator.SetTrigger("game");
        yield return new WaitForSeconds(20);
        cameraSystem.timer = 0;
        cameraSystem.canMove = false;
        pilz.SetActive(true);
        yield return new WaitForSeconds(1);
        GameTimer.Instance.pilz = true;
        pilz.SetActive(false);
        display.gameObject.SetActive(false);
        display.active = false;
        Destroy(display.gameObject);
        cameraSystem.HändlerRightBack();
        GameTimer.Instance.händlerRechtsFertig = true;
        this.gameObject.SetActive(false);
    }

    private void MovePlayer()
    {
        leftRight = Input.GetAxisRaw("Horizontal");

        if(cameraSystem.currentCam == cam)
        {
            if ((-walkDIstance < transform.localPosition.x) && (transform.localPosition.x < walkDIstance))
            {
                transform.localPosition += new Vector3(leftRight * Time.deltaTime * -speed, 0, 0); 
            }
            else if(-walkDIstance >= transform.localPosition.x && leftRight < 0)
            {
                transform.localPosition += new Vector3(leftRight * Time.deltaTime * -speed, 0, 0);
            }
            else if(walkDIstance <= transform.localPosition.x && leftRight > 0)
            {
                transform.localPosition += new Vector3(leftRight * Time.deltaTime * -speed, 0, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hand1.position, abstand);
        Gizmos.DrawWireSphere(hand2.position, abstand);
    }

    private IEnumerator ShowDodgeText()
    {
        dodge.enabled = true;

        yield return new WaitForSeconds(2);

        dodge.enabled = false;
    }
}
