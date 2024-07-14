using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private PlayerInput _input;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _runSpeed = 10;
    [SerializeField] private float _rotateSpeed = 5;
    [SerializeField] private Vector3 _gravityVector = new Vector3(0, -5f, 0);

    public Vector3 MoveVector => _moveVector;
    public float CurrentSpeed => _currentSpeed;
    public float RunSpeed => _runSpeed;


    public WalkState WalkState = new WalkState();
    public IdleState IdleState = new IdleState();
    public RunState RunState = new RunState();


    private Vector3 _moveVector;
    private Vector2 _inputVector;

    private PlayerBaseState _currentState;
    private float _currentSpeed = 0;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {

        _input.CharacterControls.Shift.performed += Run;
        _input.CharacterControls.Shift.canceled += StopRun;
        _input.Enable();
    }

    private void Run(InputAction.CallbackContext context)
    {
        _currentSpeed = _runSpeed;

        if(_moveVector.magnitude != 0)
        {
            _currentState = RunState;
            _currentState.EnterState(this);
        }
    }

    private void StopRun(InputAction.CallbackContext context)
    {
        _currentSpeed = _speed;
    }

    private void OnDisable()
    {
        _input.CharacterControls.Shift.performed -= Run;
        _input.CharacterControls.Shift.canceled -= StopRun;
        _input.Disable();
    }

    private void Start()
    {
        _currentSpeed = _speed;
        _currentState = IdleState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
        //Move();
        //Rotate();
        Gravity();
    }

    public void SwitchState(PlayerBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }

    private void OnMovement(InputValue value)
    {
        _inputVector = value.Get<Vector2>();

        _moveVector.x = _inputVector.x;
        _moveVector.z = _inputVector.y;
    }

    public void PlayAnimation(string name)
    {
        _animator.Play(name);
    }

    public void Move()
    {
        _controller.Move(_moveVector * Time.deltaTime * _currentSpeed);
        Rotate();
    }

    private void Gravity()
    {
        _controller.Move(_gravityVector * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_moveVector.magnitude == 0) return;

        Quaternion rotation = Quaternion.LookRotation(_moveVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed);
    }
}