using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportIdle : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        float rotationZ;
        rotationZ = Time.deltaTime * speed;
        transform.Rotate(0f, 0f, rotationZ);
    }
}
