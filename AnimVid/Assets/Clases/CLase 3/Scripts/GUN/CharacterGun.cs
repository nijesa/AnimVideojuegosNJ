using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterGun : MonoBehaviour, ICharacterComponent
{
   public Character ParentCharacter { get; set; }

   private bool _isFire;
   private float _nextShootTime;
   [SerializeField] private Camera camera;
   [SerializeField] private bool automatic;
   [SerializeField] private bool requireAim;
   [SerializeField]private float fireRate = 10f;
   [SerializeField] private Animator anim;
   [SerializeField]private float range = 100f;
   [SerializeField]private LayerMask hitMask;
   [SerializeField]private Transform tracerOrigin;
   [SerializeField] private bool debugDraw;
   [SerializeField] private float debugDuration;
   [SerializeField] private RecoilCameraKick recoil;
   
   [SerializeField] private float camKick;
   [SerializeField] private float camShake;
   [SerializeField] private float camRecover;

   [SerializeField] private bool Character_1;
   [SerializeField] private bool Character_2;
   [SerializeField] private bool Character_3;

    private void OnDrawGizmos()
    {
        if (!debugDraw) return;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 from = tracerOrigin ? tracerOrigin.position : ray.origin;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, from + ray.direction * range);
    }


   public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)_isFire = true;

        if (ctx.canceled)_isFire = false;
        
        if(!automatic &&ctx.performed) TryShoot();

    }

    void Update()
    {
        if(automatic && _isFire) TryShoot();
    }

    private void TryShoot()
    {
        if(requireAim && (ParentCharacter == null || !ParentCharacter.IsAiming)) return;

        if(Time.time < _nextShootTime) return;
        _nextShootTime = Time.time + 1f/Mathf.Max(1f,fireRate);

        ShootOnce();
    }

    private void ShootOnce()
        {
            if(ParentCharacter.IsWeaponEquipped == false) return;
            if(ParentCharacter.IsReloading) return;
        if (anim)
        {
            if (Character_1) anim.SetTrigger("Fire");
            if (Character_2) anim.SetTrigger("Fire2");
            if (Character_3) anim.SetTrigger("Fire3");
        } 
            
            if(recoil) recoil.Kick(camShake,camKick,camRecover);
            

            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Vector3 from = tracerOrigin ? tracerOrigin.position : ray.origin;

            if (Physics.Raycast(ray, out var hit, range, hitMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 to = hit.point;

                if (debugDraw)
                {
                    Debug.DrawRay(ray.origin, ray.direction * Vector3.Distance(ray.origin, to));
                    Debug.DrawLine(from, to, Color.red, debugDuration);
                }


                var info = new HitInfo { point = hit.point, normal = hit.normal, damage = 10f };

                if (hit.collider.TryGetComponent<IHitable>(out var h))
                {
                    h.ApplyHit(info);
                }
                else
                {
                    var rb = hit.collider.attachedRigidbody;
                    if (rb && rb.TryGetComponent<IHitable>(out var hrb))
                    {
                        hrb.ApplyHit(info);
                    }
                    else
                    {
                        var hParent = hit.collider.GetComponentInParent<IHitable>();
                        if (hParent != null) hParent.ApplyHit(info);
                    }
                }
            }
            else
            {
                Vector3 to = ray.origin + ray.direction * range;
                if (debugDraw)
                {
                    Debug.DrawRay(ray.origin, ray.direction * range,Color.gray,debugDuration);
                    Debug.DrawLine(from,to,Color.cyan,debugDuration);
                }
            }
        }

}
