using UnityEngine;

public class EnemyHitAnim : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayHitAnim()
    {
        anim.SetTrigger("Hit");
    }
}
