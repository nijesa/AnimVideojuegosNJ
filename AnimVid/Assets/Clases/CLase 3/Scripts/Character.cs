using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Character:MonoBehaviour
{
    private void Awake()
    {
        RegisterComponents();
    }

    private void RegisterComponents()
    {
        foreach (ICharacterComponent characterComponent in GetComponentsInChildren<ICharacterComponent>())
        {
            characterComponent.ParentCharacter = this;
        }
    }
}
