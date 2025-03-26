using System;
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
            return Array.Empty<ObjectBluePrint>();
        }

        var blueprints = new ObjectBluePrint[RequiredLevels.Count];
        RequiredLevels.Keys.CopyTo(blueprints, 0);
        return blueprints;
    }
}
