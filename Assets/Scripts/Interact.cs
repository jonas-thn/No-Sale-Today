using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    RaycastHit hit;
    InteractionDisplay display;

    private void Update()
    {
        MouseHitCheck();
    }
    private void MouseHitCheck()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseRay, out hit);

        if(hit.collider != null && hit.collider.TryGetComponent<InteractionDisplay>(out display))
        {
            display.DisplayOn();
        }
        else
        {
            display?.DisplayOff();
        }
    }

    
}
