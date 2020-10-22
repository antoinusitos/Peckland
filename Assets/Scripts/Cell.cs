using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum CellType
    {
        NONE,
        MAINPATH,
        BONUS
    }

    public int myX = 0;
    public int myY = 0;
    public bool myIsMainPath = false;
    public bool myIsLinkedToMainPath = false;

    public Cell myTopCell = null;
    public Cell myRightCell = null;
    public Cell myBottomCell = null;
    public Cell myLeftCell = null;

    public CellType myCellType = CellType.NONE;

    public bool myIsLastRoom = false;

    public GameObject[] myLRPrefab = null;
    public GameObject[] myLRDPrefab = null;
    public GameObject[] myLRUPrefab = null;
    public GameObject[] myLRDUPrefab = null;
    public GameObject[] myLPrefab = null;
    public GameObject[] myLDPrefab = null;
    public GameObject[] myLUPrefab = null;
    public GameObject[] myLDUPrefab = null;
    public GameObject[] myRPrefab = null;
    public GameObject[] myRDPrefab = null;
    public GameObject[] myRUPrefab = null;
    public GameObject[] myRDUPrefab = null;
    public GameObject[] myDPrefab = null;
    public GameObject[] myUPrefab = null;
    public GameObject[] myDUPrefab = null;

    private GameObject myCurrentTile = null;

    [SerializeField]
    private Transform[] myReplacement = null;

    private List<int> myLevel = null;

    [SerializeField]
    private GameObject mySolidCellPrefab = null;
    [SerializeField]
    private GameObject myEntryCellPrefab = null;
    [SerializeField]
    private GameObject myExitCellPrefab = null;

    string debug = "";

    private GameObject myStartingPoint = null;
    private GameObject myEndingPoint = null;

    public void PropagateLinkToMainPath(Cell aComeFrom)
    {
        if (myTopCell != null && !myTopCell.myIsMainPath && myTopCell != aComeFrom)
        {
            bool wasLinked = myTopCell.myIsLinkedToMainPath;
            myTopCell.myIsLinkedToMainPath = true;
            myTopCell.myCellType = Cell.CellType.BONUS;
            //myTopCell.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            if (!wasLinked)
                myTopCell.PropagateLinkToMainPath(this);
        }
        if (myRightCell != null && !myRightCell.myIsMainPath && myRightCell != aComeFrom)
        {
            bool wasLinked = myRightCell.myIsLinkedToMainPath;
            myRightCell.myIsLinkedToMainPath = true;
            myRightCell.myCellType = Cell.CellType.BONUS;
            //myRightCell.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            if (!wasLinked)
                myRightCell.PropagateLinkToMainPath(this);
        }
        if (myBottomCell != null && !myBottomCell.myIsMainPath && myBottomCell != aComeFrom)
        {
            bool wasLinked = myBottomCell.myIsLinkedToMainPath;
            myBottomCell.myIsLinkedToMainPath = true;
            myBottomCell.myCellType = Cell.CellType.BONUS;
            //myBottomCell.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            if (!wasLinked)
                myBottomCell.PropagateLinkToMainPath(this);
        }
        if (myLeftCell != null && !myLeftCell.myIsMainPath && myLeftCell != aComeFrom)
        {
            bool wasLinked = myLeftCell.myIsLinkedToMainPath;
            myLeftCell.myIsLinkedToMainPath = true;
            myLeftCell.myCellType = Cell.CellType.BONUS;
            //myLeftCell.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            if (!wasLinked)
                myLeftCell.PropagateLinkToMainPath(this);
        }
    }

    public void GenerateTiles()
    {
        List<List<int>> levels = null;
        

        if (myLeftCell != null)
        {
            if (myRightCell != null)
            {
                if(myTopCell == null && myBottomCell == null)
                {
                    debug = "LR";
                    levels = LevelLoader.GetInstance().GetLevels("LR");
                    //myCurrentTile = Instantiate(myLRPrefab[Random.Range(0, myLRPrefab.Length)], transform);
                }
                else if (myTopCell == null && myBottomCell != null)
                {
                    debug = "LRD";
                    //myCurrentTile = Instantiate(myLRDPrefab[Random.Range(0, myLRDPrefab.Length)], transform);
                }
                else if (myTopCell != null && myBottomCell == null)
                {
                    debug = "LRU";
                    //myCurrentTile = Instantiate(myLRUPrefab[Random.Range(0, myLRUPrefab.Length)], transform);
                }
                else if (myTopCell != null && myBottomCell != null)
                {
                    debug = "LRDU";
                    //myCurrentTile = Instantiate(myLRDUPrefab[Random.Range(0, myLRDUPrefab.Length)], transform);
                }
            }
            else
            {
                if (myTopCell == null && myBottomCell == null)
                {
                    debug = "L";
                    levels = LevelLoader.GetInstance().GetLevels("L");
                    //myCurrentTile = Instantiate(myLPrefab[Random.Range(0, myLPrefab.Length)], transform);
                }
                else if (myTopCell == null && myBottomCell != null)
                {
                    debug = "LD";
                    //myCurrentTile = Instantiate(myLDPrefab[Random.Range(0, myLDPrefab.Length)], transform);
                }
                else if (myTopCell != null && myBottomCell == null)
                {
                    debug = "LU";
                    //myCurrentTile = Instantiate(myLUPrefab[Random.Range(0, myLUPrefab.Length)], transform);
                }
                else if (myTopCell != null && myBottomCell != null)
                {
                    debug = "LDU";
                    //myCurrentTile = Instantiate(myLDUPrefab[Random.Range(0, myLDUPrefab.Length)], transform);
                }
            }
        }
        else if (myRightCell != null)
        {
            if (myTopCell == null && myBottomCell == null)
            {
                debug = "R";
                //myCurrentTile = Instantiate(myRPrefab[Random.Range(0, myRPrefab.Length)], transform);
            }
            else if (myTopCell == null && myBottomCell != null)
            {
                debug = "RD";
                //myCurrentTile = Instantiate(myRDPrefab[Random.Range(0, myRDPrefab.Length)], transform);
            }
            else if (myTopCell != null && myBottomCell == null)
            {
                debug = "RU";
                //myCurrentTile = Instantiate(myRUPrefab[Random.Range(0, myRUPrefab.Length)], transform);
            }
            else if (myTopCell != null && myBottomCell != null)
            {
                debug = "RDU";
                //myCurrentTile = Instantiate(myRDUPrefab[Random.Range(0, myRDUPrefab.Length)], transform);
            }
        }
        else
        {
            if (myTopCell == null && myBottomCell != null)
            {
                debug = "D";
                //myCurrentTile = Instantiate(myDPrefab[Random.Range(0, myDPrefab.Length)], transform);
            }
            else if (myTopCell != null && myBottomCell == null)
            {
                debug = "U";
                //myCurrentTile = Instantiate(myUPrefab[Random.Range(0, myUPrefab.Length)], transform);
            }
            else if (myTopCell != null && myBottomCell != null)
            {
                debug = "UD";
                //myCurrentTile = Instantiate(myDUPrefab[Random.Range(0, myDUPrefab.Length)], transform);
            }
        }

        Debug.Log("debug:" + debug);

        if (debug == "")
            return;

        levels = LevelLoader.GetInstance().GetLevels(debug);
        if(levels == null)
        {
            Debug.Log("ERROR : No levels found");
        }
        myLevel = levels[Random.Range(0, levels.Count)];

        /*if (myCurrentTile == null)
        {
            Debug.Log("ERROR");
            return;
        }
        else */
        if (myLevel == null)
        {
            Debug.Log("ERROR");
            return;
        }


        Debug.Log("found");

        ReplaceTiles();
    }

    private void ReplaceTiles()
    {
        int x = 0;
        int y = 0;
        for(int i = 0; i < myLevel.Count; i++)
        {
            if (myLevel[i] == 1 || (myLevel[i] == 2 && Random.Range(0f, 1f) <= 0.5f))
            {
                Transform go = Instantiate(mySolidCellPrefab, transform).transform;
                go.localPosition = new Vector3(x, y, 0);
            }
            else if (myLevel[i] == 3)
            {
                Transform go = Instantiate(myEntryCellPrefab, transform).transform;
                go.localPosition = new Vector3(x, y, 0);
                myStartingPoint = go.gameObject;
                myStartingPoint.SetActive(false);
            }
            else if (myLevel[i] == 4)
            {
                Transform go = Instantiate(myExitCellPrefab, transform).transform;
                go.localPosition = new Vector3(x, y, 0);
                myEndingPoint = go.gameObject;
                myEndingPoint.SetActive(false);
            }
            x++;
            if(x >= 10)
            {
                x = 0;
                y--;
            }
        }

        /*for (int i = 0; i < myCurrentTile.transform.childCount; i++)
        {
            if(myCurrentTile.transform.GetChild(i).name.Contains("ToReplace"))
            {
                /*Vector3 pos = myCurrentTile.transform.GetChild(i).position;
                Destroy(myCurrentTile.transform.GetChild(i).gameObject);
                /*if(myReplacement != null)
                {
                    Transform raplacement = Instantiate(myReplacement[Random.Range(0, myReplacement.Length)], myCurrentTile.transform);
                    raplacement.position = pos;
                }
            }
        }*/
    }

    public GameObject GetTile()
    {
        return myCurrentTile;
    }

    public GameObject GetStartingPoint()
    {
        return myStartingPoint;
    }

    public GameObject GetEndingPoint()
    {
        return myEndingPoint;
    }
}
