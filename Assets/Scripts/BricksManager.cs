using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksManager : MonoBehaviour
{
    #region Singleton

    private static BricksManager _instance;
    public static BricksManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public Sprite[] bricksAmarillo;
    public Sprite[] bricksAzulclaro;
    public Sprite[] bricksCristal;
    public Sprite[] bricksAnil;
    public Sprite[] bricksAzul;
    public Sprite[] bricksNaranja;
    public Sprite[] bricksNegro;
    public Sprite[] bricksRojo;
    public Sprite[] bricksOliva;
    public Sprite[] bricksRosa;
    public Sprite[] bricksVerde;
    [SerializeField]
    private Brick brickPrefab;

    private int maxRows = 10;
    private int maxCols = 5;
    public List<int[,]> levelsData { get; set; }

    public List<Brick> remainingBricks { get; set; }

    private GameObject bricksContainer;

    private float initSpawnPosX = -1.6f;
    private float initSpawnPosY = 3f;
    private float brickShiftX = 0.8f;
    private float brickShiftY = 0.25f;


    private void Start()
    {
        bricksContainer = new GameObject("--- BRICKS ---");
        levelsData = LoadLevels();
        GenerateBricks();
    }

    private void GenerateBricks()
    {
        remainingBricks = new List<Brick>();
        int[,] currentLevelData = levelsData[GameManager.Instance.currentLevel];
        float currentSpawnX = initSpawnPosX;
        float currentSpawnY = initSpawnPosY;
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                int brickType = currentLevelData[row, col];
                if (brickType != 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f), Quaternion.identity) as Brick;
                    newBrick.transform.SetParent(bricksContainer.transform);
                    newBrick.hitsToDestroy = brickType;
                    
                    remainingBricks.Add(newBrick);
                }

                currentSpawnX += brickShiftX;
                if(col == maxCols - 1)
                    currentSpawnX = initSpawnPosX;
            }

            currentSpawnY -= brickShiftY;
        }
    }

    public Sprite GetSprite(string color, int hits)
    {
        switch(color)
        {
            case "amarillo":
                return bricksAmarillo[hits - 1];
            case "azulclaro":
                return bricksAzulclaro[hits - 1];
            case "cristal":
                return bricksCristal[hits - 1];
            case "anil":
                return bricksAnil[hits - 1];
            case "azul":
                return bricksAzul[hits - 1];
            case "naranja":
                return bricksNaranja[hits - 1];
            case "negro":
                return bricksNegro[hits - 1];
            case "rojo":
                return bricksRojo[hits - 1];
            case "oliva":
                return bricksOliva[hits - 1];
            case "rosa":
                return bricksRosa[hits - 1];
            case "verde":
                return bricksVerde[hits - 1];
            default:
                return null;
        }
    }

    public string GenerateColor()
    {
        string[] colorsS = { "amarillo", "azulclaro", "cristal","anil", "azul", "naranja", "negro", "rojo", "oliva", "rosa", "verde" };
        int index = UnityEngine.Random.Range(0, colorsS.Length);
        return colorsS[index];
    }

    private List<int[,]> LoadLevels()
    {
        List<int[,]> levelsData = new List<int[,]>();
        levelsData.Add(null);
        string[] rows;
        int[,] currentLevel;
        int currentRow;
        string[] bricks;
        foreach (TextAsset level in Resources.LoadAll("Levels", typeof(TextAsset)))
        {
            rows = level.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            currentLevel = new int[maxRows, maxCols];
            currentRow = 0;
            for(int row = 0; row < rows.Length; row++)
            {
                bricks = rows[row].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for(int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }

                currentRow++;
            }

            int levelIndex = int.Parse(level.name);
            levelsData.Insert(levelIndex, currentLevel);
        }

        return levelsData;
    }

    public void LoadLevel()
    {
        ClearRemainingBricks();
        GenerateBricks();
    }

    private void ClearRemainingBricks()
    {
        foreach (Brick brick in remainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }
}
