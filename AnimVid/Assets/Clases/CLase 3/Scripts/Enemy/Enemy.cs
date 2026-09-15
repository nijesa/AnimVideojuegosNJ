using UnityEngine;

[DefaultExecutionOrder(-1)]
public class Enemy : MonoBehaviour
{
    private bool isDead;
        public bool IsDead
        {
            get => isDead;
            set => isDead = value;
        }
        
}
