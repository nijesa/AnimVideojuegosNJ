using UnityEngine;

public class AliveCharacter : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private GameObject perso;
    private Animator animator;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        
    }
    public void HideCharacter()
    {
        perso.SetActive(false);
    }

}
