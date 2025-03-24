using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu]
public class UnlockRequirements : ScriptableObject
{
    public SerializedDictionary<ObjectBluePrint, int> RequiredLevels;
    public ushort UnlockTime;

    public ObjectBluePrint[] GetObjectBluePrints()
    {
        if (RequiredLevels == null || RequiredLevels.Count == 0)
        {
            return new ObjectBluePrint[0];
        }

        ObjectBluePrint[] blueprints = new ObjectBluePrint[RequiredLevels.Count];
        RequiredLevels.Keys.CopyTo(blueprints, 0);
        return blueprints;
    }
}
