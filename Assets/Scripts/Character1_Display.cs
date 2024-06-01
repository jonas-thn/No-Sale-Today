using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1_Display : MonoBehaviour
{
    [Header("Breathing")]
    [SerializeField] Material testMaterial;
    [SerializeField] float breatheSpeed = 0.1f;
    [SerializeField] float movementDamping = 0.1f;
    public float maxGlow = 1;

    [Header("Looking")]
    [SerializeField] Transform lookTarget;
    public bool shouldFollow = true;

    float value = 0;
    int add = 1;

    private void Update()
    {
        Looking();

        Breathing();
    }

    private void Looking()
    {
        if (shouldFollow && lookTarget != null)
        {
            Vector3 direction = (lookTarget.position - transform.position).normalized;
            direction = new Vector3(Mathf.Clamp(direction.x, -0.55f, 0.3f), 0, Mathf.Max(direction.z, 0.9f));
            transform.forward = direction;
        }
    }

    private void Breathing()
    {
        if (value == 0.2f && add == -1)
        {
            add = 1;
        }
        else if (value == maxGlow & add == 1)
        {
            add = -1;
        }

        value += Time.deltaTime * add * breatheSpeed;
        value = Mathf.Clamp(value, 0.2f, maxGlow);

        //transform.position += new Vector3(0, add * breatheSpeed * movementDamping * Time.deltaTime, 0);
        transform.localScale += new Vector3(add * breatheSpeed * movementDamping * Time.deltaTime, add * breatheSpeed * movementDamping * Time.deltaTime, add * breatheSpeed * movementDamping * Time.deltaTime);

        testMaterial.SetColor("_EmissionColor", new Color(value * 2, value * 2, value * 2, 1));
    }
}
