using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{
    enum Cameras
    {
        middle,
        left,
        right,
        ecke,
        treppe,
        loch,
        truhe,
        leftHändler,
        rightHändler
    }

    [SerializeField] Transform targetPosition;

    [SerializeField] float timeBetweenMoves = 3;
    [SerializeField] UiFade uiFade;
    [SerializeField] float cameraTilt = 0.2f;

    [SerializeField] InteractionDisplay truheInteract;
    [SerializeField] InteractionDisplay leftHändlerInteract;
    [SerializeField] InteractionDisplay rightHandlerInteract;

    [Header("Main")]
    [SerializeField] private CinemachineVirtualCamera middleCam;
    Vector3 middlePosition;

    [SerializeField] private CinemachineVirtualCamera leftCam;
    Vector3 leftPosition;

    [SerializeField] private CinemachineVirtualCamera rightCam;
    Vector3 rightPosition;

    [SerializeField] private CinemachineVirtualCamera eckeCam;
    Vector3 eckePosition;


    [Header("Ausgänge")]
    [SerializeField] private CinemachineVirtualCamera treppeCam;
    Vector3 treppePosition;

    [SerializeField] private CinemachineVirtualCamera lochCam;
    Vector3 lochPosition;


    [Header("Interact")]

    [SerializeField] private CinemachineVirtualCamera truheCam;
    Vector3 truhePosition;

    [SerializeField] private CinemachineVirtualCamera leftHändlerCam;
    Vector3 leftHändlerPosition;

    [SerializeField] private CinemachineVirtualCamera rightHändlerCam;
    Vector3 rightHändlerPosition;


    public CinemachineVirtualCamera currentCam;

    private Cameras cameraType;

    private int maxPriority = 15;
    public bool canMove = true;
    public float timer = 0;

    Vector2 mousePos;

    Coroutine warningRoutine;

    private void Start()
    {
        currentCam = middleCam;
        cameraType = Cameras.middle;
        StartCoroutine(uiFade.FadeOut());

        middlePosition = middleCam.transform.position;
        leftPosition = leftCam.transform.position;
        rightPosition = rightCam.transform.position;
        eckePosition = eckeCam.transform.position;

        treppePosition = treppeCam.transform.position;
        lochPosition = lochCam.transform.position;

        truhePosition = truheCam.transform.position;
        leftHändlerPosition = leftHändlerCam.transform.position;
        rightHändlerPosition = rightHändlerCam.transform.position;

        if(GameTimer.Instance.erstesMalMain)
        {
            StartCoroutine(MainUI.Instance.ShowText(MainUI.Instance.mainSceneText));
            GameTimer.Instance.erstesMalMain = false;
        }
    }

    private void Update()
    {
        if(!canMove)
        {
            timer += Time.deltaTime;

            if(timer > timeBetweenMoves)
            {
                canMove = true;
            }
        }

        
        HandleMiddle();
        HandleLeft();
        HandleRight();
        HandleEcke();
        HandleTruhe();
        HandleHändlerLeft();
        HandleHändlerRight();

        TiltCamera();
        
        targetPosition.position = Camera.main.transform.position;

    }

    private void OnDestroy()
    {
        MainUI.Instance.mainSceneText.color = new Color(1, 1, 1, 0);
        MainUI.Instance.tasseWarnung.color = new Color(1, 1, 1, 0);
        MainUI.Instance.schlüsselWarnung.color = new Color(1, 1, 1, 0);
    }

    private void HandleMiddle()
    {
        if (cameraType == Cameras.middle && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && canMove)
        {
            maxPriority++;
            leftCam.Priority = maxPriority;
            currentCam = leftCam;
            cameraType= Cameras.left;

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.middle && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && canMove)
        {
            maxPriority++;
            rightCam.Priority = maxPriority;
            currentCam = rightCam;
            cameraType = Cameras.right;

            canMove = false;
            timer = 0;
        }
    }

    private void HandleLeft()
    {
        if (cameraType == Cameras.left && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && canMove)
        {
            maxPriority++;
            middleCam.Priority = maxPriority;
            currentCam = middleCam;
            cameraType = Cameras.middle;

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.left && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canMove)
        {
            maxPriority++;
            treppeCam.Priority = maxPriority;
            currentCam = treppeCam;
            cameraType = Cameras.treppe;

            StartCoroutine(uiFade.FadeIn());
            StartCoroutine(LoadAttickScene());

            canMove = false;
            timer = 0;
        }
    }

    private void HandleRight()
    {
        if (cameraType == Cameras.right && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && canMove)
        {
            maxPriority++;
            middleCam.Priority = maxPriority;
            currentCam = middleCam;
            cameraType = Cameras.middle;

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.right && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canMove)
        {
            maxPriority++;
            eckeCam.Priority = maxPriority;
            currentCam = eckeCam;
            cameraType = Cameras.ecke;

            canMove = false;
            timer = 0;
        }
    }

    private void HandleEcke()
    {
        if (cameraType == Cameras.ecke && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canMove)
        {
            maxPriority++;
            rightCam.Priority = maxPriority;
            currentCam = rightCam;
            cameraType = Cameras.right;

            canMove = false;
            timer = 0;
        }

        if(cameraType == Cameras.ecke && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && GameTimer.Instance.leeresGlas && canMove)
        {
            if(warningRoutine != null)
            {
                StopCoroutine(warningRoutine);
            }
            warningRoutine = StartCoroutine(MainUI.Instance.ShowText(MainUI.Instance.schlüsselWarnung));
        }
        else if (cameraType == Cameras.ecke && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canMove)
        {
            maxPriority++;
            lochCam.Priority = maxPriority;
            currentCam = lochCam;
            cameraType = Cameras.loch;

            StartCoroutine(uiFade.FadeIn());
            StartCoroutine(LoadLochScene());

            canMove = false;
            timer = 0;
        }
    }

    private void HandleTruhe()
    {
        if (truheInteract.active && Input.GetMouseButtonDown(0) && canMove)
        {
            maxPriority++;
            truheCam.Priority = maxPriority;
            currentCam = truheCam;
            cameraType = Cameras.truhe;

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.truhe && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canMove)
        {
            maxPriority++;
            middleCam.Priority = maxPriority;
            currentCam = middleCam;
            cameraType = Cameras.middle;

            canMove = false;
            timer = 0;
        }
    }

    private void HandleHändlerLeft()
    {
        if (leftHändlerInteract.active && Input.GetMouseButtonDown(0) && canMove)
        {
            maxPriority++;
            leftHändlerCam.Priority = maxPriority;
            currentCam = leftHändlerCam;
            cameraType = Cameras.leftHändler;

            leftHändlerInteract.gameObject.SetActive(false);
            rightHandlerInteract.gameObject.SetActive(false);

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.leftHändler && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canMove)
        {
            maxPriority++;
            middleCam.Priority = maxPriority;
            currentCam = middleCam;
            cameraType = Cameras.middle;

            leftHändlerInteract.gameObject.SetActive(true);
            rightHandlerInteract.gameObject.SetActive(true);

            canMove = false;
            timer = 0;
        }
    }

    private void HandleHändlerRight()
    {
        if (rightHandlerInteract.active && Input.GetMouseButtonDown(0) && canMove)
        {
            maxPriority++;
            rightHändlerCam.Priority = maxPriority;
            currentCam = rightHändlerCam;
            cameraType = Cameras.rightHändler;

            leftHändlerInteract.gameObject.SetActive(false);
            rightHandlerInteract.gameObject.SetActive(false);

            canMove = false;
            timer = 0;
        }

        if (cameraType == Cameras.rightHändler && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && canMove)
        {
            HändlerRightBack();
        }
    }

    public void HändlerRightBack()
    {
        maxPriority++;
        middleCam.Priority = maxPriority;
        currentCam = middleCam;
        cameraType = Cameras.middle;

        leftHändlerInteract.gameObject.SetActive(true);
        rightHandlerInteract.gameObject?.SetActive(true);

        canMove = false;
        timer = 0;
    }

    private IEnumerator LoadAttickScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Attick Scene");
    }

    private IEnumerator LoadLochScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Loch Scene");
    }

    private void TiltCamera()
    {
        Vector2 middle = new Vector2(0.5f, 0.5f);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos = mousePos - middle;
        mousePos = new Vector3(-mousePos.x, mousePos.y);

        Vector3 newPos = currentCam.transform.position + new Vector3(mousePos.x, mousePos.y, 0);

        if(cameraType == Cameras.middle)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, middlePosition.x - cameraTilt, middlePosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, middlePosition.y - cameraTilt, middlePosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.right)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, rightPosition.x - cameraTilt, rightPosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, rightPosition.y - cameraTilt, rightPosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.left)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, leftPosition.x - cameraTilt, leftPosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, leftPosition.y - cameraTilt, leftPosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.ecke)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, eckePosition.x - cameraTilt, eckePosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, eckePosition.y - cameraTilt, eckePosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.truhe)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, truhePosition.x - cameraTilt, truhePosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, truhePosition.y - cameraTilt, truhePosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.leftHändler)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, leftHändlerPosition.x - cameraTilt, leftHändlerPosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, leftHändlerPosition.y - cameraTilt, leftHändlerPosition.y + cameraTilt), newPos.z);
        }
        if (cameraType == Cameras.rightHändler)
        {
            newPos = new Vector3(Mathf.Clamp(newPos.x, rightHändlerPosition.x - cameraTilt, rightHändlerPosition.x + cameraTilt),
                                    Mathf.Clamp(newPos.y, rightHändlerPosition.y - cameraTilt, rightHändlerPosition.y + cameraTilt), newPos.z);
        }        

        currentCam.transform.position = Vector3.Lerp(currentCam.transform.position, newPos, Time.deltaTime);
    }

}
