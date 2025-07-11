using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public Canvas canvas;
    [SerializeField] private CellsSpawnLogic cellsSpawnLogic;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject tapaMicro;

    public int cellCount { get; set; }
    public int maxNCell { get; set; }

    private bool gameEnded = false;

    public void Awake()
    {
        instance = this;

        Cursor.visible = false;
        gameEnded = false;

        StartGame();
    }

    public void StartGame()
    {
        cellsSpawnLogic.ClearCellsField();
        cellsSpawnLogic.GenerateCellsField();
    }

    public void FinishGame()
    {
        player.GetComponent<PlayerLogic>().enabled = false;

        canvas.transform.Find("LevelEnd").gameObject.SetActive(true);

        TMP_Text nCells = canvas.transform.Find("LevelEnd").Find("AmountCells").GetComponent<TMP_Text>();

        nCells.text = cellCount + " / " + maxNCell;

        gameEnded = true;

    }

    private void UpdateUICells()
    {
        TMP_Text nCells = canvas.transform.Find("NumberCells").GetComponent<TMP_Text>();

        nCells.text = "Counted Cells: " + cellCount;
    }

    public void MicroscopicAnimation()
    {
        player.GetComponent<PlayerLogic>().enabled = false;

        tapaMicro.GetComponent<Animator>().Play("ZoomAnimation");
    }

    public void FinishMicroscopicAnimation()
    {
        player.GetComponent<PlayerLogic>().enabled = true;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (gameEnded && Input.GetKeyDown(KeyCode.X))
        {
            gameEnded = false;
            SceneManager.LoadScene("MainScene");
        }
    }


    void OnEnable()
    {
        PlayerLogic.OnCellChanged += UpdateUICells;
    }

    void OnDisable()
    {
        PlayerLogic.OnCellChanged -= UpdateUICells;
    }
}
