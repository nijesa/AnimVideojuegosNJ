using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character:MonoBehaviour
{
    [SerializeField] private bool isAiming;
    private Transform lockTarget;
    public bool IsAiming
    {
        get=>isAiming;
        set=> isAiming = value;
    }

    public Transform LockTarget
    {
        get => lockTarget;
        set => lockTarget = value;
    }
    private void Awake()
    {
        RegisterComponents();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }
}
