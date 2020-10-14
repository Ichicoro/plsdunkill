using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;
// using UnityEngine.InputSystem;

namespace UnityEngine.InputSystem.OnScreen {

    [AddComponentMenu("Input/On-Screen Trackpad")]
    public class OnScreenTrackpad : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler {
        private Vector2 m_LastPointerDownPos;

        public void OnPointerUp(PointerEventData data) {
            SendValueToControl(0.0f);
        }

        public void OnPointerDown(PointerEventData data) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), data.position, data.pressEventCamera, out m_LastPointerDownPos);
            // SendValueToControl(1.0f);
        }

        public void OnDrag(PointerEventData eventData) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);
            Vector2 delta = position - m_LastPointerDownPos;
            Debug.Log(delta);
            SendValueToControl(delta);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(), eventData.position, eventData.pressEventCamera, out m_LastPointerDownPos);
        }

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal { get => m_ControlPath; set => m_ControlPath = value; }
    }

}