using UnityEngine;

public class Generator : MonoBehaviour
{
    //spelunky : GridSize = 4x4
    //           RoomSize = 10x8 (WxH)

    private const int myGridSize = 4;
    private const float myChanceLinkBonusRoom = 0.35f;
    private const float myCellSeparationX = 10;
    private const float myCellSeparationY = 8;
    private int[] myGrid = null;

    [SerializeField]
    private Transform myTestSpritePrefab = null;
    private Cell[] myTransformGrid = null;
    [SerializeField]
    private Transform myLineRendererPrefab = null;
    private Cell myStartingCell = null;
    private Cell myEndingCell = null;
    [SerializeField]
    private GameObject myPlayerPrefab = null;
    private GameObject myPlayer = null;

    private void Awake()
    {
        GenerateLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            GenerateLevel();
    }

    public void GenerateLevel()
    {
        if(myTransformGrid != null)
        {
            for(int i = 0; i < myTransformGrid.Length; i++)
            {
                if(myTransformGrid[i] != null)
                    Destroy(myTransformGrid[i].gameObject);
            }
            myTransformGrid = null;
            myGrid = null;

            if (myPlayer != null)
                Destroy(myPlayer);
        }

        myGrid = new int[myGridSize * myGridSize];
        myTransformGrid = new Cell[myGrid.Length];

        Init();

        DrawMainPath();

        GenerateBonusRooms();

        RemoveNonLinkedRooms();

        GenerateRoomsTiles();

        //DebugPath();

        GameObject start = myStartingCell.GetStartingPoint();
        start.SetActive(true);
        Camera.main.transform.position = start.transform.position + Vector3.forward * -10;

        GameObject end = myEndingCell.GetEndingPoint();
        end.SetActive(true);

        myPlayer = Instantiate(myPlayerPrefab, start.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        myPlayer.name = "Player";
    }

    private void Init()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < myGrid.Length; i++)
        {
            myTransformGrid[i] = Instantiate(myTestSpritePrefab, new Vector3(x * myCellSeparationX, -y * myCellSeparationY, 0), Quaternion.identity).GetComponent<Cell>();
            myTransformGrid[i].myX = x;
            myTransformGrid[i].myY = y;
            x++;
            if (x % myGridSize == 0 && x != 0)
            {
                x = 0;
                y++;
            }
        }
    }

    private void DrawMainPath()
    {
        int x = Random.Range(0, myGridSize);
        int y = 0;

        int index = 0;

        myStartingCell = myTransformGrid[y * myGridSize + x];

        while (y < myGridSize)
        {
            myGrid[y * myGridSize + x] = 1;
            //myTransformGrid[y * myGridSize + x].GetComponentInChildren<SpriteRenderer>().color = Color.red;
            //myTransformGrid[y * myGridSize + x].GetComponentInChildren<TextMesh>().text = index.ToString();
            myTransformGrid[y * myGridSize + x].myIsMainPath = true;
            myTransformGrid[y * myGridSize + x].myCellType = Cell.CellType.MAINPATH;

            int dir = Random.Range(0, 3);
            //go left
            if (dir == 0 && x % myGridSize > 0 && myGrid[y * myGridSize + x - 1] != 1)
            {
                myTransformGrid[y * myGridSize + x].myLeftCell = myTransformGrid[y * myGridSize + x - 1];
                myTransformGrid[y * myGridSize + x - 1].myRightCell = myTransformGrid[y * myGridSize + x];
                //LineRenderer lr = myTransformGrid[y * myGridSize + x].gameObject.AddComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[]{ myTransformGrid[y * myGridSize + x].transform.position,myTransformGrid[y * myGridSize + x - 1].transform.position} );
                x--;
                index++;
            }
            //go right
            else if (dir == 1 && x % myGridSize < myGridSize - 1 && myGrid[y * myGridSize + x + 1] != 1)
            {
                myTransformGrid[y * myGridSize + x].myRightCell = myTransformGrid[y * myGridSize + x + 1];
                myTransformGrid[y * myGridSize + x + 1].myLeftCell = myTransformGrid[y * myGridSize + x];
                //LineRenderer lr = myTransformGrid[y * myGridSize + x].gameObject.AddComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[] { myTransformGrid[y * myGridSize + x].transform.position, myTransformGrid[y * myGridSize + x + 1].transform.position });
                x++;
                index++;
            }
            //go bottom
            else if (dir == 2)
            {
                if(y + 1 < myGridSize)
                {
                    myTransformGrid[y * myGridSize + x].myBottomCell = myTransformGrid[(y + 1) * myGridSize + x];
                    myTransformGrid[(y + 1) * myGridSize + x].myTopCell = myTransformGrid[y * myGridSize + x];
                    //LineRenderer lr = myTransformGrid[y * myGridSize + x].gameObject.AddComponent<LineRenderer>();
                    //lr.startWidth = 0.1f;
                    //lr.endWidth = 0.1f;
                    //lr.SetPositions(new Vector3[] { myTransformGrid[y * myGridSize + x].transform.position, myTransformGrid[(y + 1) * myGridSize + x].transform.position });
                }
                else
                {
                    myTransformGrid[y * myGridSize + x].myIsLastRoom = true;
                    myEndingCell = myTransformGrid[y * myGridSize + x];
                }
                y++;
                index++;
            }
        }
    }

    private void GenerateBonusRooms()
    {
        for (int i = 0; i < myGrid.Length; i++)
        {
            if (myTransformGrid[i].myIsMainPath)
                continue;

            //check left
            if(Random.Range(0f, 1f) <= myChanceLinkBonusRoom && myTransformGrid[i].myX > 0)
            {
                if(myTransformGrid[i - 1].myIsMainPath)
                {
                    myTransformGrid[i].myIsLinkedToMainPath = true;
                    myTransformGrid[i].myCellType = Cell.CellType.BONUS;
                }
                myTransformGrid[i].myLeftCell = myTransformGrid[i - 1];
                myTransformGrid[i - 1].myRightCell= myTransformGrid[i];
                //LineRenderer lr = Instantiate(myLineRendererPrefab, myTransformGrid[i].transform).gameObject.GetComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[] { myTransformGrid[i].transform.position, myTransformGrid[i - 1].transform.position });
            }
            //check right
            if (Random.Range(0f, 1f) <= myChanceLinkBonusRoom && myTransformGrid[i].myX < myGridSize - 1)
            {
                if (myTransformGrid[i + 1].myIsMainPath)
                {
                    myTransformGrid[i].myIsLinkedToMainPath = true;
                    myTransformGrid[i].myCellType = Cell.CellType.BONUS;
                }
                myTransformGrid[i].myRightCell = myTransformGrid[i + 1];
                myTransformGrid[i + 1].myLeftCell = myTransformGrid[i];
                //LineRenderer lr = Instantiate(myLineRendererPrefab, myTransformGrid[i].transform).gameObject.GetComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[] { myTransformGrid[i].transform.position, myTransformGrid[i + 1].transform.position });
            }
            //check Top
            if (Random.Range(0f, 1f) <= myChanceLinkBonusRoom && myTransformGrid[i].myY > 0)
            {
                if (myTransformGrid[i - myGridSize].myIsMainPath)
                {
                    myTransformGrid[i].myIsLinkedToMainPath = true;
                    myTransformGrid[i].myCellType = Cell.CellType.BONUS;
                }
                myTransformGrid[i].myTopCell = myTransformGrid[i - myGridSize];
                myTransformGrid[i - myGridSize].myBottomCell = myTransformGrid[i];
                //LineRenderer lr = Instantiate(myLineRendererPrefab, myTransformGrid[i].transform).gameObject.GetComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[] { myTransformGrid[i].transform.position, myTransformGrid[i - myGridSize].transform.position });
            }
            //check Bottom
            if (Random.Range(0f, 1f) <= myChanceLinkBonusRoom && myTransformGrid[i].myY < myGridSize - 1)
            {
                if (myTransformGrid[i + myGridSize].myIsMainPath)
                {
                    myTransformGrid[i].myIsLinkedToMainPath = true;
                    myTransformGrid[i].myCellType = Cell.CellType.BONUS;
                }
                myTransformGrid[i].myBottomCell = myTransformGrid[i + myGridSize];
                myTransformGrid[i + myGridSize].myTopCell = myTransformGrid[i];
                //LineRenderer lr = Instantiate(myLineRendererPrefab, myTransformGrid[i].transform).gameObject.GetComponent<LineRenderer>();
                //lr.startWidth = 0.1f;
                //lr.endWidth = 0.1f;
                //lr.SetPositions(new Vector3[] { myTransformGrid[i].transform.position, myTransformGrid[i + myGridSize].transform.position });
            }
        }

        for (int i = 0; i < myGrid.Length; i++)
        {
            if (myTransformGrid[i].myIsMainPath)
                continue;

            if (myTransformGrid[i].myIsLinkedToMainPath)
            {
                //myTransformGrid[i].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                myTransformGrid[i].PropagateLinkToMainPath(null);
            }
        }
    }

    private void RemoveNonLinkedRooms()
    {
        for (int i = 0; i < myGrid.Length; i++)
        {
            if (!myTransformGrid[i].myIsMainPath && !myTransformGrid[i].myIsLinkedToMainPath)
            {
                Destroy(myTransformGrid[i].gameObject);
            }
        }
    }

    private void GenerateRoomsTiles()
    {
        for (int i = 0; i < myTransformGrid.Length; i++)
        {
            if (myTransformGrid[i] != null)
            {
                myTransformGrid[i].GenerateTiles();
            }
        }
    }

    private void DebugPath()
    {
        string toPrint = "";
        for (int i = 0; i < myGrid.Length; i++)
        {
            if (i % myGridSize == 0 && i != 0)
            {
                Debug.Log(toPrint);
                toPrint = "";
            }

            toPrint += myGrid[i];
        }
    }
}
