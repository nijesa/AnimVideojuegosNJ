using UnityEngine;
using UnityEngine.InputSystem;

public class ShowHideGun : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private GameObject gun;
    public Character ParentCharacter { get; set; }

    public void onShowHideGun()
    {
        if(ParentCharacter.IsWeaponEquipped)
        {
            gun.SetActive(false);
            ParentCharacter.IsWeaponEquipped = false;
        }
        else
        {
            gun.SetActive(true);
            ParentCharacter.IsWeaponEquipped = true;
        }
    }
}
