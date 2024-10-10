using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    // List of blueprints to be randomly selected in level generation
    public List<LevelPiece> levelPrefabs = new List<LevelPiece>();
    //Starting point on first level generation
    public Transform levelStartPoint;
    //Blue prints arranged into current level design
    public List<LevelPiece> pieces = new List<LevelPiece>();

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GenerateInitialPieces();
    }

    public void GenerateInitialPieces()
    {
        for (int i = 0; i < 2; i++)
        {
            AddPiece();
        }
    }
    public void AddPiece()
    {
        //Create random value betweeen 0 and number of level blueprints
        int randomIndex = Random.Range(0, levelPrefabs.Count);

        LevelPiece piece = (LevelPiece)Instantiate(levelPrefabs[randomIndex]);
        piece.transform.SetParent(this.transform, false);

        Vector3 spawnPosition = Vector3.zero;

        if (pieces.Count == 0)
        {
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            spawnPosition = pieces[pieces.Count - 1].exitPoint.position;
        }
        piece.transform.position = spawnPosition;
        pieces.Add(piece);
    }

    public void RemoveOldestPiece()
    {
        LevelPiece oldestPiece = pieces[0];

        pieces.Remove(oldestPiece);
        Destroy(oldestPiece.gameObject);
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
