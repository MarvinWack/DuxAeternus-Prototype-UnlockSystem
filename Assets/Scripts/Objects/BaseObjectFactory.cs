using System;
using Core;
using UnityEngine;

namespace Objects
{
    public class BaseObjectFactory : MonoBehaviour, ITickReceiverBuilder, IRequirementBuilder, IUnlockableBuilder
    {
        [Header("Prefabs")]
        [SerializeField] private Tech techPrefab;
        [SerializeField] private CoreBuildingManager coreBuildingManagerPrefab;
        [SerializeField] private SmallBuildingManager smallBuildingManagerPrefab;
        [SerializeField] private LargeBuildingManager largeBuildingManagerPrefab;
        
        public event Action<ITickReceiver> OnUpdatableCreated;
        public event IRequirementBuilder.RequirementCreated OnRequirementCreated;
        public event IUnlockableBuilder.UnlockableCreated OnUnlockableCreated;

        private UnlockHandler _unlockHandler = new();


        private void Awake()
        {
            UpdateSubscriber.RegisterUpdatableBuilder(this);
            _unlockHandler.RegisterUnlockableBuilder(this);
            _unlockHandler.RegisterRequirementBuilder(this);
        }

        public BaseObject CreateObject(ObjectBluePrint bluePrint, Transform parent)
        {
            return bluePrint.ObjectType switch
            {
                ObjectType.Building => CreateBuildingManager(bluePrint as BuildingBlueprint, parent.Find("Buildings")),
                ObjectType.Research => CreateTech(bluePrint as TechBlueprint, parent.Find("Techs")),
                _ => throw new Exception("Invalid Object Type")
            };
        }

        private BaseObject CreateBuildingManager(BuildingBlueprint bluePrint, Transform parent)
        {
            BuildingManager instance = bluePrint.Type switch
            {
                BuildingType.Core => Instantiate(coreBuildingManagerPrefab, parent),
                BuildingType.Small => Instantiate(smallBuildingManagerPrefab, parent),
                BuildingType.Large => Instantiate(largeBuildingManagerPrefab, parent),
                _ => throw new Exception("Invalid Building Type")
            };
            
            instance.name = bluePrint.name + "Manager";

            instance.SetData(bluePrint);
            
            return Setup(instance, bluePrint);
        }

        private BaseObject CreateTech(TechBlueprint bluePrint, Transform parent)
        {
            Tech instance = Instantiate(techPrefab, parent);
            
            instance.name = bluePrint.name;
            
            instance.SetData(bluePrint);
            
            return Setup(instance, bluePrint);
        }

        private BaseObject Setup(BaseObject instance, ObjectBluePrint bluePrint)
        {
            OnUpdatableCreated?.Invoke(instance);
            OnRequirementCreated?.Invoke(instance);
            OnUnlockableCreated?.Invoke(instance.GetEventHandler(), instance.GetRequirements());
            
            // instance.SetData(bluePrint);
            
            return instance;
        }
    }
}