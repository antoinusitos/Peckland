using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform myTransform = null;
    private Transform myPlayer = null;
    private float mySpeed = 5.0f;
    [SerializeField]
    private Vector3 myOffset = Vector3.forward * -10;

    private void Start()
    {
        myTransform = transform;
    }

    private void Update()
    {
        if (myPlayer == null)
        {
            myPlayer = GameObject.Find("Player")?.transform;
            return;
        }

        myTransform.position = Vector3.Lerp(myTransform.position, myPlayer.position + myOffset, Time.deltaTime * mySpeed);
    }
}
