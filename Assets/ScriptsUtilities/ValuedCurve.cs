using System;
using UnityEngine;

namespace ScriptsUtilities
{
    [Serializable]
    public class ValuedCurve
    {
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private float _verticalAxisMax;
        public float verticalAxisMax => _verticalAxisMax;

        [SerializeField] private float _horizontalAxisMax;
        public float horizontalAxisMax => _horizontalAxisMax;

        public ValuedCurve(AnimationCurve curve, float maxValue, float maxTime)
        {
            _curve = curve;
            _horizontalAxisMax = maxTime;
            _verticalAxisMax = maxValue;
        }

        public float Evaluate(float time)
        {
            time = Mathf.Clamp(time, 0, _horizontalAxisMax);
            return _curve.Evaluate(time / _horizontalAxisMax) * _verticalAxisMax;
        }
    }
}