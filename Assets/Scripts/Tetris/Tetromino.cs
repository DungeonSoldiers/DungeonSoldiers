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
// Estrutura que contêm data acerca dos "Tetrominoes"
public struct TetrominoData
{
    // Variável com uma das figuras
    public Tetromino tetromino;
    // Variável com o "Tile" a utilizar (Depende da figura)
    public Tile tile;
    // Lista utilizada para guardar as posições das células dos "Tetrominoes"
    public Vector2Int[] cells { get; private set; }
    // Lista bidimensional utilizada para guardar informação acerca dos "wall kicks" dos "Tetrominoes"
    public Vector2Int[,] wallKicks { get; private set; }

    // Função inicial
    public void Initialize()
    {
        // Obtêm as posições de todas as células de uma figura no "script" Data.cs
        cells = Data.Cells[tetromino];
        // Obtêm todos os "wall kicks" de uma figura
        wallKicks = Data.WallKicks[tetromino];
    }
}