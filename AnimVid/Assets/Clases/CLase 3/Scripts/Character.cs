using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character:MonoBehaviour
{
    private bool isCrouching;
        public bool IsCrouching
        {
            get => isCrouching;
            set => isCrouching = value;
        }
        private bool isReloading;
        public bool IsReloading
        {
            get => isReloading;
            set => isReloading = value;
        }
        private bool isWeaponEquipped = true;
        public bool IsWeaponEquipped
        {
            get => isWeaponEquipped;
            set => isWeaponEquipped = value;
        }
        private Transform lockTarget;
        private bool isAiming;
        public bool IsAiming
        {
            get => isAiming;
            set => isAiming = value;
        }
        

        public Transform LockTarget
        {
            get =>lockTarget;
            set =>lockTarget = value;
        }
        
        private void Awake()
        {
            RegisterComponents();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void RegisterComponents()
        {
            foreach (ICharacterComponent component in GetComponentsInChildren<ICharacterComponent>())
            {
                component.ParentCharacter = this;
            }
        }
}
