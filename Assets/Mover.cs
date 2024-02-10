using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float rotationsPerSecond = 0.1f;
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 120 * Mathf.PI * rotationsPerSecond);
    }
}
