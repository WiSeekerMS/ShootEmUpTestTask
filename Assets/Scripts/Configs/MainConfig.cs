using UI;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/MainConfig")]
    public class MainConfig : ScriptableObject
    {
        [SerializeField] private PointsInfo _pointsInfoPrefab;

        public PointsInfo PointsInfoPrefab => _pointsInfoPrefab;
    }
}