using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private float _pointsToComplete;
        [SerializeField] private float _distanceToTarget;

        public int LevelIndex => _levelIndex;
        public float PointsToComplete => _pointsToComplete;
        public float DistanceToTarget => _distanceToTarget;
    }
}