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
    public State EnemyState
    {
        get { return currentState; }

        set
        {
            // 이전상태 저장
            var prevState = currentState;
            currentState = value;

            // 바뀔 때 필요한 변화
            switch(currentState)
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
                    break;
            }
        }
    }

    public ZombieData zom;

    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    public AttackArea attackArea;

    private bool HasTarget;
    private bool coAtk;
    public float searchDistance = 10f;

    private int damage = 20;
    private int health;
    private float atkInterval = 1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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

        // 공격 체크
        if(attackArea.isAttack)
        {
            EnemyState = State.Attack;
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
        if(HasTarget)
        {
            EnemyState = State.Trace;
        }


        // 이 상태일 때 실행할 기능
        agent.velocity = Vector3.zero;



    }

    private void UpdateTrace()
    {
        if(!HasTarget)
        {
            EnemyState = State.Idle;
        }

        agent.SetDestination(target.position);

        var lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);

    }

    private void UpdateAttack()
    {
        if(!attackArea.isAttack)
        {
            EnemyState = State.Idle;

        }

        agent.velocity = Vector3.zero;


        if(!coAtk)
        {
            StartCoroutine(CoAttack(attackArea.target));

        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);
    }

    private void UpdateDead()
    {
    }
    #endregion

    private IEnumerator CoAttack(GameObject player)
    {


        coAtk = true;
        Debug.Log("공격");
        animator.speed = 3;

        yield return new WaitForSeconds(atkInterval);

        animator.speed = 1;
        coAtk = false;

    }

}
