using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : Character
{
    [SerializeField] private AITask[] tasks;
    private int taskIndex = 0;
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Movement()
    {
        base.Movement();
    }
    protected override void OnGameStarted(object[] obj)
    {
        base.OnGameStarted(obj);
        if (tasks == null || tasks.Length == 0) return;
        foreach (var task in tasks) 
        {
            task.Initialize(this);
        }
        tasks[taskIndex].DoTask();
    }
    public void DoNextTask()
    {
        taskIndex++;
        if (taskIndex >= tasks.Length) taskIndex = 0;
        tasks[taskIndex].DoTask();
    }
}