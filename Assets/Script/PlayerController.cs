
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] InputReader input;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] float smoothTime = 0.2f;

    float gatheringDuration = 1.0f;

    List<Timer> timers;
    CountdownTimer gatheringTimer;

    StateMachine stateMachine;

    static readonly int Speed = Animator.StringToHash("Speed");

    private Vector3 moveInput = Vector3.zero;
    private const float ZeroF = 0f;

    private bool isClickCollectible = false;

    // Animator parameters

#region 유니티 이벤트 메세지
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

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
    }

    void OnDisable()
    {
        input.Move -= Move;
        input.Click -= Click;
    }
    void Update()
    {
        stateMachine.Update();
        HandleTimers();
        CheckCollision();
        UpdateAnimator();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        //transform.LookAt(transform.position + moveInput.normalized);
        //agent.SetDestination(transform.position + moveInput.normalized);
    }
#endregion

    void SetupTimers()
    {
        // Setup timers
        gatheringTimer = new CountdownTimer(gatheringDuration);
        timers = new(1) { gatheringTimer };
    }

    private void SetupStateMachine()
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

    private void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

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
        RaycastHit hit;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            var collectible = hit.collider.gameObject.GetComponent<Collectible>();
            if (collectible != null)
            {
                Debug.Log("Click collectible!");
                isClickCollectible = true;
            }
        }
        agent.SetDestination(hit.point);
    }
    private void CheckCollision()
    {
        if (isClickCollectible)
        {
            // 목적지까지의 남은 거리가 거의 0이고, 에이전트가 경로를 따라 움직이고 있을 때
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // 남은 거리가 stoppingDistance 이하이고, 에이전트가 정지 상태라면 도달한 것으로 간주
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    isClickCollectible = false;
                    Debug.Log("Agent has reached the destination!");
                    gatheringTimer.Start();
                }
            }
        }
    }
    void HandleTimers()
    {
        foreach (var timer in timers)
        {
            timer.Tick(Time.deltaTime);
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat(Speed, agent.velocity.magnitude / agent.speed);
    }

    public void HandleMovement()
    {

    }
}
