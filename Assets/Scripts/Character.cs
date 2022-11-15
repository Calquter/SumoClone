using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody _rigidBody;

    [SerializeField] protected float _playerSpeed;
    [SerializeField] protected float _baseForce;
    public int takenHitCountFromBack;


    public bool isTakenHit;
    private float _timer = 2f;

    private void LateUpdate()
    {
        HitAllow();
    }

    private float CalculateHitForce(Transform enemyTransform)
    {
        float dotValue = DotProductForHitter(enemyTransform);

        if (dotValue >= 0.3f) //This means you are in front of the enemy.
            return 1f;
        else if (dotValue < 0.3f && dotValue > -0.3f) //This means you are beside of the enemy.
            return 1.5f;
        else //This means you are behind the enemy.
            return 2f;
    }

    protected void HitToEnemies(Rigidbody enemyRB)
    {
        _animator.SetTrigger("Hit");

        Character character = enemyRB.GetComponent<Character>();
        character.isTakenHit = true;
        print(character.gameObject.name);


        float additionalHitForceMultiplier = CalculateHitForce(enemyRB.transform);

        Vector3 forceDirection = enemyRB.transform.position - transform.position;
        enemyRB.AddForce(forceDirection * _baseForce * additionalHitForceMultiplier, ForceMode.VelocityChange);
    }

    private float DotProductForHitter(Transform enemyTransform) //This method will calculate where is the hitter.
    {
        return Vector3.Dot(enemyTransform.forward, transform.position - enemyTransform.position);
    }

    private void HitAllow()
    {
        if (!isTakenHit)
            return;

        if (_timer <= 0)
        {
            isTakenHit = false;
            _timer = 2f;
        }
        else
            _timer -= Time.deltaTime;
    }
}
