using UnityEngine;
using UnityEngine.AI;

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
                if (animator != null)
                {
                    animator.SetBool("Run", false);
                }
            }
        }
    }

    protected override void Initialize()
    {
        stackController = GetComponentInChildren<StackController>();
        if (stackController != null)
            stackController.Initialize(characterSO.StackCapacity);
        else Debug.LogError("Stack controller not found !!");

        animator = GetComponentInChildren<Animator>();
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
            transform.LookAt(transform.position + movementDir);
            transform.position += (movementDir * Sensitivity * Speed * Time.deltaTime);
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