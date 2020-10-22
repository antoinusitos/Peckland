using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private List<Enemy> myAllCollidingEntities;

    private void Awake()
    {
        myAllCollidingEntities = new List<Enemy>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(PlayerInput.myActionKey))
        {
            for(int i = 0; i < myAllCollidingEntities.Count; i++)
            {
                myAllCollidingEntities[i].TakeDamage(100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        if (myAllCollidingEntities.Contains(enemy))
            return;

        myAllCollidingEntities.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        if (!myAllCollidingEntities.Contains(enemy))
            return;

        myAllCollidingEntities.Remove(enemy);
    }
}
