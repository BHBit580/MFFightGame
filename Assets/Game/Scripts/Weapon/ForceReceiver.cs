using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void AddForce(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}
