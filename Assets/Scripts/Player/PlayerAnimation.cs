using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerMovement _playerMovement;
    private Rigidbody _rb;

    private static int inputXHash = Animator.StringToHash("inputX");
    private static int inputYHash = Animator.StringToHash("inputY");
    private static int isJumpingHash = Animator.StringToHash("isJumping");
    private static int isFallingHash = Animator.StringToHash("isFalling");
    private static int isGroundedHash = Animator.StringToHash("isGrounded");

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        _animator.SetFloat(inputXHash, inputX);
        _animator.SetFloat(inputYHash, inputY);

        bool isGrounded = _playerMovement.grounded;
        bool isJumping = !isGrounded && _rb.linearVelocity.y > 0.1f;
        bool isFalling = !isGrounded && _rb.linearVelocity.y < -0.1f;

        _animator.SetBool(isGroundedHash, isGrounded);
        _animator.SetBool(isJumpingHash, isJumping);
        _animator.SetBool(isFallingHash, isFalling);
    }
}