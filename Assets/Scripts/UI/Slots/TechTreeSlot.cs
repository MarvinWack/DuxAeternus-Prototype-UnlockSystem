using System;
using UI.Slot;
using UnityEngine;

namespace UI
{
    public class TechTreeSlot : MonoBehaviour
    {
        [SerializeField] private ExtendedText currentLevelText;
        [SerializeField] private ExtendedButton button;
        
        public void Setup(Tuple<ExtendedButton, ExtendedText> tuple)
        {
            tuple.Item1.transform.SetParent(transform.GetChild(1), false);
            // tuple.Item1.transform.SetAsFirstSibling();
            
            tuple.Item2.transform.SetParent(transform.GetChild(1), false);
            // tuple.Item2.transform.SetAsFirstSibling();
        }

        public ExtendedButton GetButton()
        {
            return button;
        }
    }
}