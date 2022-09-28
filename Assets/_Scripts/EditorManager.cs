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
    bool instatiated = false;

    public GameObject prefab1;
    public GameObject prefab2;

    GameObject item;

    private void OnEnable()
    {
        inputAction.Player.Enable();
    }

    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        inputAction = new PlayerAction();

        mainCam.enabled = true;
        editorCam.enabled = false;

        inputAction.Editor.EnableEditor.performed += cntxt => SwitchCamera();
        inputAction.Editor.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor.AddItem2.performed += cntxt => AddItem(2);

        inputAction.Editor.DropItem.performed += cntxt => DropItem();
    }

    private void AddItem(int itemID)
    {
        if (!editorMode || instatiated)
            return;

        switch (itemID)
        {
            case 1:
                item = Instantiate(prefab1);
                break;
            case 2:
                item = Instantiate(prefab1);
                break;
            default:
                break;
        }

    }

    private void DropItem()
    {

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
    }
}
