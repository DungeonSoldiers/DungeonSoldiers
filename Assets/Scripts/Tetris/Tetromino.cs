using UnityEngine;
using UnityEngine.Tilemaps;

// Lista enumerada com todas as figuras dos "Tetrominoes"
public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

// Mostra a estrutura no Unity
[System.Serializable]
// Estrutura que cont�m data acerca dos "Tetrominoes"
public struct TetrominoData
{
    // Vari�vel com uma das figuras
    public Tetromino tetromino;
    // Vari�vel com o "Tile" a utilizar (Depende da figura)
    public Tile tile;
    // Lista utilizada para guardar as posi��es das c�lulas dos "Tetrominoes"
    public Vector2Int[] cells { get; private set; }
    // Lista bidimensional utilizada para guardar informa��o acerca dos "wall kicks" dos "Tetrominoes"
    public Vector2Int[,] wallKicks { get; private set; }

    // Fun��o inicial
    public void Initialize()
    {
        // Obt�m as posi��es de todas as c�lulas de uma figura no "script" Data.cs
        cells = Data.Cells[tetromino];
        // Obt�m todos os "wall kicks" de uma figura
        wallKicks = Data.WallKicks[tetromino];
    }
}