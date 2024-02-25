using UnityEngine;

public class AI : Character
{
    public Transform MovementTarget;
    public bool StackFull => stackController != null ? stackController.IsStackFull() : true;
    public bool StackEmty => stackController != null ? !stackController.IsStackHasAnyProduct() : false;
    public bool IsTargetReached => MovementTarget != null ? Vector3.Distance(transform.position, MovementTarget.position) < .25f : false;
    [SerializeField] private AITask[] tasks;
    private int taskIndex = 0;
    protected override void Initialize()
    {
        base.Initialize();
    }
    protected override void Movement()
    {
        navMeshAgent.SetDestination(MovementTarget.position);
    }
    public void MoveToTarget() 
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        if (animator != null) animator.SetBool("Run", true);
        Movement();
    }
    public void StopMovement()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        if (animator != null) animator.SetBool("Run", false);
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