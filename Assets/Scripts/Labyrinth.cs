using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Labyrinth : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] float cameraTilt = 1f;
    [SerializeField] float speed = 1;
    [SerializeField] Transform checkPosition;
    [SerializeField] GameObject schlüsselGroß;
    [SerializeField] GameObject schlüsselKlein;
    [SerializeField] UiFade uifade;
    [SerializeField] Animator spinneAnim;

    [SerializeField] AudioSource spiderAudio;
    [SerializeField] AudioSource ding;

    bool cooldown = false;

    Vector2 mousePos;
    Vector3 initialCameraPos;
    float angle;
    bool canMove = true;

    private void Start()
    {
        initialCameraPos = camera.transform.position;
        StartCoroutine(uifade.FadeOut());
        StartCoroutine(Cooldown());
        StartCoroutine(MainUI.Instance.ShowText(MainUI.Instance.lochText));

        if(!GameTimer.Instance.erstesMalSchlüssel)
        {
            schlüsselGroß.SetActive(false);
        }
    }

    private void Update()
    {
        TiltCamera();

        if(!canMove)
        {
            spinneAnim.SetBool("laufen", false);
            spiderAudio.Stop();
        }

        if (cooldown)
        {
            CheckMovement();
            MovePlayer();

            if (transform.position.z > 3.7f)
            {
                StartCoroutine(LoadMainScene());
            } 
        }
    }

    private void OnDestroy()
    {
        MainUI.Instance.lochText.color = new Color(1, 1, 1, 0);
    }

    private void TiltCamera()
    {
        Vector2 middle = new Vector2(0.5f, 0.5f);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos = mousePos - middle;
        mousePos = new Vector3(-mousePos.x, mousePos.y);

        Vector3 newPos = camera.transform.position + new Vector3(mousePos.x, mousePos.y, 0);

        
        
        newPos = new Vector3(Mathf.Clamp(newPos.x, initialCameraPos.x - cameraTilt, initialCameraPos.x + cameraTilt),
                             Mathf.Clamp(newPos.y, initialCameraPos.y - cameraTilt, initialCameraPos.y + cameraTilt), newPos.z);
        

        camera.transform.position = Vector3.Lerp(camera.transform.position, newPos, Time.deltaTime);
    }

    private void MovePlayer()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            angle = 0;
            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (canMove)
            {
                transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
                spinneAnim.SetBool("laufen", true);

                if(!spiderAudio.isPlaying)
                {
                    spiderAudio.Play();
                }
            }
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            angle = 180;
            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (canMove)
            {
                transform.position += new Vector3(0, 0, speed * Time.deltaTime);
                spinneAnim.SetBool("laufen", true);

                if (!spiderAudio.isPlaying)
                {
                    spiderAudio.Play();
                }
            }
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftAlt)))
        {
            angle = 270;
            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (canMove)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                spinneAnim.SetBool("laufen", true);

                if (!spiderAudio.isPlaying)
                {
                    spiderAudio.Play();
                }
            }
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            angle = 90;
            transform.rotation = Quaternion.Euler(0, angle, 0);

            if (canMove)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                spinneAnim.SetBool("laufen", true);

                if (!spiderAudio.isPlaying)
                {
                    spiderAudio.Play();
                }
            }
        }
        else
        {
            spinneAnim.SetBool("laufen", false);
            spiderAudio.Stop();
        }
    }

    private void CheckMovement()
    {
        canMove = Physics.Raycast(checkPosition.position, Vector3.down, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Schlüssel"))
        {
            ding.Play();
            schlüsselGroß.SetActive(false);
            schlüsselKlein.SetActive(true);
            GameTimer.Instance.schlüssel = true;
            GameTimer.Instance.erstesMalSchlüssel = false;
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        cooldown = true;
    }

    private IEnumerator LoadMainScene()
    {
        cooldown = false;
        StartCoroutine(uifade.FadeIn());
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main Scene");
    }
}
