using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerAnimation playerAnimation;
    private bool weaponDrawn = false;
    private float timepassed;
    private float clipLength;
    private float clipSpeed;
    private bool attack;
    private bool queuedAttack;

    void Awake()
    {
        controls = new PlayerControls();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void OnEnable()
    {
        controls.PlayerInput.DrawWeapon.performed += OnDrawWeapon;
        controls.PlayerInput.DrawWeapon.Enable();
        controls.PlayerInput.Attack.performed += OnAttack;
        controls.PlayerInput.Attack.Enable();
    }

    void OnDisable()
    {
        controls.PlayerInput.DrawWeapon.performed -= OnDrawWeapon;
        controls.PlayerInput.DrawWeapon.Disable();
        controls.PlayerInput.Attack.performed -= OnAttack;
        controls.PlayerInput.Attack.Disable();
    }

    void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (!weaponDrawn)
        {
            playerAnimation.TriggerDrawWeapon();
            weaponDrawn = true;
        }
        else
        {
            playerAnimation.TriggerSheathWeapon();
            weaponDrawn = false;
        }
    }

    void OnAttack(InputAction.CallbackContext context)
    {
        if (!weaponDrawn) return;

        if (!attack)
        {
            attack = true;
            timepassed = 0f;
            playerAnimation.TriggerAttack();
            playerAnimation.Animator.SetFloat("speed", 0f);
            playerAnimation.Animator.applyRootMotion = true;
            queuedAttack = false;
        }
        else
        {
            queuedAttack = true;
        }
    }

    void Update()
    {
        if (!attack) return;

        timepassed += Time.deltaTime;
        var animator = playerAnimation.Animator;
        if (animator.GetCurrentAnimatorClipInfoCount(1) > 0)
        {
            clipLength = animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            clipSpeed = animator.GetCurrentAnimatorStateInfo(1).speed;
            if (timepassed >= clipLength / clipSpeed)
            {
                if (queuedAttack)
                {
                    attack = true;
                    timepassed = 0f;
                    playerAnimation.TriggerAttack();
                    playerAnimation.Animator.SetFloat("speed", 0f);
                    playerAnimation.Animator.applyRootMotion = true;
                    queuedAttack = false;
                }
                else
                {
                    attack = false;
                    animator.applyRootMotion = false;
                    playerAnimation.TriggerMove();
                }
            }
        }
    }
}