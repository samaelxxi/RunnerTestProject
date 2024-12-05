using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardAcceleration;
    [SerializeField] float forwardMaxSpeed;
    [SerializeField] float horizontalSpeed;

    public bool ShouldRun { get; set; }

    Rigidbody _rigidbody;
    float _rightInput;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetMoveInput();
    }

    void FixedUpdate()
    {
        ShouldRun = true;
        if (!ShouldRun) return;

        UpdateSpeed();
        Move();
    }

    void GetMoveInput()
    {
        _rightInput = Input.GetAxis("Horizontal");
    }

    void UpdateSpeed()
    {
        forwardSpeed = Mathf.Clamp(forwardSpeed + forwardAcceleration * Time.fixedDeltaTime, 
                                   0, forwardMaxSpeed);
    }

    void Move()
    {
        float rightDelta = _rightInput * horizontalSpeed * Time.fixedDeltaTime;
        float forwardDelta = forwardSpeed * Time.fixedDeltaTime;
        Debug.Log(forwardDelta);
        var newPos = transform.position + rightDelta * transform.right 
                                        + forwardDelta * transform.forward;

        _rigidbody.MovePosition(newPos);
    }
}
