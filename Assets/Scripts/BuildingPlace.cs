using UnityEngine;
using UnityEngine.UI;

public class BuildingPlace : MonoBehaviour
{
    [SerializeField]
    private GameObject myBuildCanvas = null;

    [SerializeField]
    private GameObject myBuildingCanvas = null;

    [SerializeField]
    private GameObject myRuinObject = null;

    private SpriteRenderer mySpriteRenderer = null;

    private bool myWantToBuild = false;

    private bool myIsBought = false;
    [SerializeField]
    private int myCost = 100;

    [SerializeField]
    private Text myShowingText = null;

    [SerializeField]
    private Building[] myBuildingsToBuild = null;

    [SerializeField]
    private Text myBuildingText = null;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovePlayer>())
        {
            if(myIsBought)
            {
                myShowingText.text = "Build";
            }
            else
            {
                myShowingText.text = "Buy";
            }

            myBuildCanvas.SetActive(true);
            myWantToBuild = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MovePlayer>())
        {
            myBuildCanvas.SetActive(false);
            myWantToBuild = false;
            myBuildingCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if (myWantToBuild && Input.GetKeyDown(PlayerInput.myActionKey))
        {
            if(myIsBought)
            {
                myBuildCanvas.SetActive(false);
                myBuildingCanvas.SetActive(true);
            }
            else if(PlayerData.GetInstance().GetGold() >= myCost)
            {
                PlayerData.GetInstance().RemoveGold(myCost);
                myIsBought = true;
                myRuinObject.SetActive(false);
                mySpriteRenderer.enabled = true;
                myShowingText.text = "Build";
            }
        }
    }

    public void TryToBuild(int aBuildingIndex)
    {
        if (aBuildingIndex != -1 && aBuildingIndex < myBuildingsToBuild.Length && myBuildingsToBuild[aBuildingIndex] != null && myBuildingsToBuild[aBuildingIndex].GetCost() <= PlayerData.GetInstance().GetGold())
        {
            PlayerData.GetInstance().RemoveGold(myBuildingsToBuild[aBuildingIndex].GetCost());
            Instantiate(myBuildingsToBuild[aBuildingIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ShowBuilding(int aBuildingIndex)
    {
        if(aBuildingIndex != -1 && aBuildingIndex < myBuildingsToBuild.Length && myBuildingsToBuild[aBuildingIndex] != null)
        {
            myBuildingText.text = myBuildingsToBuild[aBuildingIndex].GetName();
        }
        else
        {
            myBuildingText.text = "";
        }
    }
}
