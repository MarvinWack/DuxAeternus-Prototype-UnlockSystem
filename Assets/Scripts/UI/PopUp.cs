using Entities.Buildings;
using UnityEngine;

namespace UI
{
    public abstract class PopUp : MonoBehaviour
    {
        [SerializeField] protected Blocker blocker;

        private void Awake()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            blocker.gameObject.SetActive(false);
            Hide();
        }

        public virtual void Show(int index, Vector3 position, IPopUpCaller caller = null)
        {
            blocker.gameObject.SetActive(true);
            gameObject.SetActive(true);
            transform.position = position;
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}