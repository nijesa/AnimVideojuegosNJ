using UnityEngine;
public interface IHitable
{
    void ApplyHit(HitInfo hit);
}
public struct HitInfo
    {
        public Vector3 point;
        public Vector3 normal;
        public float damage;
    }