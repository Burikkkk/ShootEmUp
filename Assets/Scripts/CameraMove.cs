using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    private void Update()
    {
        position.x += speed * Time.deltaTime;
        transform.position = position;
    }
}
