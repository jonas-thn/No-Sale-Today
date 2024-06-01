using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Character2_Display : MonoBehaviour
{
    [Header("Flicker")]
    [SerializeField] Material testMaterial;
    [SerializeField] float flickerSpeed = 1;
    float valueFlicker = 0;
    int addlicker = 1;
    float maxGlow;

    [Header("Looking")]
    [SerializeField] Transform head;
    [SerializeField] Transform lookTarget;
    public bool shouldFollow = true;

    AudioSource hitSound;

    private void Awake()
    {
        hitSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Flicker();
        Looking();
    }

    private void Flicker()
    {
        if (valueFlicker == 0 && addlicker == -1)
        {
            addlicker = 1;
            maxGlow = Random.value;
        }
        else if (valueFlicker == maxGlow & addlicker == 1)
        {
            addlicker = -1;
        }

        valueFlicker += Time.deltaTime * addlicker * flickerSpeed;
        valueFlicker = Mathf.Clamp(valueFlicker, 0, maxGlow);

        testMaterial.SetColor("_EmissionColor", new Color(valueFlicker * 15, valueFlicker * 15, valueFlicker * 15, 1));
    }

    private void Looking()
    {
        if (shouldFollow && lookTarget != null)
        {
            Vector3 direction = (head.position - lookTarget.position).normalized;
            //direction = new Vector3(Mathf.Clamp(direction.x, -0.55f, 0.6f), 0, Mathf.Max(direction.z, 0.9f));
            direction = new Vector3(direction.x, 0, direction.z);
            direction = -direction;
            direction = new Vector3(Mathf.Clamp(direction.x, -0.2f, 0.1f), direction.y, direction.z);
            head.transform.forward = direction;
        }
    }

    public void PlayHitSound()
    {
        hitSound.Play();
    }
}
