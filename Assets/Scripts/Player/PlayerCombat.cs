using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControls _controls;
    private PlayerAnimation _playerAnimation;
    private bool weaponDrawn = false;
    private float timepassed;
    private float clipLength;
    private float clipSpeed;
    private bool attack;
    private bool queuedAttack;

    void Awake()
    {
        _controls = new PlayerControls();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    void OnEnable()
    {
        _controls.PlayerInput.DrawWeapon.performed += OnDrawWeapon;
        _controls.PlayerInput.DrawWeapon.Enable();
        _controls.PlayerInput.Attack.performed += OnAttack;
        _controls.PlayerInput.Attack.Enable();
    }

    void OnDisable()
    {
        _controls.PlayerInput.DrawWeapon.performed -= OnDrawWeapon;
        _controls.PlayerInput.DrawWeapon.Disable();
        _controls.PlayerInput.Attack.performed -= OnAttack;
        _controls.PlayerInput.Attack.Disable();
    }

    void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (!weaponDrawn)
        {
            _playerAnimation.TriggerDrawWeapon();
            weaponDrawn = true;
        }
        else
        {
            _playerAnimation.TriggerSheathWeapon();
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
            _playerAnimation.TriggerAttack();
            _playerAnimation.Animator.SetFloat("speed", 0f);
            _playerAnimation.Animator.applyRootMotion = true;
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
        var animator = _playerAnimation.Animator;
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
                    _playerAnimation.TriggerAttack();
                    _playerAnimation.Animator.SetFloat("speed", 0f);
                    _playerAnimation.Animator.applyRootMotion = true;
                    queuedAttack = false;
                }
                else
                {
                    attack = false;
                    animator.applyRootMotion = false;
                    _playerAnimation.TriggerMove();
                }
            }
        }
    }
}