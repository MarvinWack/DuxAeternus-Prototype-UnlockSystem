using System;
using System.Collections.Generic;
using Production.Items;
using Production.Storage;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private Texture2D _resourceIcon;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private ResourceStorage _resourceStorage;
        [SerializeField] private ResourceType _resourceType;

        private void Awake()
        {
            _resourceStorage.OnResourceAmountChanged += UpdateUI;
        }
        
        private void UpdateUI(Dictionary<ResourceType, int> resources)
        {
            _amountText.text = resources[_resourceType].ToString();
        }
    }
}