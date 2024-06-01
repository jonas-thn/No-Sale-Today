using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainAudio : MonoBehaviour
{
    public static MainAudio Instance;

    [SerializeField] AudioSource music1;
    [SerializeField] AudioSource music2;

    [Range(0,1)][SerializeField] float volumeMenu;
    [Range(0, 1)][SerializeField] float volumeMain;
    [Range(0, 1)][SerializeField] float volumeLoch;
    [Range(0, 1)][SerializeField] float volumeAttick;

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
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if(arg1.buildIndex == 0)
        {
            music1.volume = volumeMenu;
            music2.volume = volumeMenu / 2.3f;
        }

        if (arg1.buildIndex == 1)
        {
            music1.volume = volumeMain;
            music2.volume = volumeMain / 2.3f;
        }

        if (arg1.buildIndex == 2)
        {
            music1.volume = volumeLoch;
            music2.volume = volumeLoch / 2.3f;
        }

        if (arg1.buildIndex == 0)
        {
            music1.volume = volumeAttick;
            music2.volume = volumeAttick / 2.3f;
        }

    }
}
