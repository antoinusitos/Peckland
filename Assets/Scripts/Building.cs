using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private string myName = "";

    [SerializeField]
    private int myCost = 0;

    public string GetName()
    {
        return myName + " (" + myCost + ")";
    }

    public int GetCost()
    {
        return myCost;
    }
}
