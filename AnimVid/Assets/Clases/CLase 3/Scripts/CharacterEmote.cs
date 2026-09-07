using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterEmote : MonoBehaviour, ICharacterComponent
{
    public Character ParentCharacter { get; set; }
    private Animator anim;
    private bool reloadStarted;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnEmote()
    {
        if(!ParentCharacter.IsCrouching || !ParentCharacter.IsReloading || !ParentCharacter.IsWeaponEquipped || !ParentCharacter.IsAiming)
        {
            if(anim) anim.SetTrigger("Emote");
        }
    }
    public void onReload()
    {
        ParentCharacter.IsReloading = true;
        if(anim) anim.SetTrigger("Reload");
        
    }

    void Update()
    {
        if(!ParentCharacter.IsReloading) return;
        else
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

            if (state.IsName("Reload") )
            {
                reloadStarted = true;
                if(state.normalizedTime >= 1f)
                {
                    FinishReload();
                }
                
            }
            else if (reloadStarted)
            {
                FinishReload();
            }
        }
    }

    void FinishReload()
    {
        ParentCharacter.IsReloading = false;
        reloadStarted = false;
    }
}
