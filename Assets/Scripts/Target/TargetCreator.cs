using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Target
{
    public class TargetCreator : MonoBehaviour
    {
        [SerializeField] private BuildingBlock _buildingBlock;
        [SerializeField] private Material _blockMaterial;
        [SerializeField, Range(1f, 10f)] private float _blockSize = 1f;
        private List<PointsInfo> _pointsInfo;
        
        private Transform _parentTransform;
        private IBuildingBlock[,] _blockArray;
        private bool _isPrepare;
        private float _halfBlockSize;
        private int _root;

        public void Prepare(List<PointsInfo> pointsInfo)
        {
            if (pointsInfo == null)
            {
                Debug.LogError("Points information missing.");
                return;
            }
            
            _pointsInfo = pointsInfo;
            _halfBlockSize = _blockSize / 2f;
            _root = 2 * (_pointsInfo.Count - 1) + 1;
            _isPrepare = true;
        }

        public void CreateTarget(Vector3 position)
        {
            if (!_isPrepare) return;
            
            var parent = new GameObject("Target");
            _parentTransform = parent.transform;
            _parentTransform.position = position;
            
            CreateSquareTarget();
            PaintBlocks();
        }

        public void SetTargetPosition(Vector3 position)
        {
            if (!_isPrepare || _blockArray == null)
                return;
            
            _parentTransform.position = position;
        }

        private void CreateSquareTarget()
        {
            var axleCenter = _root * _halfBlockSize / 2f;
            var centralPoint = Vector2.one * axleCenter;
            _blockArray = new IBuildingBlock[_root, _root];
        
            for (int i = 0; i < _root; i++)
            {
                for (int j = 0; j < _root; j++)
                {
                    var block = Instantiate(_buildingBlock, _parentTransform);
                    _blockArray[j, i] = block;
                    
                    block.BlockMaterial = new Material(_blockMaterial);
                    block.Size = Vector3.one * _blockSize;
                    block.LocalPosition = new Vector3
                    { 
                        x = j * _blockSize - centralPoint.x, 
                        y = i * _blockSize - centralPoint.y, 
                        z = 0f
                    };

                    block.Init();
                }
            }
        }

        private void FillBlockInfo(int x, int y, int infoIndex)
        {
            var block = _blockArray[x, y];
            var info = _pointsInfo[infoIndex];
            block.SetColor = info.GetColor;
            block.Points = info.GetPoints;
        }
        
        private void PaintBlocks()
        {
            var pi = _pointsInfo.Count - 1;
            var infoArray = new PointsInfo[_root, pi];

            //Color the lower part of the target
            var t = 0;
            for (int i = 0; i < pi; i++)
            {
                for (int j = 0; j < t; j++)
                {
                    FillBlockInfo(j, i, j);
                    infoArray[j, i] = _pointsInfo[j];
                }
                
                for (int j = t; j < _root - t; j++)
                {
                    FillBlockInfo(j, i, t);
                    infoArray[j, i] = _pointsInfo[t];
                }

                for (int j = _root - t; j < _root; j++)
                {
                    var index = _root - j - 1;
                    FillBlockInfo(j, i, index);
                    infoArray[j, i] = _pointsInfo[index];
                }

                ++t;
            }

            //Paint the middle strip
            var k = pi;
            for (int j = 0; j < _root; j++)
            {
                var index = j < _pointsInfo.Count ? j : --k;
                FillBlockInfo(j, pi, index);
            }

            //Color the upper part of the target
            var y = 0;
            for (int i = _root - 1; i > pi; i--)
            {
                var x = 0;
                for (int j = 0; j < _root; j++)
                {
                    var block = _blockArray[j, i];
                    var info = infoArray[x++, y];
                    block.SetColor = info.GetColor;
                    block.Points = info.GetPoints;
                }
                
                ++y;
            }
        }
    }
}
