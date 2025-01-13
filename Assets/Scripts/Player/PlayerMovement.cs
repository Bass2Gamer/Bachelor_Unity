using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Camera _playerCamera;

    [Header("Base Movement")]
    public float runAcceleration = 50f;
    public float runSpeed = 4f;
    public float drag = 20f;

    [Header("Camera Settings")]
    public float lookSenseH = 0.1f;
    public float lookSenseV = 0.1f;
    public float lookLimitV = 89f;

    private PlayerInput _playerLocomotionInput;
    private Vector2 _cameraRotation = Vector2.zero;
    private Vector2 _playerTargetRotation = Vector2.zero;

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
        Vector3 newVelocity = _rigidbody.linearVelocity + movementDelta;

        // Add drag to player
        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);

        // Apply the new velocity to the Rigidbody
        _rigidbody.linearVelocity = newVelocity;
    }

    private void LateUpdate()
    {
        _cameraRotation.x += lookSenseH * _playerLocomotionInput.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenseV * _playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);

        _playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * _playerLocomotionInput.LookInput.x;
        transform.rotation = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);

        _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
    }
}