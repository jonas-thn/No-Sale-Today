using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    [SerializeField] GameObject glas;
    [SerializeField] GameObject flüssigkeit;
    [SerializeField] GameObject schlüssel;

    private void Update()
    {
        if(GameTimer.Instance.schlüssel)
        {
            schlüssel.SetActive(true);
        }
        else
        {
            schlüssel.SetActive(false);
        }

        if(GameTimer.Instance.leeresGlas)
        {
            glas.SetActive(true);
        }
        else
        {
            glas.SetActive(false);
        }

        if (GameTimer.Instance.flüssigkeitGlas)
        {
            flüssigkeit.SetActive(true);
        }
        else
        {
            flüssigkeit.SetActive(false);
        }
    }
}
