using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITask: MonoBehaviour
{
    protected AI targetAI;

    public virtual void Initialize(AI target)
    {
        if (target == null)
        {
            Debug.LogError("Target AI not found - " + this);
            return;
        }
        targetAI = target;
    }
    protected virtual void OnComplete()
    {
        targetAI.DoNextTask();
    }
    public virtual void DoTask()
    {
        StartCoroutine(Task());
    }

    protected virtual IEnumerator Task()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("AI task completed");
        OnComplete();
    }
}
