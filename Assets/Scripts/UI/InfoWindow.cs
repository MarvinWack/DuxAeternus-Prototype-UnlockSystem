using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InfoWindow : PopUp
    {
        [SerializeField] private TMP_Text infoText;

        private bool _isFadingIn;
        
        public override async void Show(int index, Vector3 position, IPopUpCaller caller)
        {
            if (_isFadingIn)
                return;
            
            _isFadingIn = true;
            
            await Task.Delay(1800);

            _isFadingIn = false;
            
            base.Show(index, position, caller);
            
            if(caller is InfoWindowCaller infoWindowCaller)
            {
                infoText.text = infoWindowCaller.GetInfo();
            }
            else
            {
                Debug.LogError("Caller is not an InfoWindow caller");
            }
        }
        
        public override async void Hide()
        {
            while (_isFadingIn)
            {
                await Task.Yield();
            }
            base.Hide();
            infoText.text = string.Empty;
        }
    }
}