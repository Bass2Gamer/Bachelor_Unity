using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private PlayerMovement _playerMovement;
    private Rigidbody _rb;

    private static readonly int isMovingHash = Animator.StringToHash("isMoving");
    private static readonly int drawWeaponHash = Animator.StringToHash("drawWeapon");
    private static readonly int sheathWeaponHash = Animator.StringToHash("sheathWeapon");
    private static readonly int speedHash = Animator.StringToHash("speed");
    private static readonly int attackHash = Animator.StringToHash("attack");
    private static readonly int moveHash = Animator.StringToHash("move");

    public Animator Animator => _animator;

    void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update() => UpdateAnimationState();

    private void UpdateAnimationState()
    {
        float speed = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z).magnitude;
        _animator.SetFloat(speedHash, speed);
        _animator.SetBool(isMovingHash, speed > 0.1f);
    }

    public void TriggerDrawWeapon() => _animator.SetTrigger(drawWeaponHash);
    public void TriggerSheathWeapon() => _animator.SetTrigger(sheathWeaponHash);
    public void TriggerAttack() => _animator.SetTrigger(attackHash);
    public void TriggerMove() => _animator.SetTrigger(moveHash);
}