using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody _rigidBody;

    public float moveSpeed;
    [SerializeField] private float _rotationSpeed;
    private Vector2 _inputValues;

    private float _targetAngle = 0;
    private float _angle = 0;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _inputValues.x = joystick.Horizontal;
        _inputValues.y = joystick.Vertical;

        if (_inputValues.magnitude > .1f)
        {
            _targetAngle = Mathf.Atan2(_inputValues.x, _inputValues.y) * Mathf.Rad2Deg;

        }
        _angle = Mathf.LerpAngle(transform.eulerAngles.y, _targetAngle, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, _angle, 0);

        _rigidBody.velocity = (Vector3.forward * _inputValues.y + Vector3.right * _inputValues.x).normalized * moveSpeed;
    }
}
