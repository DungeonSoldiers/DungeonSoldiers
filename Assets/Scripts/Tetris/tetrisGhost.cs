using UnityEngine;
using UnityEngine.Tilemaps;

public class tetrisGhost : MonoBehaviour
{
    // Variável com a telha fantasma
    public Tile tile;
    // Variável utilizada como conexão ao "script" Board.cs
    public Board board;
    // Variável com a peça que o jogador está a controlar
    public Piece trackingPiece;
    // Variável com o "Tilemap"
    public Tilemap tilemap { get; private set; }
    // Lista utilizada para guardar as células do "Tetromino" fantasma
    public Vector3Int[] cells { get; private set; }
    // Variável com o deslocamento a ser aplicado às células do "Tetromino" fantasma
    public Vector3Int Position { get; private set; }

    // Esta função é chamada quando o jogo começa
    private void Awake()
    {
        // Obtêm o "Tilemap"
        tilemap = GetComponentInChildren<Tilemap>();
        // Aplica um tamanho à lista "cells"
        cells = new Vector3Int[4];
    }

    // Função executada após todas as funções Update()
    private void LateUpdate()
    {
        // Executa a função para limpar o "Tilemap"
        Clear();
        // Executa a função para copiar todas as células da peça que o jogador está a controlar
        Copy();
        // Executa a função para meter a peça fantasma no fundo da tábua que esteja vazio
        Drop();
        // Executa a função para inserir a peça fantasma no "Tilemap"
        Set();
    }

    // Função para limpar o "Tilemap"
    private void Clear()
    {
        // Obtêm todas as células da peça
        for (int i = 0; i < cells.Length; i++)
        {
            // Obtêm a posição da célula
            Vector3Int tilePosition = cells[i] + Position;
            // Limpa a célula
            tilemap.SetTile(tilePosition, null);
        }
    }

    // Função para copiar todas as células da peça que o jogador está a controlar
    private void Copy()
    {
        // Obtêm todas as células da peça
        for (int i = 0; i < cells.Length; i++)
            // Copia as células da peça ativa
            cells[i] = trackingPiece.cells[i];
    }

    // Função para meter a peça fantasma no fundo da tábua que esteja vazio
    private void Drop()
    {
        // Obtêm a posição da peça ativa
        Vector3Int position = trackingPiece.Position;

        // Obtêm a linha onde a peça está localizada
        int current = position.y;
        // Obtêm a última linha
        int bottom = -board.boardSize.y / 2 - 1;

        // Retira a peça ativa da tábua
        board.Clear(trackingPiece);

        // Utiliza um ciclo "for" para obter as linhas desde a linha da peça ativa até à última linha
        for (int row = current; row >= bottom; row--)
        {
            // Muda o eixo Y da variável "position"
            position.y = row;

            // Verifica se a posição é válida
            if (board.IsValidPosition(trackingPiece, position))
                // Caso seja, aplica a nova posição à peça fantasma
                Position = position;
            else
                // Caso contrário, o ciclo é parado
                break;
        }

        // Volta a adicionar a peça ativa à tábua
        board.Set(trackingPiece);
    }

    // Função para inserir a peça fantasma no "Tilemap"
    private void Set()
    {
        // Obtêm todas as células da peça
        for (int i = 0; i < cells.Length; i++)
        {
            // Obtêm a posição da célula e aplica o deslocamento definido anteriormente
            Vector3Int tilePosition = cells[i] + Position;
            // Insere a célula no "Tilemap"
            tilemap.SetTile(tilePosition, tile);
        }
    }
}