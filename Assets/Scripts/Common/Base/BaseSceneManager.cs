using System.Collections.Generic;
using Configs;
using UnityEngine;
using Zenject;

namespace Common.Base
{
    public abstract class BaseSceneManager : MonoBehaviour
    {
        protected MainConfig _mainConfig;
        protected List<LevelConfig> _levelConfigs;

        [Inject]
        private void Constructor(MainConfig mainConfig)
        {
            _mainConfig = mainConfig;
            _levelConfigs = _mainConfig.LevelConfigs;
        }
        
        private void StartLevel()
        {
            UnlockPlayerControl();
            OnStartLevel();
        }

        private void GoToNextLevel()
        {
            OnGoToNextLevel();
        }
        
        protected void UnlockPlayerControl()
        {
        }
        
        protected void BlockPlayerControl()
        {
        }

        #region Virtual Methods
        protected virtual void OnStartLevel()
        {
        }

        protected virtual void OnGoToNextLevel()
        {
        }
        
        protected virtual bool CheckLevelComplete()
        {
            return false;
        }
        #endregion
    }
}