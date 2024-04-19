using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class is responsible for the movement of the enemies. It will move any object that has this script attached to it in a random direction every frame.
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [SerializeField, Tooltip("The speed of the enemy.")] private float speed = 1.0f;
    [SerializeField, Tooltip("The interval in seconds it takes the enemy to change direction.")] private float changeDirectionInterval = 1.5f;
    private float changeDirectionTimer = 0.0f;
    private Vector3 direction;

    // This method will move the enemy in a random direction. It will use the speed variable to determine how fast the enemy will move.
    // The transform.Translate method will move the enemy in the direction of the vector calculated by the calculateRandomDirection method.
    private void MoveInRandomDirection()
    {
        // Calculate the movement direction
        Vector3 movementDirection = direction * speed * Time.deltaTime;

        // Move the character
        transform.Translate(movementDirection, Space.World);

        // Rotate the character to face the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100f * Time.deltaTime);
        }
    }


    private Vector3 calculateRandomDirection()
    {
        changeDirectionTimer = changeDirectionInterval;
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        return randomDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(changeDirectionTimer <= 0)
        {
            Debug.Log("Changing direction...");
            direction = calculateRandomDirection();
        }
        else
        {
            changeDirectionTimer -= Time.deltaTime;
        }
        MoveInRandomDirection();
    }
}
