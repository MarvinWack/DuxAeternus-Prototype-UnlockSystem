// using Production.Items;
// using UI.Slot;
// using UnityEngine;
//
// namespace UI
// {
//     public class ParameterButton : ExtendedButton, IParameterButton<int>
//     {
//         [SerializeField] private ItemBlueprint _itemBlueprint;
//         [SerializeField] private int _intValue;
//         
//         public void SetParameter(ItemBlueprint parameter)
//         {
//             _itemBlueprint = parameter;
//         }
//
//         public void SetParameter(int value)
//         {
//             _intValue = value;
//         }
//
//         public int GetParameter()
//         {
//             throw new System.NotImplementedException();
//         }
//
//         public ItemBlueprint GetItemBlueprint()
//         {
//             return _itemBlueprint;
//         }
//
//         public int GetIntValue()
//         {
//             return _intValue;
//         }
//     }
// }