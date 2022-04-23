using UnityEngine;
using UnityEngine.Tilemaps;

public class tetrisGhost : MonoBehaviour
{
    // Vari�vel com a telha fantasma
    public Tile tile;
    // Vari�vel utilizada como conex�o ao "script" Board.cs
    public Board board;
    // Vari�vel com a pe�a que o jogador est� a controlar
    public Piece trackingPiece;
    // Vari�vel com o "Tilemap"
    public Tilemap tilemap { get; private set; }
    // Lista utilizada para guardar as c�lulas do "Tetromino" fantasma
    public Vector3Int[] cells { get; private set; }
    // Vari�vel com o deslocamento a ser aplicado �s c�lulas do "Tetromino" fantasma
    public Vector3Int Position { get; private set; }

    // Esta fun��o � chamada quando o jogo come�a
    private void Awake()
    {
        // Obt�m o "Tilemap"
        tilemap = GetComponentInChildren<Tilemap>();
        // Aplica um tamanho � lista "cells"
        cells = new Vector3Int[4];
    }

    // Fun��o executada ap�s todas as fun��es Update()
    private void LateUpdate()
    {
        // Executa a fun��o para limpar o "Tilemap"
        Clear();
        // Executa a fun��o para copiar todas as c�lulas da pe�a que o jogador est� a controlar
        Copy();
        // Executa a fun��o para meter a pe�a fantasma no fundo da t�bua que esteja vazio
        Drop();
        // Executa a fun��o para inserir a pe�a fantasma no "Tilemap"
        Set();
    }

    // Fun��o para limpar o "Tilemap"
    private void Clear()
    {
        // Obt�m todas as c�lulas da pe�a
        for (int i = 0; i < cells.Length; i++)
        {
            // Obt�m a posi��o da c�lula
            Vector3Int tilePosition = cells[i] + Position;
            // Limpa a c�lula
            tilemap.SetTile(tilePosition, null);
        }
    }

    // Fun��o para copiar todas as c�lulas da pe�a que o jogador est� a controlar
    private void Copy()
    {
        // Obt�m todas as c�lulas da pe�a
        for (int i = 0; i < cells.Length; i++)
            // Copia as c�lulas da pe�a ativa
            cells[i] = trackingPiece.cells[i];
    }

    // Fun��o para meter a pe�a fantasma no fundo da t�bua que esteja vazio
    private void Drop()
    {
        // Obt�m a posi��o da pe�a ativa
        Vector3Int position = trackingPiece.Position;

        // Obt�m a linha onde a pe�a est� localizada
        int current = position.y;
        // Obt�m a �ltima linha
        int bottom = -board.boardSize.y / 2 - 1;

        // Retira a pe�a ativa da t�bua
        board.Clear(trackingPiece);

        // Utiliza um ciclo "for" para obter as linhas desde a linha da pe�a ativa at� � �ltima linha
        for (int row = current; row >= bottom; row--)
        {
            // Muda o eixo Y da vari�vel "position"
            position.y = row;

            // Verifica se a posi��o � v�lida
            if (board.IsValidPosition(trackingPiece, position))
                // Caso seja, aplica a nova posi��o � pe�a fantasma
                Position = position;
            else
                // Caso contr�rio, o ciclo � parado
                break;
        }

        // Volta a adicionar a pe�a ativa � t�bua
        board.Set(trackingPiece);
    }

    // Fun��o para inserir a pe�a fantasma no "Tilemap"
    private void Set()
    {
        // Obt�m todas as c�lulas da pe�a
        for (int i = 0; i < cells.Length; i++)
        {
            // Obt�m a posi��o da c�lula e aplica o deslocamento definido anteriormente
            Vector3Int tilePosition = cells[i] + Position;
            // Insere a c�lula no "Tilemap"
            tilemap.SetTile(tilePosition, tile);
        }
    }
}