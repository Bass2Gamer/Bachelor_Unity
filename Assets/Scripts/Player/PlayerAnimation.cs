using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody rb;

    private static readonly int isMovingHash = Animator.StringToHash("isMoving");
    private static readonly int drawWeaponHash = Animator.StringToHash("drawWeapon");
    private static readonly int sheathWeaponHash = Animator.StringToHash("sheathWeapon");
    private static readonly int speedHash = Animator.StringToHash("speed");
    private static readonly int attackHash = Animator.StringToHash("attack");
    private static readonly int moveHash = Animator.StringToHash("move");

    public Animator Animator => animator;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void Update() => UpdateAnimationState();

    private void UpdateAnimationState()
    {
        float speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        animator.SetFloat(speedHash, speed);
        animator.SetBool(isMovingHash, speed > 0.1f);
    }

    public void TriggerDrawWeapon() => animator.SetTrigger(drawWeaponHash);
    public void TriggerSheathWeapon() => animator.SetTrigger(sheathWeaponHash);
    public void TriggerAttack() => animator.SetTrigger(attackHash);
    public void TriggerMove() => animator.SetTrigger(moveHash);
}