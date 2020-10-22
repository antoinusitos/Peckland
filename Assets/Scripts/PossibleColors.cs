using UnityEngine;

public class PossibleColors : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer = null;

    [SerializeField]
    private Color[] myAlternativeColor = null;

    private const float myReplaceChange = 0.3f;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if(Random.Range(0f, 1f) <= myReplaceChange)
        {
            mySpriteRenderer.color = myAlternativeColor[Random.Range(0, myAlternativeColor.Length)];
        }
    }
}
