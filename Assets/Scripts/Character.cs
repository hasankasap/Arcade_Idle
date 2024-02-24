
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour, ICharacter
{
    [SerializeField] protected CharacterSO characterSO;
    [SerializeField] protected StackController stackController;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    private float speedAddValue = 0;

    public float Speed { get { return characterSO.Speed + speedAddValue; } set { Speed = characterSO.Speed; } }

    public float Sensitivity { get { return characterSO.Sensitivity; } set { Sensitivity = characterSO.Sensitivity; } }
    public float RotationSpeed { get { return characterSO.RotationSpeed; } set { RotationSpeed = characterSO.RotationSpeed; } }
    public float Acceleration { get { return characterSO.Acceleration; } set { Acceleration = characterSO.Acceleration; } }

    protected virtual void Start()
    {
        Initialize();
    }
    protected virtual void OnEnable()
    {
        EventManager.StartListening(GameEvents.GAME_STARTED, OnGameStarted);
    }
    protected virtual void OnDisable()
    {
        EventManager.StopListening(GameEvents.GAME_STARTED, OnGameStarted);
    }

    protected virtual void Initialize()
    {
        stackController = GetComponentInChildren<StackController>();
        if (stackController != null)
            stackController.Initialize(characterSO.StackCapacity);
        else Debug.LogError("Stack controller not found !!");
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.speed = Speed;
        navMeshAgent.acceleration = Acceleration;
        navMeshAgent.angularSpeed = RotationSpeed;

        animator = GetComponentInChildren<Animator>();
    }
    protected virtual void Movement()
    {
    }

    public void TakeProducts(Product asset)
    {
        if (stackController != null)
            stackController.AddStack(asset);
    }

    public bool CanTakeProducts()
    {
        if (stackController != null)
            return !stackController.IsStackFull();
        return false;
    }

    public Product DropProductsWithType(ProductTypes type)
    {
        if (stackController == null)
            return null;
        return stackController.GetLastProductWithType(type);
    }

    public bool CanDropWantedProductTypes(ProductTypes type)
    {
        if (stackController != null)
            return stackController.IsStackHasWantedProducts(type);
        return false;
    }
    public Product DropToTrash()
    {
        return stackController.GetLastProduct();
    }

    public bool CanDropProductToTrash()
    {
        if (stackController != null)
            return stackController.IsStackHasAnyProduct();
        return false;
    }

    protected virtual void OnGameStarted(object[] obj)
    {

    }
}