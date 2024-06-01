using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour 
{
    public static GameTimer Instance;

    public float time;

    public bool rezept = false;
    public bool leeresGlas = false;
    public bool flüssigkeitGlas = false;
    public bool pilz = false;
    public bool kristall = false;
    public bool schlüssel = false;

    public bool erstesMalSchlüssel = true;
    public bool erstesMalMain = true;
    public bool kesselLeer = true;
    public bool glasDa = true;
    public bool fassDa = true;
    public bool truheDa = true;
    public bool kartenDa = true;
    public bool händlerRechtsFertig = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu Scene");
        }
    }

    
}
