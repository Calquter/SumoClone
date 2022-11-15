using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Character
{
    [Range(2, 4)] [SerializeField] private float _minTime;
    [Range(4, 6)] [SerializeField] private float _maxTime;

    private float _timer = 0f;

    private float _targetAngle;

    [SerializeField] private Transform _controlPoint;

    private void Start()
    {
        SetUpTimer();
        _targetAngle = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void LateUpdate()
    {
        HitAllow();

        if (!ControlPlatformBounds())
            _timer = 0;
    }

    private void ChangeDirection()
    {
        if (isTakenHit)
            return;

        if (_timer <= 0)
        {
            _targetAngle = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);

            SetUpTimer();
        }
        else
            _timer -= Time.deltaTime;
    }

    private void MoveForward()
    {
        if (!isTakenHit)
            _rigidBody.velocity = transform.forward * _playerSpeed - transform.up;
    }

    private bool ControlPlatformBounds()
    {
        return Physics.Raycast(_controlPoint.position, -_controlPoint.up, 10f);
    }

    private void SetUpTimer() => _timer = Random.Range(_minTime, _maxTime);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // Layer 6 is hitable layer.
            HitToEnemies(collision.gameObject.GetComponent<Rigidbody>());
    }

}
