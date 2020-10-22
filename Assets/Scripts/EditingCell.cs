using UnityEngine;

[ExecuteInEditMode]
public class EditingCell : MonoBehaviour
{
    public enum CellType
    {
        EMPTY,
        SOLID,
        POSSIBLESOLID,
        ENTRY,
        EXIT,
    }

    [SerializeField]
    private Color mySolidColor = new Color(161.0f / 255, 72.0f / 255, 24.0f / 255);
    [SerializeField]
    private Color myPossibleSolidColor = new Color(181.0f / 255, 132.0f / 255, 105.0f / 255);
    [SerializeField]
    private Color myEmptyColor = new Color(255, 255, 255, 20.0f / 255);
    [SerializeField]
    private Color myEntryColor = new Color(0, 255, 0);
    [SerializeField]
    private Color myExitColor = new Color(255, 0, 0);

    private SpriteRenderer mySpriteRenderer = null;

    [SerializeField]
    private CellType myCellType = CellType.SOLID;

    private void Awake()
    {
        if (mySpriteRenderer == null)
            mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (mySpriteRenderer == null)
            mySpriteRenderer = GetComponent<SpriteRenderer>();

        switch(myCellType)
        {
            case CellType.SOLID:
                {
                    mySpriteRenderer.color = mySolidColor;
                    break;
                }
            case CellType.EMPTY:
                {
                    mySpriteRenderer.color = myEmptyColor;
                    break;
                }
            case CellType.POSSIBLESOLID:
                {
                    mySpriteRenderer.color = myPossibleSolidColor;
                    break;
                }
            case CellType.ENTRY:
                {
                    mySpriteRenderer.color = myEntryColor;
                    break;
                }
            case CellType.EXIT:
                {
                    mySpriteRenderer.color = myExitColor;
                    break;
                }
        }
    }

    public int GetCellType()
    {
        return (int)myCellType;
    }
}
