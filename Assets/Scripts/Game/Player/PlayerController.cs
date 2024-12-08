using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace RunnerGame.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerControllerSettingsSO settings;

        float _forwardSpeed;

        public bool ShouldRun { get; private set; }
        public float Speed => ShouldRun ? _forwardSpeed : 0;

        Rigidbody _rigidbody;
        float _rightInput;
        float _horizontalLimit;


        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _forwardSpeed = settings.StartForwardSpeed;
        }

        void Update()
        {
            GetMoveInput();
        }

        public void StopRunning()
        {
            ShouldRun = false;
            _forwardSpeed = 0;
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
            _forwardSpeed = Mathf.Clamp(_forwardSpeed + settings.ForwardAcceleration * Time.fixedDeltaTime, 
                                    0, settings.ForwardMaxSpeed);
        }

        void Move()
        {
            float rightDelta = _rightInput * settings.HorizontalSpeed * Time.fixedDeltaTime;
            float forwardDelta = _forwardSpeed * Time.fixedDeltaTime;
            var newPos = transform.position + rightDelta   * transform.right 
                                            + forwardDelta * transform.forward;
            newPos.x = Mathf.Clamp(newPos.x, -_horizontalLimit, _horizontalLimit);

            _rigidbody.MovePosition(newPos);
        }
    }
}