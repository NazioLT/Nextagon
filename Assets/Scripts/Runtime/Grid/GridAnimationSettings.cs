using UnityEngine;

[System.Serializable]
public class GridAnimationSettings
{
    [field: SerializeField] public float timeBetweenFallingAnim { private set; get; } = 0.25f;
    [field: SerializeField] public float fallingDuration { private set; get; } = 0.5f;
    [field: SerializeField] public float fallingSpacementFactor { private set; get; } = 100f;
    [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private RectTransform spawnPoint;

    public float SpawnHeight => spawnPoint ? spawnPoint.position.y : 500f;
}
