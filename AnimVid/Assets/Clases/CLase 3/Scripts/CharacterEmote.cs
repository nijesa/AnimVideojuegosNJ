using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterEmote : MonoBehaviour, ICharacterComponent
{
    public Character ParentCharacter { get; set; }
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnEmote()
    {
        if(!ParentCharacter.IsCrouching || !ParentCharacter.IsReloading || !ParentCharacter.IsWeaponEquipped || !ParentCharacter.IsAiming)
        {
            anim.SetTrigger("Emote");
        }
    }
}
