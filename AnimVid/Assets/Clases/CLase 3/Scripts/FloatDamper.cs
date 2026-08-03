using System;
using UnityEngine;

[Serializable]
public struct FloatDamper
{
    [SerializeField] private float _SmoothTime;

    private float currentVelocity;
    public float targetValue{get; set;}
    public float currentValue{get; private set;}
    public void Update()
    {
        currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref currentVelocity, _SmoothTime);
    }

}