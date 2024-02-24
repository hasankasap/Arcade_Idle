using UnityEngine;

public class Player : Character
{
    private FloatingJoystick joystick;
    private bool canPlay = false;

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    private void Update()
    {
        if (canPlay)
        {
            Movement();
            if (Input.GetMouseButtonUp(0))
            {
                if (!navMeshAgent.isStopped)
                    navMeshAgent.isStopped = true;
                if (animator != null)
                {
                    animator.SetBool("Run", false);
                }
            }
        }
    }

    protected override void Initialize()
    {
        base.Initialize();
        joystick = FindObjectOfType<FloatingJoystick>();
    }
    protected override void Movement()
    {
        base.Movement();
        if (joystick == null)
            return;
        Vector3 movementDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * Sensitivity;
        if (movementDir.magnitude > .05f)
        {
            if (navMeshAgent.isStopped)
                navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(transform.position + movementDir);
            if (animator != null)
            {
                animator.SetBool("Run", true);
            }
        }

    }
    protected override void OnGameStarted(object[] obj)
    {
        base.OnGameStarted(obj);
        canPlay = true;
    }
}