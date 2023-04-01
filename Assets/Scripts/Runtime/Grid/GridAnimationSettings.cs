using UnityEngine;

[System.Serializable]
public class GridAnimationSettings
{
    [field: SerializeField] public float timeBetweenFallingAnim { private set; get; } = 0.25f;
    [field: SerializeField] public float fallingSpeed { private set; get; } = 10;
    [SerializeField] private RectTransform spawnPoint;

    public float SpawnHeight => spawnPoint ? spawnPoint.position.y : 500f;
}
