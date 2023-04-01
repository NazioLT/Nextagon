using UnityEngine;
using Nazio_LT.Tools.NTween;

[System.Serializable]
public class GridAnimationSettings
{
    [field: SerializeField] public float timeBetweenFallingAnim { private set; get; } = 0.25f;
    [SerializeField] private float fallingDuration  = 0.5f;
    [SerializeField] private float fallingSpacementFactor = 10f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AnimationCurve scaleFactorAnimationCurve = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField] private RectTransform spawnPoint;

    public NTweener MoveToAnim(RectTransform _rect, Vector2 _origin, Vector2 _target)
    {
        float _animationTime = fallingDuration;//V = d / t => T = D / V

        return NTweening.NTBuild((_t) =>
        {
            _rect.anchoredPosition = Vector2.Lerp(_origin, _target, _t);
            _rect.localScale = scaleFactorAnimationCurve.Evaluate(_t) * Vector3.one;
        }, _animationTime).AddTimeCurve(animationCurve).StartTween();
    }

    public float SpawnHeight => spawnPoint ? spawnPoint.position.y : 500f;
    public float FallingSpacementFactor => fallingSpacementFactor;
}