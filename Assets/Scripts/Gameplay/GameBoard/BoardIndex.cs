using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.GameBoard
{
    public class BoardIndex : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Color color;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Text debugText;

        public Action<BoardIndex> onClick;

        public Figure Figure { get; private set; }

        public void SetColor(Color newColor)
        {
            color = newColor;
            image.color = color;
        }

        public void SetFigure(Figure newFigure)
        {
            Figure = newFigure;
            Figure.transform.SetParent(transform);
            Figure.transform.localPosition = Vector3.zero;
        }
        
        public void RemoveFigure()
        {
            Figure = null;
        }
        
        public void SetSize(Vector2 size)
        {
            rectTransform.sizeDelta = size;
            image.rectTransform.sizeDelta = size;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this);
        }
        
        public void SetText(string text)
        {
            debugText.text = text;
        }
    }
}