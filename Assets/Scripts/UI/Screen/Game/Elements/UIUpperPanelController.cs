using System;
using Core.Base;
using Core.Data;
using Gameplay.Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screen.Game.Elements
{
    public class UIUpperPanelController : MonoBehaviour
    {
        [SerializeField] private Text title;
        [SerializeField] private Text timer;

        public void Init()
        {
            title.text = CoreData.GameSettings.CurrentRuleData.Title;
        }

        private void FixedUpdate()
        {
            TimeSpan time = TimeSpan.FromSeconds(BaseGameController.Instance().GetGameTime());
            timer.text = time.ToString(@"mm\:ss");
        }
    }
}