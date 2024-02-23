
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour, ICharacter
{

    [SerializeField] protected CharacterSO characterSO;
    protected NavMeshAgent navMeshAgent;
    protected Animator animator;

    private float speedAddValue = 0;

    public float Speed { get { return characterSO.speed + speedAddValue; } set { Speed = characterSO.speed; } }

    public float Sensitivity { get { return characterSO.sensitivity; } set { Sensitivity = characterSO.sensitivity; } }
    public float RotationSpeed { get { return characterSO.rotationSpeed; } set { RotationSpeed = characterSO.rotationSpeed; } }
    public float Acceleration { get { return characterSO.acceleration; } set { Acceleration = characterSO.acceleration; } }

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
    }

    public bool CanTakeProducts()
    {

        return false;
    }

    public Product DropProductsWithType(ProductTypes type)
    {
        return null;
    }

    public bool CanDropWantedProductTypes(ProductTypes type)
    {
        return false;
    }
    public Product DropToTrash()
    {
        return null;
    }

    public bool CanDropProductToTrash()
    {
        return false;
    }

    protected virtual void OnGameStarted(object[] obj)
    {

    }
}