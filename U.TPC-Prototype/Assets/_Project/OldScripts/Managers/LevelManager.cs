/*
* LevelManager - Used to get access to ResourcesManager for gameplay runtime items
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Scriptables.Managers;

namespace ANM.Managers
{
    public static class LevelManager
    {
        private static ResourcesManager _resourcesManager;

        
        public static ResourcesManager GetResourcesManager()
        {
            if (_resourcesManager != null) return _resourcesManager;
            _resourcesManager = Resources.Load<ResourcesManager>("ResourceManager");
            _resourcesManager.Initialize();
            return _resourcesManager;
        }
    }
}
