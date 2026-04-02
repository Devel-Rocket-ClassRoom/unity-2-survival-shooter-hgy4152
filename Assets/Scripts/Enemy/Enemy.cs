using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 상태 패턴 연습
    public enum State
    {
        Idle,
        Trace,
        Attack,
        Dead
    }

    private State currentState;
    public State CurrentState
    {
        get { return currentState; }

        set
        {
            // 이전상태 저장
            var prevState = currentState;
            currentState = value;

            // 바뀔 때 필요한 변화
            switch (currentState)
            {
                case State.Idle:
                    animator.SetBool("isTrace", false);
                    agent.isStopped = true;
                    break;
                case State.Trace:
                    animator.SetBool("isTrace", true);
                    agent.isStopped = false;
                    break;
                case State.Attack:
                    animator.SetBool("isTrace", true);
                    agent.isStopped = false;
                    break;
                case State.Dead:
                    animator.SetTrigger("isDead");
                    agent.enabled = false;
                    break;
            }
        }
    }

    // 나중에 스포너에서 할당
    public ZombieData zom;

    public AtkPattern atkPattern; // >> 공격패턴 추가

    private Transform target;
    private NavMeshAgent agent;
    public Animator animator;
    public AttackArea attackArea;

    public ParticleSystem hitEffect;

    private bool HasTarget;
    public float searchDistance = 10f;

    public int damage = 20;
    public int health;
    public float atkInterval = 1f;



    public bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    public void SetUp()
    {
        health = zom.Health;
        damage = zom.Attack;
        agent.speed = zom.Speed;
        atkInterval = zom.AtkInterval;
    }

    private void Update()
    {
        // 거리 체크
        if (searchDistance > Vector3.Distance(transform.position, target.position))
        {
            HasTarget = true;
        }
        else
        {
            HasTarget = false;
        }


        if(health <= 0 && !isDead)
        {
            CurrentState = State.Dead;
            isDead = true;
        }

        switch (currentState)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Trace:
                UpdateTrace();
                break;
            case State.Attack:
                UpdateAttack();
                break;
            case State.Dead:
                UpdateDead();
                break;
        }

    }
    #region State
    private void UpdateIdle()
    {
        // 이 상태일 때 전환 가능한 상태들
        if (HasTarget)
        {
            CurrentState = State.Trace;
        }


        // 이 상태일 때 실행할 기능
        agent.velocity = Vector3.zero;



    }

    private void UpdateTrace()
    {
        if (!HasTarget)
        {
            CurrentState = State.Idle;
        }

        // 공격 체크
        if (attackArea.isAttack)
        {
            CurrentState = State.Attack;
        }

        agent.SetDestination(target.position);

        var lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);

    }

    private void UpdateAttack()
    {
        if (!attackArea.isAttack)
        {
            CurrentState = State.Idle;

        }

        agent.velocity = Vector3.zero;

        // 생성할때 맞춰서 스포너에서 할당해주기
        atkPattern.Attack(target.gameObject, gameObject);

        var lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);
    }

    private void UpdateDead()
    {
        Destroy(gameObject, 2f);
    }
    #endregion

    public void OnDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;


        hitEffect.transform.LookAt(hitPoint);
        hitEffect.Play();
    }

    public void StartSinking()
    {
        StartCoroutine(CoSinking());

    }

    IEnumerator CoSinking()
    {
        float sinkSpeed = 3f; 

        while (true)
        {
            transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
            if (transform.position.y < -5f)
            {
                break;
            }

            yield return null; 
        }
    }
}
