using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    private static PlayerData myInstance = null;

    private int myGold = 0;

    [SerializeField]
    private Text myGoldText = null;

    public static PlayerData GetInstance()
    {
        return myInstance;
    }

    private void Awake()
    {
        if (PlayerData.GetInstance())
            Destroy(gameObject);
        myInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetGold()
    {
        return myGold;
    }

    public void RemoveGold(int aValue)
    {
        myGold -= aValue;
        myGoldText.text = myGold.ToString();
    }

    public void AddGold(int aValue)
    {
        myGold += aValue;
        myGoldText.text = myGold.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddGold(100);
        }
    }
}
