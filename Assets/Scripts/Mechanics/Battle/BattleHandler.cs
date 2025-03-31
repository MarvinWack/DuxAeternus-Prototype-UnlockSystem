using System;
using System.Collections.Generic;
using System.Linq;
using Objects.TroopTypes;
using Unity.Mathematics;
using UnityEngine;

namespace Mechanics.Battle
{
    public class BattleHandler : MonoBehaviour
    {
        [SerializeField] private TroopTypeCreator _creatorPlayerA;
        [SerializeField] private TroopTypeCreator _creatorPlayerB;
        [SerializeField] private bool _isFighting = false;
        private void Awake()
        {
            Updater.Instance.BattleTick += BattleTickHandler;
        }

        private void BattleTickHandler()
        {
            if (!_isFighting || !CheckIfUnitsAvailableToFight())
                return;
            
            HandleRangedPhase();
            if (!CheckIfUnitsAvailableToFight())
                return;
            
            HandleMeleePhase();
        }

        private bool CheckIfUnitsAvailableToFight()
        {
            if(_creatorPlayerA.CheckIfUnitsAvailableToFight() && 
               _creatorPlayerB.CheckIfUnitsAvailableToFight())
                return true;

            _isFighting = false;
            return false;
        }

        private void HandleRangedPhase()
        {
            
            Dictionary<TroopType, float> typeSharesA = CalculateTroopTypeShare(_creatorPlayerA.TroopTypes);
            Dictionary<TroopType, float> typeSharesB = CalculateTroopTypeShare(_creatorPlayerB.TroopTypes);
            
            int damageA = CalculateRangedDamage(_creatorPlayerA.TroopTypes);
            int damageB = CalculateRangedDamage(_creatorPlayerB.TroopTypes);
            
            var lossesPerTypeA = CalculateLossesPerType(typeSharesA, damageB);
            var lossesPerTypeB = CalculateLossesPerType(typeSharesB, damageA);
            
            _creatorPlayerA.ApplyTroopLosses(lossesPerTypeA);
            _creatorPlayerB.ApplyTroopLosses(lossesPerTypeB);
        }

        private void HandleMeleePhase()
        {
            Dictionary<TroopType, float> typeSharesA = CalculateTroopTypeShare(_creatorPlayerA.TroopTypes);
            Dictionary<TroopType, float> typeSharesB = CalculateTroopTypeShare(_creatorPlayerB.TroopTypes);
            
            int damageA = CalculateMeleeDamage(_creatorPlayerA.TroopTypes);
            int damageB = CalculateMeleeDamage(_creatorPlayerB.TroopTypes);
            
            var lossesPerTypeA = CalculateLossesPerType(typeSharesA, damageB);
            var lossesPerTypeB = CalculateLossesPerType(typeSharesB, damageA);
            
            _creatorPlayerA.ApplyTroopLosses(lossesPerTypeA);
            _creatorPlayerB.ApplyTroopLosses(lossesPerTypeB);
        }

        private Dictionary<TroopType, float> CalculateTroopTypeShare(List<TroopType> troopTypes)
        {
            int totalAmount = troopTypes.Sum(type => type.TotalAmount);

            return troopTypes.ToDictionary(type => type, type => (float)type.TotalAmount / totalAmount);
        }

        private int CalculateRangedDamage(List<TroopType> troopTypes)
        {
            return troopTypes.Sum(type => type.Weapon.RangedDamage * type.TotalAmount);
        }
        
        private int CalculateMeleeDamage(List<TroopType> troopTypes)
        {
            return troopTypes.Sum(type => type.Weapon.MeleeDamage * type.TotalAmount);
        }
        
        private Dictionary<TroopType, int> CalculateLossesPerType(Dictionary<TroopType, float> typeShares, int totalDamage)
        {
            Dictionary<TroopType, int> lossesPerType = new();
        
            foreach (var type in typeShares)
            {
                lossesPerType.Add(type.Key, Mathf.RoundToInt(typeShares[type.Key] * totalDamage / type.Key.Armour.Defense));
            }

            return lossesPerType;
        }
    }
}