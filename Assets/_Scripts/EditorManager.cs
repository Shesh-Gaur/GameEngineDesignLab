using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class EditorManager : MonoBehaviour
{
    public static EditorManager instance;

    PlayerAction inputAction;
    public Camera mainCam;
    public Camera editorCam;

    public bool editorMode = false;
    bool instantiated = false;

    Vector3 mousePos;

    Subject subject = new Subject();

    public GameObject prefab1;
    public GameObject prefab2;

    GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        inputAction = PlayerInputController.controller.inputAction;

        inputAction.Editor.EnableEditor.performed += cntxt => SwitchCamera();
        inputAction.Editor.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor.AddItem2.performed += cntxt => AddItem(2);

        inputAction.Editor.DropItem.performed += cntxt => DropItem();

        mainCam.enabled = true;
        editorCam.enabled = false;
    }

    private void AddItem(int itemID)
    {
        if (!editorMode || instantiated)
            return;

        switch (itemID)
        {
            case 1:
                item = Instantiate(prefab1);
                SpikeBall spike1 = new SpikeBall(item, new GreenMat());
                subject.AddObserver(spike1);
                break;
            case 2:
                item = Instantiate(prefab1);
                SpikeBall spike2 = new SpikeBall(item, new YellowMat());
                subject.AddObserver(spike2);
                break;
            default:
                break;
        }

        subject.Notify();
        instantiated = true;

    }

    private void DropItem()
    {
        if (editorMode && instantiated)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Collider>().enabled = true;

            instantiated = false;
        }
    }

    private void SwitchCamera()
    {
        editorMode = !editorMode;
        Debug.Log("Run!");
        mainCam.enabled = !mainCam.enabled;
        editorCam.enabled = !editorCam.enabled;     
    }

    // Update is called once per frame
    void Update()
    {
        if (editorMode)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }

        if (instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 10.0f);
            item.transform.position = editorCam.ScreenToWorldPoint(mousePos);
        }
    }
}
