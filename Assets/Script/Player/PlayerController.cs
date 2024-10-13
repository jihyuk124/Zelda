
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] PlayerModel playerModel;
    [SerializeField] PlayerInputReader input;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float smoothTime = 0.2f;

    [Header("Attack Settings")]
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float attackDistance = 1f;
    [SerializeField] int attackDamage = 10;

    Transform mainCam;

    float gatheringDuration = 1.0f;

    List<Timer> timers;
    CountdownTimer gatheringTimer;
    StateMachine stateMachine;

    static readonly int Speed = Animator.StringToHash("Speed");
    float currentSpeed;
    float velocity;

    private Vector3 moveInput = Vector3.zero;
    private const float ZeroF = 0f;
    private List<InteractableObject> interactableObjectList = new List<InteractableObject>();
    private InteractableObject currentInteractionObject = = null;

    // private List<>

    // Animator parameters

#region 유니티 이벤트 메세지
    void Awake()
    {
        mainCam = Camera.main.transform;
        rb.freezeRotation = true;

        SetupTimers();
        SetupStateMachine();
    }

    void Start()
    {
        input.EnablePlayerActions();
    }

    void OnEnable()
    {
        input.Move += Move;
        input.Click += Click;
        input.ChangeWeapon += playerModel.ChangeNextWeapon;
        input.ChangeShield += playerModel.ChangeNextShield;
        input.StartInteraction += StartInteraction;
        input.CancelInteraction += CancelInteract;
        input.InventoryToggle += InventoryToggle;
    }

    void OnDisable()
    {
        input.Move -= Move;
        input.Click -= Click;
        input.ChangeWeapon -= playerModel.ChangeNextWeapon;
        input.ChangeShield -= playerModel.ChangeNextShield;
    }
    void Update()
    {
        stateMachine.Update();
        HandleTimers();
        UpdateAnimator();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        CheckInteraction();
    }
    #endregion

    void SetupTimers()
    {
        // Setup timers
        gatheringTimer = new CountdownTimer(gatheringDuration);
        timers = new(1) { gatheringTimer };
    }

    void SetupStateMachine()
    {
        // State Machine
        stateMachine = new StateMachine();

        // Declare states
        var locomotionState = new LocomotionState(this, animator);
        var gatheringState = new GatheringState(this, animator);

        // Define transitions
        At(locomotionState, gatheringState, new FuncPredicate(() => gatheringTimer.IsRunning));
        At(gatheringState, locomotionState, new FuncPredicate(() => !gatheringTimer.IsRunning));

        Any(locomotionState, new FuncPredicate(ReturnToLocomotionState));

        // Set initial state
        stateMachine.SetState(locomotionState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    void Move(Vector2 val)
    {
        moveInput = new Vector3(val.x, 0, val.y);
    }

    bool ReturnToLocomotionState()
    {
        return !gatheringTimer.IsRunning;
    }

    public void Click()
    {
        if (gatheringTimer.IsRunning)
        {
            return;
        }
    }

    public void StartInteraction()
    {
        if (gatheringTimer.IsRunning)
        {
            return;
        }

        if (interactableObjectList.Count == 0)
        {
            return;
        }
        Vector3 playerPosition = transform.position;
        interactableObjectList.Sort((obj1, obj2) =>
        {
            return Vector3.Distance(playerPosition, obj1.transform.position)
            .CompareTo(Vector3.Distance(playerPosition, obj2.transform.position));
        }
        );


        Vector3 direction = interactableObjectList[0].transform.position - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            gatheringTimer.Start();
        }
    }

    public void CancelInteract()
    {

    }

    public void InventoryToggle()
    {

    }

    public void HandleMovement()
    {
        if (moveInput.magnitude > ZeroF)
        {
            moveInput.Normalize();
            transform.rotation = Quaternion.LookRotation(moveInput);
            currentSpeed = Mathf.SmoothDamp(currentSpeed, moveSpeed, ref velocity, smoothTime);
        }
        else
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, ZeroF, ref velocity, smoothTime);
        }
        float deltaSpeed = currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.forward * deltaSpeed);
    }

    void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat(Speed, currentSpeed / moveSpeed);
    }

    void CheckInteraction()
    {
        interactableObjectList.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f);
        if (hitColliders.Length == 0)
        {
            return;
        }

        for (int i = 0; i < hitColliders.Length; i++)
        {
            var interactableObject = hitColliders[i].gameObject.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                interactableObjectList.Add(interactableObject);
                Debug.Log("interactableObject add");
            }
        }
    }
}
