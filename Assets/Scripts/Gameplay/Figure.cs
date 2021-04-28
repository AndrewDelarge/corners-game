using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [Serializable]
    public enum FigureType
    {
        Checker,
    }
    
    public class Figure : MonoBehaviour
    {
        [SerializeField] private FigureType type;
        [SerializeField] private GameObject outline;
        
        public FigureType Type => type;
        
        private void Awake()
        {
            SetSelect(false);
        }
        
        public void SetSelect(bool select)
        {
            outline.SetActive(select);
        }
    }
}