using System;
using UnityEngine;

namespace Objects.Research
{
    public class BaseObjectFactory : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private BaseObject techPrefab;
        [SerializeField] private CoreBuildingManager coreBuildingManagerPrefab;
        [SerializeField] private SmallBuildingManager smallBuildingManagerPrefab;
        [SerializeField] private LargeBuildingManager largeBuildingManagerPrefab;
        
        public BaseObject CreateObject(ObjectBluePrint bluePrint, Transform parent)
        {
            return bluePrint.ObjectType switch
            {
                ObjectType.Building => CreateBuildingManager(bluePrint as BuildingBlueprint, parent),
                ObjectType.Research => CreateTech(bluePrint, parent),
                _ => throw new Exception("Invalid Object Type")
            };
        }

        private BaseObject CreateBuildingManager(BuildingBlueprint bluePrint, Transform parent)
        {
            BaseObject instance = bluePrint.Type switch
            {
                BuildingType.Core => Instantiate(coreBuildingManagerPrefab, parent),
                BuildingType.Small => Instantiate(smallBuildingManagerPrefab, parent),
                BuildingType.Large => Instantiate(largeBuildingManagerPrefab, parent),
                _ => throw new Exception("Invalid Building Type")
            };
            
            instance.name = bluePrint.name + "Manager";
            instance.SetData(bluePrint);
            return instance;
        }

        private BaseObject CreateTech(ObjectBluePrint bluePrint, Transform parent)
        {
            var instance = Instantiate(techPrefab, parent);
            instance.name = bluePrint.name;
            instance.SetData(bluePrint as TechBlueprint);
            return instance;
        }
    }
}