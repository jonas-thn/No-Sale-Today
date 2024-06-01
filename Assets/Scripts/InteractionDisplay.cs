using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDisplay : MonoBehaviour
{
    enum IneractionObject
    {
        Truhe,
        HändlerLeft,
        HändlerRight,
        Glas,
        Fass,
        Rätsel
    }

    public bool active = false;

    [SerializeField] IneractionObject interactionObject;
    
    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        meshRenderer.enabled = false;
    }

    private void OnMouseEnter()
    {
        DisplayOn();
    }

    private void OnMouseExit()
    {
        DisplayOff();
    }

    public void DisplayOn()
    {
        active = true;
        meshRenderer.enabled = true;
    }

    public void DisplayOff()
    {
        active = false;
        meshRenderer.enabled = false;
    }
}
