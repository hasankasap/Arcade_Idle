using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoTransformerPickPointTask : AITask
{
    [SerializeField] private bool manualTarget;
    [SerializeField] private AssetTransformer tranformer;
    [SerializeField] AssetTransformer[] transformers;
    public override void Initialize(AI target)
    {
        base.Initialize(target);
        if (!manualTarget)
            transformers = FindObjectsOfType<AssetTransformer>();
    }
    private AssetTransformer GetClosestTrash()
    {
        if (transformers.Length > 1)
        {
            AssetTransformer closest = transformers[0];
            float closestDistance = Vector3.Distance(closest.transform.position, transform.position);
            float distance = 0;
            foreach (AssetTransformer t in transformers)
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
        return transformers[0];
    }
    protected override void OnComplete()
    {
        targetAI.DoNextTask();
        targetAI.StopMovement();
    }
    protected override IEnumerator Task()
    {
        if (!manualTarget)
        {
            tranformer = GetClosestTrash();
        }
        if (tranformer != null)
        {
            targetAI.MovementTarget = tranformer.GetPicUpAreaCenter();
            targetAI.MoveToTarget();
        }
        yield return new WaitUntil(() => targetAI.IsTargetReached);
        OnComplete();
    }
}
