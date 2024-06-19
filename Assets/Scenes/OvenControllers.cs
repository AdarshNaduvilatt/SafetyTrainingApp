using UnityEngine;
using UnityEngine.UI;

public class OvenControllers : MonoBehaviour
{
    public Animator ovenAnimator; 
    public Button startButton;
    public Button stopButton;
    public Button menuButton;
    public Button emergencyStopButton;
    public Button openButton;
    public Button closeButton;
    public Button placeButton; 
    public Button removeButton; 
    public Text timeDisplay;
    public Text warningMessage;
    public GameObject workpiece;

    private bool isRunning = false;
    private float hardeningTime = 10.0f;
    private float elapsedTime = 0.0f;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Add listeners for the new buttons
        placeButton.onClick.AddListener(PlaceWorkpiece);
        removeButton.onClick.AddListener(RemoveWorkpiece);

        // Add existing button listeners
        startButton.onClick.AddListener(StartOven);
        stopButton.onClick.AddListener(StopOven);
        menuButton.onClick.AddListener(OpenDoor);
        emergencyStopButton.onClick.AddListener(EmergencyStop);
        openButton.onClick.AddListener(OpenDoor);
        closeButton.onClick.AddListener(CloseDoor);

        warningMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!Cursor.visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            timeDisplay.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString() + "s";

            if (elapsedTime >= hardeningTime)
            {
                CompleteHardeningProcess();
            }
        }
    }

    public void StartOven()
    {
        if (!isRunning)
        {
            if (IsDoorClosed())
            {
                ovenAnimator.SetTrigger("OpenDoor");
                Debug.Log("OpenDoor trigger set in StartOven");
            }
            else
            {
                warningMessage.gameObject.SetActive(true);
            }
        }
    }

    public void PlaceWorkpiece()
    {
        if (IsDoorClosed())
        {
            ovenAnimator.SetTrigger("OpenDoor");
            Debug.Log("OpenDoor trigger set for placing workpiece in PlaceWorkpiece");
        }
        else
        {
            warningMessage.gameObject.SetActive(true);
        }
    }

    public void PlaceWorkpieceAnimation()
    {
        ovenAnimator.SetTrigger("PlaceWorkpiece");
        Debug.Log("PlaceWorkpiece trigger set in PlaceWorkpieceAnimation");
    }

    public void RemoveWorkpiece()
    {
        ovenAnimator.SetTrigger("OpenDoor");
        Debug.Log("OpenDoor trigger set for removing workpiece in RemoveWorkpiece");
        ovenAnimator.SetTrigger("RemoveWorkpiece");
        Debug.Log("RemoveWorkpiece trigger set in RemoveWorkpiece");
    }

    public void CloseDoor()
    {
        ovenAnimator.SetTrigger("CloseDoor");
        Debug.Log("CloseDoor trigger set in CloseDoor");
    }

    public void StartHardeningProcess()
    {
        isRunning = true;
        elapsedTime = 0.0f;
        Debug.Log("Hardening process started in StartHardeningProcess");
    }

    public void StopOven()
    {
        if (isRunning)
        {
            isRunning = false;
            elapsedTime = 0.0f;
            Debug.Log("Oven stopped in StopOven");
        }
    }

    public void CompleteHardeningProcess()
    {
        isRunning = false;
        ovenAnimator.SetTrigger("ChangeAppearance");
        Debug.Log("Hardening process completed in CompleteHardeningProcess");
    }

    public void EmergencyStop()
    {
        StopOven();
        warningMessage.gameObject.SetActive(false);
        Debug.Log("Emergency stop activated in EmergencyStop");
    }

    public void OpenDoor()
    {
        ovenAnimator.SetTrigger("OpenDoor");
        Debug.Log("OpenDoor trigger set in OpenDoor");
    }

    bool IsDoorClosed()
    {
        // Check if the door is in the closed state
        AnimatorStateInfo stateInfo = ovenAnimator.GetCurrentAnimatorStateInfo(0);
        bool isClosed = stateInfo.IsName("DoorClose");
        Debug.Log("IsDoorClosed: " + isClosed);
        return isClosed;
    }
}
