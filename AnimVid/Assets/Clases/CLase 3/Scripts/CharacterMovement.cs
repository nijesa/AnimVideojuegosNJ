using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour, ICharacterComponent
{
     [SerializeField] private FloatDamper speedX;
        [SerializeField] private FloatDamper speedY;
        [SerializeField] private Camera camera;
        [SerializeField] private float angularSpeed;
        private Quaternion targetRotation;
        
        private int _speedXHash;
        private int _speedYHash;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _speedXHash = Animator.StringToHash("SpeedX");
            _speedYHash = Animator.StringToHash("SpeedY");
        }

        private void SolveCharacterRotation()
        {
            Vector3 floorNormal = transform.up;
            Vector3 cameraRealForward = camera.transform.forward;
            float angleInterpolator = Mathf.Abs(Vector3.Dot(cameraRealForward, floorNormal));
            Vector3 cameraForward = Vector3.Lerp(cameraRealForward, camera.transform.up, angleInterpolator).normalized;
            Vector3 characterForward = Vector3.ProjectOnPlane(cameraForward,floorNormal).normalized;
            Debug.DrawLine(transform.position, transform.position + characterForward*3, Color.green,5);
            targetRotation = Quaternion.LookRotation(characterForward,floorNormal);
        }

        [SerializeField] private float crouchSpeedMultiplier = 0.5f;
        private Vector2 _rawInput;

        public void OnMove(InputAction.CallbackContext ctx)
        {
            _rawInput = ctx.ReadValue<Vector2>();
        }
        private void Update()
        {
            float multiplier = (ParentCharacter != null && ParentCharacter.IsCrouching) ? crouchSpeedMultiplier : 1f;
            speedX.targetValue = _rawInput.x * multiplier;
            speedY.targetValue = _rawInput.y * multiplier;

            speedX.Update();
            speedY.Update();
            _animator.SetFloat(_speedXHash,speedX.currentValue);
            _animator.SetFloat(_speedYHash,speedY.currentValue);
            SolveCharacterRotation();
            if (ParentCharacter != null && ParentCharacter.IsAiming)
            {
                // Al apuntar, alinear suavemente el cuerpo con la cámara para que la cadera no se deforme
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * 8f * Time.deltaTime);
            }
            else
            {
                ApplyCharacterRotation();
            }
        }

        private void ApplyCharacterRotation()
        {
            float motionMagnitud = Mathf.Sqrt(speedX.targetValue * speedX.targetValue + speedY.targetValue * speedY.targetValue);
            float rotationSpeed = Mathf.SmoothStep(0, .01f, motionMagnitud);

            // Si está quieto pero el ángulo con la cámara supera los 70 grados, girar el cuerpo para no torcer la cintura
            float angleToCamera = Quaternion.Angle(transform.rotation, targetRotation);
            if (motionMagnitud < 0.01f && angleToCamera > 70f)
            {
                rotationSpeed = Mathf.Clamp01((angleToCamera - 70f) / 30f);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * rotationSpeed);
        }

        public Character ParentCharacter { get; set; }
}
