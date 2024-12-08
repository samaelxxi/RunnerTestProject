using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // TODO move to SO?
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardAcceleration;
    [SerializeField] float forwardMaxSpeed;
    [SerializeField] float horizontalSpeed;

    public bool ShouldRun { get; private set; }
    public float Speed => ShouldRun ? forwardSpeed : 0;

    Rigidbody _rigidbody;
    float _rightInput;
    float _horizontalLimit;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetMoveInput();
    }

    public void StopRunning()
    {
        ShouldRun = false;
        forwardSpeed = 0;
    }

    public void StartRunning()
    {
        ShouldRun = true;
    }

    void FixedUpdate()
    {
        if (!ShouldRun) return;

        UpdateSpeed();
        Move();
    }

    public void SetHorizontalLimit(float limit)
    {
        _horizontalLimit = limit;
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
        var newPos = transform.position + rightDelta   * transform.right 
                                        + forwardDelta * transform.forward;
        newPos.x = Mathf.Clamp(newPos.x, -_horizontalLimit, _horizontalLimit);

        _rigidbody.MovePosition(newPos);
    }
}
