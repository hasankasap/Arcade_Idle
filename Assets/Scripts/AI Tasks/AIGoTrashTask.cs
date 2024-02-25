using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGoTrashTask : AITask
{
    [SerializeField] private bool manualTarget;
    [SerializeField] private TrashController trash;
    [SerializeField] private TrashController[] trashes;
    public override void Initialize(AI target)
    {
        base.Initialize(target);
        if (!manualTarget)
            trashes = FindObjectsOfType<TrashController>();
    }
    private TrashController GetClosestTrash()
    {
        if (trashes.Length > 1)
        {
            TrashController closest = trashes[0];
            float closestDistance = Vector3.Distance(closest.transform.position, transform.position);
            float distance = 0;
            foreach (TrashController t in trashes)
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
        return trashes[0];
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
            trash = GetClosestTrash();
        }
        if (trash != null)
        {
            targetAI.MovementTarget = trash.GetTranshCanDepositPoint();
            targetAI.MoveToTarget();
        }
        yield return new WaitUntil(() => targetAI.IsTargetReached);
        OnComplete();
    }
}
