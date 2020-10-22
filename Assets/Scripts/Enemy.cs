using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int myLife = 10;

    public void TakeDamage(int aValue)
    {
        myLife -= aValue;
        if (myLife <= 0)
        {
            PlayerData.GetInstance().AddGold(10);
            Destroy(gameObject);
        }
    }
}
