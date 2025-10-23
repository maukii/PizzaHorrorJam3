using System.Collections.Generic;
using UnityEngine;

public static class GameEventSystem
{
    public static System.Action<CollectableType> OnCollectableCollected;

    private static readonly HashSet<CollectableType> _collectedItems = new HashSet<CollectableType>();


    public static void RegisterCollectable(CollectableType type)
    {
        if (_collectedItems.Contains(type)) return;

        _collectedItems.Add(type);

        Debug.Log($"Collected {type}");
        OnCollectableCollected?.Invoke(type);
    }

    public static bool IsCollected(CollectableType type) => _collectedItems.Contains(type);

    public static void Reset() => _collectedItems.Clear();
}
