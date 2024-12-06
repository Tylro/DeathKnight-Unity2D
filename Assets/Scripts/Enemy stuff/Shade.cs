using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : Enemy
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float stunDuration;

    float timer;

    public static Shade Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Debug.Log("shade spawned");
        SaveData.Instance.SaveShadeData();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ChangeState(EnemyStates.Shade_Idle);
    }
    protected override void Update()
    {
        base.Update();
        if (!PlayerController.Instance.pState.alive)
        {
            ChangeState(EnemyStates.Shade_Idle);
        }
    }

    protected override void UpdateEnemyStates()
    {
        float _dist = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        switch (GetCurrentEnemyState)
        {
            case EnemyStates.Shade_Idle:
                if (_dist < chaseDistance)
                {
                    ChangeState(EnemyStates.Shade_Chase);
                }
                break;

            case EnemyStates.Shade_Chase:
                rb.MovePosition(Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, Time.deltaTime * speed));

                Flip();
                break;

            case EnemyStates.Shade_Stunned:
                timer += Time.deltaTime;

                if (timer > stunDuration)
                {
                    ChangeState(EnemyStates.Shade_Idle);
                    timer = 0;
                }
                break;

            case EnemyStates.Shade_Death:
                Death(Random.Range(5, 10));
                break;
        }
    }

    public override void EnemyGetsHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        base.EnemyGetsHit(_damageDone, _hitDirection, _hitForce);

        if (health > 0)
        {
            ChangeState(EnemyStates.Shade_Stunned);
        }
        else
        {
            ChangeState(EnemyStates.Shade_Death);
        }
    }

    protected override void Death(float _destroyTime)
    {
        rb.gravityScale = 12;
        base.Death(_destroyTime);
    }

    protected override void ChangeCurrentAnimation()
    {
        if(GetCurrentEnemyState == EnemyStates.Shade_Idle)
        {
            anim.Play("Player_Idle");
        }

        anim.SetBool("Walking", GetCurrentEnemyState == EnemyStates.Shade_Chase);

        if (GetCurrentEnemyState == EnemyStates.Shade_Death)
        {
            PlayerController.Instance.RestoreMana();
            SaveData.Instance.SavePlayerData();
            anim.SetTrigger("Death");
            Destroy(gameObject, 0.5f);
        }
    }
    protected override void Attack()
    {
        anim.SetTrigger("Attacking");
        PlayerController.Instance.TakeDamage(damage);
    }

    void Flip()
    {
        sr.flipX = PlayerController.Instance.transform.position.x < transform.position.x;
    }
}
