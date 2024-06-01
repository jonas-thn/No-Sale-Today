using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    [SerializeField] GameObject glas;
    [SerializeField] GameObject fl�ssigkeit;
    [SerializeField] GameObject schl�ssel;

    private void Update()
    {
        if(GameTimer.Instance.schl�ssel)
        {
            schl�ssel.SetActive(true);
        }
        else
        {
            schl�ssel.SetActive(false);
        }

        if(GameTimer.Instance.leeresGlas)
        {
            glas.SetActive(true);
        }
        else
        {
            glas.SetActive(false);
        }

        if (GameTimer.Instance.fl�ssigkeitGlas)
        {
            fl�ssigkeit.SetActive(true);
        }
        else
        {
            fl�ssigkeit.SetActive(false);
        }
    }
}
