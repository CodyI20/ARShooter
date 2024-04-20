using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Tooltip("The speed of the enemy.")] private float speed = 1.0f;
    [SerializeField, Tooltip("The interval in seconds it takes the enemy to change direction.")] private float changeDirectionInterval = 1.5f;

    private float changeDirectionTimer = 0.0f;
    private float changeDirectionSpeed = 1f;
    private Vector3 direction;

    private void MoveInRandomDirection()
    {
        Vector3 movementDirection = direction * speed * Time.deltaTime;
        transform.Translate(movementDirection, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 3 * changeDirectionSpeed * Time.deltaTime);
        }
    }

    private Vector3 calculateRandomDirection()
    {
        changeDirectionTimer = changeDirectionInterval;
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        return randomDirection.normalized;
    }

    private void applyChangeDirection()
    {
        if (changeDirectionTimer <= 0)
        {
            //Debug.Log("Changing direction...");
            direction = calculateRandomDirection();

            float angleDifference = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction));
            changeDirectionSpeed = angleDifference / changeDirectionInterval;
        }
        else
        {
            changeDirectionTimer -= Time.deltaTime;
        }
    }

    void Update()
    {
        //Only gets executed once every changeDirectionInterval seconds
        applyChangeDirection();
        //Gets executed every frame
        MoveInRandomDirection();
    }
}
