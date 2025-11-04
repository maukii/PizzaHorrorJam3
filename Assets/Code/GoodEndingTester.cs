using UnityEngine;

public class GoodEndingTester : MonoBehaviour
{
    void Start()
    {
        GameEventSystem.RegisterCollectable(CollectableType.Collectable_1);
        GameEventSystem.RegisterCollectable(CollectableType.Collectable_2);
        GameEventSystem.RegisterCollectable(CollectableType.Collectable_3);
        GameEventSystem.RegisterCollectable(CollectableType.Collectable_4);
    }
}
