using System.Collections.Generic;
using System.Linq;
using Core.Base;
using Gameplay.Scriptable;
using UnityEngine;

namespace Core.DataLoader
{
    public class CornersGamemodeLoader : BaseDataLoader
    {
        private const string GAMEMODE_PATH = "Data/Gamemodes";
        
        
        public override DataLoaderResult Load()
        {
            var gameModes = Resources.LoadAll<GameRuleData>(GAMEMODE_PATH);
            
            return new CornersGamemodeLoadResult(gameModes);
        }
    }
}