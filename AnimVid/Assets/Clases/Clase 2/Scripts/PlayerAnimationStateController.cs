using UnityEngine;


public class PlayerAnimationStateController : MonoBehaviour
{
    Animator animator;
    float Velocity_X=0f;
    float Velocity_Z=0f;
    public float acceleration=0.1f;
    public float deceleration=0.5f;
    int VelocityHash_X;
    int VelocityHash_Z;
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityHash_X = Animator.StringToHash("Velocity_X");
        VelocityHash_Z = Animator.StringToHash("Velocity_Z");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool backwardPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool leftPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        bool anyKeyPressed = Input.anyKey;


        /* #region Forward Movement    
            if (forwardPressed && Velocity_Z < 1.0f)
            {
                Velocity_Z += Time.deltaTime * acceleration;
            }

            if(!forwardPressed && Velocity_Z > 0f)
            {
                Velocity_Z -= Time.deltaTime * deceleration;
            }

            if (!forwardPressed && Velocity_Z < 0f)
            {
                Velocity_Z = 0.0f;
            }
        #endregion*/

        if (anyKeyPressed)
        {
            if(forwardPressed)
            {
                Velocity_Z = velManager(Velocity_Z, true);
            }
            if (backwardPressed)
            {
                Velocity_Z = velManager(Velocity_Z, false);
            }

            if (leftPressed)
            {
                Velocity_X = velManager(Velocity_X, false);
            }
            if (rightPressed)
            {
                Velocity_X = velManager(Velocity_X, true);
            }
        }
            

        animator.SetFloat(VelocityHash_Z, Velocity_Z);
        animator.SetFloat(VelocityHash_X, Velocity_X);
    }


    void noButtonPressed()
    {
        Velocity_X = 0.0f;
        Velocity_Z = 0.0f;
    }

    float velManager(float velocityTuki, bool direction)
    {
        float newVelocity = velocityTuki;
        if (direction)
        {
            if (velocityTuki < 1.0f)
            {
                newVelocity += Time.deltaTime * acceleration;
            } 
        }
        else if (!direction)
        {
            if (velocityTuki > -1.0f)
            {
                newVelocity -= Time.deltaTime * acceleration;

            }
        }
        return newVelocity;
    }

}
