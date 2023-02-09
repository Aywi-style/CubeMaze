using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private Vector3 _currentVelocity;

    private InputManager _inputManager;

    private void Start()
    {
        _currentVelocity = new Vector3();
        _inputManager = InputManager.Get();
        _speed = 5;
    }

    void Update()
    {
        ChangeAnimationParams();
    }

    private void FixedUpdate()
    {
        if (_inputManager.Horizontal == 0 && _inputManager.Vertical == 0)
        {
            return;
        }

        Move();
        RotateBodyByAxisY();
    }

    private void Move()
    {
        Vector3 moveVector = new Vector3(_inputManager.Horizontal, 0, _inputManager.Vertical);
        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        _currentVelocity.x = moveVector.x * _speed;
        _currentVelocity.y = _rigidbody.velocity.y;
        _currentVelocity.z = moveVector.z * _speed;

        _rigidbody.velocity = _currentVelocity;
    }

    private void ChangeAnimationParams()
    {
        Vector3 transformForward = transform.forward;
        Vector3 transformRight = transform.right;

        transformForward.y = 0;
        transformRight.y = 0;

        Vector3 moveVector = Vector3.forward * _inputManager.Vertical + Vector3.right * _inputManager.Horizontal;
        moveVector = Vector3.ClampMagnitude(moveVector, 1);

        Vector3 relativeVector = transform.InverseTransformDirection(moveVector);

        _animator.SetFloat("DirectionX", relativeVector.x);
        _animator.SetFloat("DirectionY", relativeVector.z);
    }

    private void RotateBodyByAxisY()
    {
        Vector3 lookDirection = new Vector3(_inputManager.Horizontal, 0, _inputManager.Vertical);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.fixedDeltaTime);
    }
}
