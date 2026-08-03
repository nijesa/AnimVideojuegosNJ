using UnityEngine;


public class PlayerAnimationStateController : MonoBehaviour
{
    Animator animator;
    float Velocity_X=0f;
    public float acceleration=0.1f;
    public float deceleration=0.5f;
    int VelocityHash_X;

    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityHash_X = Animator.StringToHash("Velocity");

    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool backPress = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
       
        
            #region Forward

                if (forwardPressed && Velocity_X < 1.0f)
                {
                    Velocity_X += Time.deltaTime * acceleration;
                }
            #endregion

            #region Backward
                if (backPress && Velocity_X > -1.0f)
                {
                    Velocity_X -= Time.deltaTime * acceleration;
                }
            #endregion

            if (!forwardPressed && !backPress && Velocity_X != 0f)
                {
                    backToStart();
                }


        animator.SetFloat(VelocityHash_X, Velocity_X);
    }


    void noButtonPressed()
    {
        Velocity_X = 0.0f;

    }

    void backToStart()
    {
        float goingBack = Mathf.MoveTowards(Velocity_X, 0.0f, Time.deltaTime * deceleration);
        Velocity_X = goingBack;
    }

}
