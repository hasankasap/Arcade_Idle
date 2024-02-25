using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoSpawnerTask : AITask
{
    [SerializeField] private bool manualTarget;
    [SerializeField] private AssetSpawner spawner;
    [SerializeField] private AssetSpawner[] spawners;

    public override void Initialize(AI target)
    {
        base.Initialize(target);
        if (!manualTarget)
            spawners = FindObjectsOfType<AssetSpawner>();
    }
    private AssetSpawner GetClosestTrash()
    {
        if (spawners.Length > 1)
        {
            AssetSpawner closest = spawners[0];
            float closestDistance = Vector3.Distance(closest.transform.position, transform.position);
            float distance = 0;
            foreach (AssetSpawner t in spawners)
            {
                distance = Vector3.Distance(t.transform.position, transform.position);
                if (distance <= closestDistance)
                {
                    closest = t;
                    closestDistance = distance;
                }
            }
            return closest;
        }
        return spawners[0];
    }
    protected override void OnComplete()
    {
        targetAI.StopMovement();
        targetAI.DoNextTask();
    }
    protected override IEnumerator Task()
    {
        if (!manualTarget)
        {
            spawner = GetClosestTrash();
        }
        if (spawner != null)
        {
            targetAI.MovementTarget = spawner.GetSpawnAreaCenter();
            targetAI.MoveToTarget();
        }
        yield return new WaitUntil(() => targetAI.IsTargetReached);
        OnComplete();
    }
}
