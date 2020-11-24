using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InterfacePanels
{
    public class InterfaceTrigger : EventTrigger
    {
        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
        }

        public override void OnCancel(BaseEventData eventData)
        {
            base.OnCancel(eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            base.OnDrop(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            base.OnInitializePotentialDrag(eventData);
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
        }

        public override void OnScroll(PointerEventData eventData)
        {
            base.OnScroll(eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
        }

        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
