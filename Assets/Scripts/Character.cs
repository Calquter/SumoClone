using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    [SerializeField] protected float _playerSpeed;
    [SerializeField] protected float _baseForce;
    public int takenHitCount;


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

    private void HitToEnemies(Rigidbody enemyRB)
    {
        _animator.SetTrigger("Hit");

        float additionalHitForceMultiplier = CalculateHitForce(enemyRB.transform);

        Vector3 forceDirection = enemyRB.transform.position - transform.position;
        enemyRB.AddForce(forceDirection * _baseForce * additionalHitForceMultiplier, ForceMode.VelocityChange);
    }

    private float DotProductForHitter(Transform enemyTransform) //This method will calculate where is the hitter.
    {
        return Vector3.Dot(enemyTransform.forward, transform.position - enemyTransform.position);
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // Layer 6 is enemy layer.
            HitToEnemies(collision.gameObject.GetComponent<Rigidbody>());
    }
}
