using Cinemachine;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private int zoomLevel = 0; // 0 = nivel de zoom mas alto

    public delegate void NumberCells();
    public static event NumberCells OnCellChanged;


    [Header("Controles")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;

    [SerializeField] private KeyCode zoom;
    [SerializeField] private KeyCode finishGame;


    void MoveMicroscopic()
    {
        float xAxis = 0;
        float zAxis = 0;

        if (Input.GetKey(up))
            zAxis = 1 * playerSpeed * 0.01f;
        else if (Input.GetKey(down))
            zAxis = -1 * playerSpeed * 0.01f;
        else if (Input.GetKey(right))
            xAxis = 1 * playerSpeed * 0.01f;
        else if (Input.GetKey(left))
            xAxis = -1 * playerSpeed * 0.01f;

        Vector3 newPosition = new Vector3 (xAxis, 0, zAxis);

        virtualCamera.transform.position += newPosition;
    }

    void CountCells()
    {
        if(Input.GetMouseButtonDown(0))
            GameManager.instance.cellCount++;

        if (Input.GetMouseButtonDown(1))
            GameManager.instance.cellCount--;

        OnCellChanged?.Invoke();
    }

    void ChangeZoom()
    {
        if(Input.GetKeyDown(zoom))
        {
            zoomLevel++;
            if (zoomLevel > 2)
                zoomLevel = 0;

            GameManager.instance.MicroscopicAnimation();
        }
    }

    public void CameraNewPosition()
    {  
        virtualCamera.enabled = false;

        if(zoomLevel == 0)
            virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, 60, virtualCamera.transform.position.z);
        else if (zoomLevel == 1)
            virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, 40, virtualCamera.transform.position.z);
        else if (zoomLevel == 2)
            virtualCamera.transform.position = new Vector3(virtualCamera.transform.position.x, 25, virtualCamera.transform.position.z);

        virtualCamera.enabled = true;

    }

    void FinishCount()
    {
        if(Input.GetKeyDown(finishGame))
            GameManager.instance.FinishGame();
    }


    private void Update()
    {
        CountCells();

        ChangeZoom();

        FinishCount();
    }


    void FixedUpdate()
    {
        MoveMicroscopic();
    }
}
