using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTest : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        anim.SetTrigger("Mov");
    }
}
