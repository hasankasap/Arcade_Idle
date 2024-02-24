using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrashTask : AITask
{
    protected override IEnumerator Task()
    {
        targetAI.transform.position = Vector3.one * 100f;
        return base.Task();
    }
}
