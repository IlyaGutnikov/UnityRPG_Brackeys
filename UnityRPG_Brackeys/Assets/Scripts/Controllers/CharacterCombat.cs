using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float AttackSpeed = 1f;
    private float _attackCooldown = 0f;

    public event Action OnAttack;

    private CharacterStats _myStats;

    void Start()
    {
        _myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        _attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, 1f));

            if (OnAttack != null)
            {
                OnAttack();
            }

            _attackCooldown = 1f / AttackSpeed;
        }
    }

    private IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(_myStats.Damage.GetValue());

    }
}
