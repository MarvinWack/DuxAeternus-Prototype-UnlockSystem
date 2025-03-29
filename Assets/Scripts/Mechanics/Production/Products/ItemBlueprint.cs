using Production.Storage;
using UnityEngine;
using UnityEngine.Serialization;

namespace Production.Items
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }
    
    public enum DualWeilding
    {
        Disabled,
        Enabled,
        TwoHanded
    }
    
    [CreateAssetMenu]
    public class ItemBlueprint : ProductBlueprint
    {
        public RequiredProducts Cost;
        public int MeleeDamage;
        public int RangedDamage;
        public int Defense;
        public WeaponType WeaponType;
        DualWeilding DualWeilding;
    }
}