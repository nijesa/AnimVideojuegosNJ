using UnityEngine;
using UnityEngine.InputSystem;

public class ShowHideGun : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private GameObject gun;
    private Animator anim;
    public Character ParentCharacter { get; set; }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void onShowHideGun()
    {
        if(ParentCharacter.IsWeaponEquipped)
        {

            ParentCharacter.IsWeaponEquipped = false;
            if(anim)anim.SetTrigger("Equip");
        }
        else
        {

            ParentCharacter.IsWeaponEquipped = true;
            if(anim)anim.SetTrigger("Equip");
        }
    }

    public void animShowHide()
    {
        if (ParentCharacter.IsWeaponEquipped)
        {
            gun.SetActive(true);
        }
        else
        {
            gun.SetActive(false);
        }
    }
}
