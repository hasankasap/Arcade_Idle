using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITakeProductTask : AITask
{
    protected override IEnumerator Task()
    {
        yield return new WaitUntil(() => targetAI.StackFull);
        OnComplete();
    }
}
