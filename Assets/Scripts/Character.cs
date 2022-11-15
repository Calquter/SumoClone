using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator animator;
    [SerializeField] protected Rigidbody _rigidBody;

    [SerializeField] protected float _playerSpeed;
    [SerializeField] protected float _baseForce;
    public int takenHitCountFromBack;


    public bool isTakenHit;
    public float timer = 2f;


    private float CalculateHitForce(Transform enemyTransform, out float dotValue) //Calculate additional hit force.
    {
        dotValue = DotProductForHitter(enemyTransform);

        if (dotValue >= 0.3f) //This means you are in front of the enemy.
            return 1f;
        else if (dotValue < 0.3f && dotValue > -0.3f) //This means you are beside of the enemy.
            return 1.5f;
        else //This means you are behind the enemy.
            return 2f;
    }

    protected void HitToEnemies(Rigidbody enemyRB)
    {
        animator.SetTrigger("Hit");

        float dotValue;
        float additionalHitForceMultiplier = CalculateHitForce(enemyRB.transform, out dotValue);
       
        if (dotValue <= 0.3f)
        {
            Character character = enemyRB.GetComponent<Character>();
            character.isTakenHit = true; //When you hit an enemy, they need to stop moving somwhere and this controls it.

            character.timer += (Mathf.Abs(dotValue) * 0.5f) + (enemyRB.GetComponent<Character>().takenHitCountFromBack * 0.3f);

            Vector3 forceDirection = enemyRB.transform.position - transform.position;
            enemyRB.AddForce(forceDirection * _baseForce * additionalHitForceMultiplier, ForceMode.VelocityChange);
            character.animator.SetTrigger("TookHit");
        }
            
    }

    private float DotProductForHitter(Transform enemyTransform) //This method will calculate where is the hitter.
    {
        return Vector3.Dot(enemyTransform.forward, transform.position - enemyTransform.position);
    }

    protected void HitAllow() //Controls the stun time when you get a hit.
    {
        if (!isTakenHit)
            return;

        if (timer <= 0)
        {
            isTakenHit = false;
            timer = 1f;
        }
        else
            timer -= Time.deltaTime;
    }
}
