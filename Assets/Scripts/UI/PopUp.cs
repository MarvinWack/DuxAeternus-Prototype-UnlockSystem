using UnityEngine;

namespace UI
{
    public abstract class PopUp : MonoBehaviour
    {
        [SerializeField] protected Blocker blocker;

        protected void Awake()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            blocker.gameObject.SetActive(false);
            Hide();
        }

        public virtual void Show(Vector3 position, IPopUpCaller caller = null)
        {
            blocker.gameObject.SetActive(true);
            gameObject.SetActive(true);
            
            var newPosition = KeepPositionInsideScreen(position);
            transform.position = newPosition;
        }

        private Vector3 KeepPositionInsideScreen(Vector3 position)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            
            Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.lossyScale);
            
            position.x = Mathf.Clamp(position.x, size.x / 2, Screen.width - size.x / 2);
            position.y = Mathf.Clamp(position.y, size.y / 2, Screen.height - size.y / 2);

            return position;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}