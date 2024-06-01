using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] AudioSource splash;

    public void PlaySplash()
    {
        splash.Play();
    }
}
