using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputMove;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 5;

    private Vector2 _moveVector2 = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        _moveVector2 = _inputMove.action.ReadValue<Vector2>();

        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 moveVector = new Vector3(_moveVector2.x, 0, _moveVector2.y);

        _controller.Move(moveVector * Time.deltaTime * _speed);
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(_moveVector2.x, 0, _moveVector2.y);

        if(direction.magnitude == 0) return;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speed);
    }
}