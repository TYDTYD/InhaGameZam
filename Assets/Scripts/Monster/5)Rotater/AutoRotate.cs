using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [Tooltip("초당 회전 속도 (도 단위)")]
    public Vector3 rotationSpeed = new Vector3(0f, 0f, 200f); // Z축 회전

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}