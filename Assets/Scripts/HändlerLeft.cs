using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HändlerLeft : MonoBehaviour
{
    [SerializeField] GameObject stapel;
    [SerializeField] GameObject armOben;
    [SerializeField] GameObject armTisch;
    [SerializeField] GameObject karteGroß;


    List<int> richtig = new List<int> { 3, 0, 4, 2, 1 };

    public List<int> numbers = new List<int>();

    bool fertig = false;

    private void Start()
    {
        if(!GameTimer.Instance.kartenDa)
        {
            this.gameObject.SetActive(false);
            armOben.SetActive(false);

            karteGroß.SetActive(true);
            armTisch.SetActive(true);
        }
    }

    private void Update()
    {
        CheckWinner();
    }

    private void CheckWinner()
    {
        if(!fertig)
        {
            fertig = richtig.SequenceEqual(numbers);
        }

        if(fertig)
        {
            GameTimer.Instance.rezept = true;
            stapel.gameObject.SetActive(false);
            armOben.gameObject.SetActive(false);
            armTisch.gameObject.SetActive(true);
            karteGroß.gameObject.SetActive(true);
            GameTimer.Instance.kartenDa = false;
            this.gameObject.SetActive(false);
        }
    }

}
