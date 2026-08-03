using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
    public Character ParentCharacter { get; set; }

    [SerializeField] private FloatDamper speedX;
    [SerializeField] private FloatDamper speedY;

    private Animator _animator;
    private int _speedXHash;
    private int _speedYHash;

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

    private void Update()
    {
       speedX.Update();
       speedY.Update();

       _animator.SetFloat(_speedXHash, speedX.currentValue);
        _animator.SetFloat(_speedYHash, speedY.currentValue);
    }
}
