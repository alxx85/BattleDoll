using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAttack))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    private PlayerAttack _playerAttack;
    private float _mouseRotate;
    private float _currentSpeed;
    private Vector3 _moveDirection;
    private Vector3 _direction;

    public float RotateSpeed => _rotateSpeed;

    public event UnityAction<float, float> ChangeMoveDirection;
    public event UnityAction PlayerAttacks;

    private void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _currentSpeed = _speed;
    }

    private void OnEnable()
    {
        GetComponent<CharacterProperties>().ScaleChanged += OnScaleChanged;
    }

    private void OnDisable()
    {
        ChangeMoveDirection?.Invoke(0, 0);
        GetComponent<CharacterProperties>().ScaleChanged -= OnScaleChanged;
    }

    private void Update()
    {
        if (_playerAttack.IsAttacking == false)
        {
            PlayerInput();

            transform.Rotate(0, _mouseRotate, 0);
            transform.Translate(_moveDirection);
        }
    }

    public void ChangedRotateSpeed(float value)
    {
        _rotateSpeed = value;
    }

    private void PlayerInput()
    {
        _moveDirection = Vector3.zero;
        _moveDirection += Vector3.right * Input.GetAxis("Horizontal");
        _moveDirection += Vector3.forward * Input.GetAxis("Vertical");
        
        if (_direction != _moveDirection)
        {
            ChangeMoveDirection?.Invoke(_moveDirection.x, _moveDirection.z);
            _direction = _moveDirection;
        }
        
        _moveDirection = _moveDirection.normalized * _currentSpeed * Time.deltaTime;
        _mouseRotate = Input.GetAxis("Mouse X") * _rotateSpeed;

        if (Input.GetMouseButtonDown(0))
            PlayerAttacks?.Invoke();
    }

    private void OnScaleChanged(float newScale)
    {
        _currentSpeed = _speed + _speed * newScale;
    }
}
