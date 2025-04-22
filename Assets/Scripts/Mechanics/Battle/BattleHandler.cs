using System.Collections.Generic;
using System.Linq;
using Mechanics.Traits;
using Objects.TroopTypes;
using UnityEngine;

namespace Mechanics.Battle
{
    public class BattleHandler : MonoBehaviour
    {
        [SerializeField] private TroopTypeCreator _creatorPlayerA;
        [SerializeField] private TroopTypeCreator _creatorPlayerB;
        [SerializeField] private bool _isFighting = false;

        private int _tickCounter;
        private void Awake()
        {
            Updater.Instance.BattleTick += BattleTickHandler;
        }

        private void BattleTickHandler()
        {
            if (!_isFighting || !CheckIfUnitsAvailableToFight())
                return;

            _tickCounter++;
            
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
            
            var lossesPerTypeA = CalculateRangedDamage(_creatorPlayerB.TroopTypes, typeSharesA);
            var lossesPerTypeB = CalculateRangedDamage(_creatorPlayerA.TroopTypes, typeSharesB);
            
            _creatorPlayerA.ApplyTroopLosses(lossesPerTypeA);
            _creatorPlayerB.ApplyTroopLosses(lossesPerTypeB);
        }

        private void HandleMeleePhase()
        {
            Dictionary<TroopType, float> typeSharesA = CalculateTroopTypeShare(_creatorPlayerA.TroopTypes);
            Dictionary<TroopType, float> typeSharesB = CalculateTroopTypeShare(_creatorPlayerB.TroopTypes);
            
            var lossesPerTypeA = CalculateMeleeDamage(_creatorPlayerB.TroopTypes, typeSharesA);
            var lossesPerTypeB = CalculateMeleeDamage(_creatorPlayerA.TroopTypes, typeSharesB);
            
            _creatorPlayerA.ApplyTroopLosses(lossesPerTypeA);
            _creatorPlayerB.ApplyTroopLosses(lossesPerTypeB);
        }

        private Dictionary<TroopType, int> CalculateRangedDamage(List<TroopType> attTroopTypes, Dictionary<TroopType, float> typeShares)
        {
            Dictionary<TroopType, int> lossesPerType = new();
            
            foreach (var type in attTroopTypes)
            {
                int totalDamage = type.Weapon.RangedDamage * type.TotalAmount;

                foreach (var target in typeShares)
                {
                    int damageShare = Mathf.RoundToInt(
                        totalDamage * target.Value * GetTraitModifiers(type, target.Key) / target.Key.Armour.Defense);

                    if (!lossesPerType.TryAdd(target.Key, damageShare))
                    {
                        lossesPerType[target.Key] += damageShare;
                    }
                }
            }

            return lossesPerType;
        }

        private Dictionary<TroopType, int> CalculateMeleeDamage(List<TroopType> attTroopTypes, Dictionary<TroopType, float> typeShares)
        {
            Dictionary<TroopType, int> lossesPerType = new();
            
            foreach (var type in attTroopTypes)
            {
                int totalDamage = type.Weapon.MeleeDamage * type.TotalAmount;

                foreach (var target in typeShares)
                {
                    int damageShare = Mathf.RoundToInt(
                        totalDamage * target.Value * GetTraitModifiers(type, target.Key) / target.Key.Armour.Defense);
                    
                    if (!lossesPerType.TryAdd(target.Key, damageShare))
                    {
                        lossesPerType[target.Key] += damageShare;
                    }
                }
            }

            return lossesPerType;
        }

        private float GetTraitModifiers(TroopType attackingType, TroopType target)
        {
            float multiplier = 1;

            foreach (var trait in attackingType.Weapon.Traits)
            {
                multiplier += trait switch
                {
                    AttackTrait attackTrait => attackTrait.Target == target.Armour ? attackTrait.Value : 0,
                    TickRelatedTrait tickRelatedTrait => tickRelatedTrait.GetModifier(_tickCounter),
                    AmountRelatedTrait amountRelatedTrait => amountRelatedTrait.GetModifier(attackingType.TotalAmount),
                    _ => 1
                };
            }

            return multiplier;
        }

        private Dictionary<TroopType, float> CalculateTroopTypeShare(List<TroopType> troopTypes)
        {
            int totalAmount = troopTypes.Sum(type => type.TotalAmount);

            return troopTypes.ToDictionary(type => type, type => (float)type.TotalAmount / totalAmount);
        }
    }
}