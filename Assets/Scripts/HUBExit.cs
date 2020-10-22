using UnityEngine;
using UnityEngine.SceneManagement;

public class HUBExit : MonoBehaviour
{
    private bool myCanExit = false;
    [SerializeField]
    private GameObject myCanvas = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MovePlayer>())
        {
            myCanExit = true;
            myCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MovePlayer>())
        {
            myCanExit = false;
            myCanvas.SetActive(false);
        }
    }

    private void Update()
    {
        if(myCanExit && Input.GetKeyDown(PlayerInput.myActionKey))
        {
            SceneManager.LoadScene(2);
        }
    }
}
