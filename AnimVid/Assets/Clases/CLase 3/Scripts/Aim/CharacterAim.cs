using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.Animations;


public class CharacterAim : MonoBehaviour,ICharacterComponent
{
    [SerializeField] private CinemachineCamera aimCamera;  
    [SerializeField] private FloatDamper aimDamper;
    [SerializeField] private AimConstraint aimConstraint;
    [SerializeField] private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    
    
    public Character ParentCharacter { get; set; }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if(!ctx.started && !ctx.canceled)
        {
            return;
        }

        aimCamera?.gameObject.SetActive(ctx.started);
        ParentCharacter.IsAiming = ctx.started;
        aimDamper.targetValue = ctx.started ? 1 : 0;


    }

    private void LateUpdate()
    {
        aimDamper.Update();
        anim.SetLayerWeight(1, aimDamper.targetValue);
    }
    
}

