using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    
    protected Animator baseAnimator;
    protected Animator weaponAnimator;
    
    protected PlayerAttackState state;
    
    protected int attackCounter;

    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (attackCounter >= weaponData.amountOfAttacks) attackCounter = 0;
        
        baseAnimator.SetBool("attack", true);
        weaponAnimator.SetBool("attack", true);
        
        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter);
    }
    
    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("attack", false);
        weaponAnimator.SetBool("attack", false);

        attackCounter++;
        
        gameObject.SetActive(false);
    }
    
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }
    
    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }
    
    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetShouldCheckFlip(false);
    }
    
    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetShouldCheckFlip(true);
    }
    
    public virtual void AnimationActionTrigger()
    {
        
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}
