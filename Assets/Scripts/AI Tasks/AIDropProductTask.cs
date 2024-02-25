using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDropProductTask : AITask
{
    protected override IEnumerator Task()
    {
        yield return new WaitUntil(() => targetAI.StackEmty);
        OnComplete();
    }
}
