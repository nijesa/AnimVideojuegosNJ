using UnityEngine;

public class HitReceiver : MonoBehaviour, IHitable
{

    [SerializeField] private Animator animator;
    [SerializeField] private string hitTrigger = "Hit";
    [SerializeField] private AliveCharacter ac;
    public void ApplyHit(HitInfo hit)
    {
        Debug.Log("ApplyHit");
        if(animator)
        {
            
            Debug.Log("ApplyHit animator");
            ac.TakeDamage(hit.damage);
        }
    }
}