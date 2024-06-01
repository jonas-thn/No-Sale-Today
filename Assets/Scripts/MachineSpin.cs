using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSpin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 1;

    Vector3 rotationVector;

    private void Update()
    {
        rotationVector = new Vector3(0, 0, spinSpeed * Time.deltaTime);
        transform.Rotate(rotationVector);
    }
}
