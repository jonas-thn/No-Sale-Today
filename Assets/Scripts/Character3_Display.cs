using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character3_Display : MonoBehaviour
{
    [SerializeField] Material testMaterial;
    [SerializeField] float flickerSpeed = 1;
    float valueFlicker = 0;
    int addFlicker = 1;
    float maxGlow;

    private void Update()
    {
        Flicker();
    }

    private void Flicker()
    {
        if (valueFlicker == 0 && addFlicker == -1)
        {
            addFlicker = 1;
            maxGlow = Random.value;
        }
        else if (valueFlicker == maxGlow & addFlicker == 1)
        {
            addFlicker = -1;
        }

        valueFlicker += Time.deltaTime * addFlicker * flickerSpeed;
        valueFlicker = Mathf.Clamp(valueFlicker, 0, maxGlow);

        testMaterial.SetFloat("_Opacity", valueFlicker);
    }
}
