using UnityEngine;


public class PlayerAnimationStateController : MonoBehaviour
{
    Animator animator;
    float Velocity=0f;
    public float acceleration=0.1f;
    public float deceleration=0.5f;
    int VelocityHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        if (forwardPressed && Velocity<1f)
        {
            Velocity += Time.deltaTime * acceleration;
        }

        if(!forwardPressed && Velocity > 0f)
        {
            Velocity -= Time.deltaTime * deceleration;
        }

        animator.SetFloat(VelocityHash, Velocity);
    }
}
