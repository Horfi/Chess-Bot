using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("ART")]
    [SerializeField] private Material tileMaterial;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    

    // decleration
    private const int TileCount_X = 8;
    private const int TileCount_Y = 8;
    private GameObject[,] tiles;
    private Vector3 bounds;


    private void Awake()
    {
        GenerateAllTiles(tileSize, TileCount_X, TileCount_Y);

    }

    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset+=transform.position.y;
        bounds = new Vector3((tileCountX/2)*tileSize,0,(tileCountY/2)*tileSize)+boardCenter;
        // deklaracja dlugosci x i y 2d array planszy szachowej | kazdy 1 element to 1 pole 
        tiles = new GameObject[tileCountX, tileCountY];  
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                tiles[x, y] = GenerateSingleTile(tileSize,x,y); 
    }

    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        GameObject tileObject = new GameObject(string.Format("X:{0},Y:{1}",x,y));
        //transform pola jest podpiety do transform calej planszy 
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(x*tileSize,yOffset,y*tileSize) - bounds;
        vertices[1] = new Vector3(x*tileSize,yOffset,(y+1)*tileSize) - bounds;
        vertices[2] = new Vector3((x+1)*tileSize,yOffset,y*tileSize)- bounds;
        vertices[3] = new Vector3((x+1)*tileSize,yOffset,(y+1)*tileSize) - bounds;

        int[] triangles = new int[]{3,2,1,0,1,2};

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        tileObject.AddComponent<BoxCollider>();

        return tileObject; 
    }
}



