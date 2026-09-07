using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLook : MonoBehaviour, ICharacterComponent
{
    [SerializeField] private Transform target;
    [SerializeField] private FloatDamper horizontalDamper;
    [SerializeField] private FloatDamper verticalDamper;

    [SerializeField] private float horiontalRotationSpeed;
    [SerializeField] private float verticalRotationSpeed;
    [SerializeField] private Vector2 verticalRotationLimits;
    [SerializeField] private Vector2 horizontalRotationLimits;

    [SerializeField] private float verticalRotation;

    public Character ParentCharacter { get; set; }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();
        inputValue = inputValue/ new Vector2(Screen.width, Screen.height);

        horizontalDamper.targetValue = inputValue.x;
        verticalDamper.targetValue = inputValue.y;
    }

    private void ApplyLookRotation()
    {
        if (target == null)
        {
            throw new NullReferenceException("Look target null");
        }

        if(ParentCharacter.LockTarget != null)
        {
            Vector3 lookDirection = (ParentCharacter.LockTarget.position - target.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            target.rotation = rotation;
            return;
        }

        target.RotateAround(target.position, transform.up, horizontalDamper.currentValue*horiontalRotationSpeed * 360 * Time.deltaTime);
        verticalRotation += verticalDamper.currentValue * verticalRotationSpeed * 360 * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, verticalRotationLimits.x, verticalRotationLimits.y);

        Vector3 euler = target.localEulerAngles;
        euler.x = verticalRotation;

        target.localEulerAngles = euler;

    }

    void Update()
    {
        horizontalDamper.Update();
        verticalDamper.Update();
        ApplyLookRotation();
    }
}
