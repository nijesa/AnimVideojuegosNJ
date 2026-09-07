using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
    public Character ParentCharacter { get; set; }

    [SerializeField] private FloatDamper speedX;
    [SerializeField] private FloatDamper speedY;
    [SerializeField] private float angularSpeed;

    [SerializeField] private Camera _camera;
    private Animator _animator;
    private int _speedXHash;
    private int _speedYHash;

    private Quaternion targetRotation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speedXHash = Animator.StringToHash("SpeedX");
        _speedYHash = Animator.StringToHash("SpeedY");
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 inputValue = ctx.ReadValue<Vector2>();

        speedX.targetValue = inputValue.x;
        speedY.targetValue = inputValue.y;

        
    }

    private void SolveCharacterRotation()
    {
        Vector3 floorNormal = transform.up;
        Vector3 cameraRealForward = _camera.transform.forward;

        float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
        Vector3 cameraForward = Vector3.Lerp(cameraRealForward, _camera.transform.up, angleInterpolator).normalized;
        Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward, floorNormal).normalized;

        Debug.DrawLine(transform.position, transform.position + characterForward * 2, Color.magenta,5);

        targetRotation = Quaternion.LookRotation(characterForward, floorNormal);
    }

    private void ApplyCharacterRotation()
    {
        float motionMagnitude = Mathf.Sqrt(speedX.targetValue*speedX.targetValue + speedY.targetValue*speedY.targetValue);
        float rotationSpeed = Mathf.SmoothStep(0,.1f, motionMagnitude);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed );
    

    }

    private void Update()
    {
       speedX.Update();
       speedY.Update();

       _animator.SetFloat(_speedXHash, speedX.currentValue);
        _animator.SetFloat(_speedYHash, speedY.currentValue);

        SolveCharacterRotation();
        if(!ParentCharacter.IsAiming)
        {
            ApplyCharacterRotation();
        }
    }
}
