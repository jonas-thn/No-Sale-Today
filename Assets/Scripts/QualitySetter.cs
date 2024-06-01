using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualitySetter : MonoBehaviour
{
    public static QualitySetter Instance;

    public bool oneTime = true;

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
    }

    private void Start()
    {
        if(oneTime)
        {
            QualitySettings.SetQualityLevel(0);
            oneTime = false;
        }
    }
}
