using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    // Vari�vel utilizada para conectar ao "Tilemap"
    public Tilemap tilemap { get; private set; }
    // Vari�vel utilizada para obter a pe�a que est� a ser controlada pelo jogador
    public Piece activePiece { get; private set; }
    // Lista que cont�m data sobre "Tetrominoes" (Forma geom�trica constitu�da por 4 quadradros de forma ortogonalmente)
    public TetrominoData[] tetrominoes;
    // Vari�vel que possuir� a posi��o para gerar um "Tetromino"
    public Vector3Int spawnPosition;
    // Vari�vel com o tamanho da t�bua
    public Vector2Int boardSize = new Vector2Int(10, 20);

    // Calcula os limites da t�bua
    public RectInt Bounds
    {
        get
        {
            // Obt�m a posi��o da t�bua e aplica um deslocamento de metade
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            // Retorna a posi��o com o deslocamento e o tamanho da t�bua
            return new RectInt(position, boardSize);
        }
    }

    // Esta fun��o � chamada quando o jogo come�a
    private void Awake()
    {
        // Obt�m o componente "Tilemap" dentro do objeto onde o "Script" est� localizado
        tilemap = GetComponentInChildren<Tilemap>();
        // Obt�m o componente "Tilemap" dentro do objeto onde o "Script" est� localizado
        activePiece = GetComponentInChildren<Piece>();

        // Utiliza um ciclo "for" para utilizar a fun��o "Initialize" em todos os "Tetrominoes"
        for (int i = 0; i < tetrominoes.Length; i++)
            // Utiliza a fun��o "Initialize" que forma as figuras dos "Tetrominoes"
            tetrominoes[i].Initialize();
    }

    // A fun��o � chamada antes da atualiza��o do primeiro frame
    private void Start()
    {
        // Executa a fun��o para gerar um "Tetromino"
        SpawnPiece();
    }

    // Fun��o utilizada para gerar um "Tetromino"
    public void SpawnPiece()
    {
        // Obt�m um n�mero aleat�rio na lista de "Tetrominoes"
        int random = Random.Range(0, tetrominoes.Length);
        // Obt�m a pe�a aleat�ria
        TetrominoData data = tetrominoes[random];

        // Executa a fun��o "Initialize" localizada no "Script" Piece.cs
        activePiece.Initialize(this, spawnPosition, data);

        // Verifica se a posi��o do "Tetromino" � v�lida
        if (IsValidPosition(activePiece, spawnPosition))
            // Se esta for valida ent�o o "Tetromino" ser� inserido no "Tilemap"
            Set(activePiece);
        else
            // Caso contr�rio, o jogo ser� reiniciado
            GameOver();
    }

    // Fun��o utilizada para reiniciar o jogo
    private void GameOver()
    {
        // Limpa a t�bua
        tilemap.ClearAllTiles();
    }

    // Fun��o utilizada para inserir as pe�as do "Tetromino" no "Tilemap"
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obt�m a posi��o da pe�a
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            // Insere a pe�a no "Tilemap"
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // Fun��o utilizada para limpar uma pe�a do "Tilemap"
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obt�m a posi��o da pe�a
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            // Retira a pe�a do "Tilemap"
            tilemap.SetTile(tilePosition, null);
        }
    }

    // Verifica se a nova posi��o da pe�a � v�lida
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        // Obt�m os limites da t�bua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para verificar a posi��o de cada quadrado do "Tetromino"
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obt�m a nova posi��o
            Vector3Int tilePosition = piece.cells[i] + position;

            // Verifica se a pe�a est� fora dos limites da t�bua ou se est� a sobrepor outra
            if (!Bounds.Contains((Vector2Int)tilePosition) || tilemap.HasTile(tilePosition))
                // Caso alguma destes testes seja verdadeiro, a fun��o retornar� falsa
                return false;
        }

        // Caso ambos os casos sejam falsos, ent�o a fun��o retornar� verdadeiro
        return true;
    }

    // Fun��o utilizada para limpar as linhas que est�o cheias
    public void ClearLines()
    {
        // Obt�m os limites da t�bua
        RectInt bounds = Bounds;
        // Associa a �ltima linha com a vari�vel
        int row = bounds.yMin;

        // Utilizamos um ciclo "while" para verificar-mos todas as linhas de baixo para cima
        while (row < bounds.yMax)
        {
            // Verifica se a linha est� cheia
            if (IsLineFull(row))
                // Se estiver cheia, a linha ser� limpa
                LineClear(row);
                /* Quando a linha for limpa, n�o iremos aumentar o valor da vari�vel "row"
                 * porque a fun��o de limpar move as linhas acima para baixo */
            else
                // Avan�a para a pr�xima linha
                row++;
        }
    }

    // Verifica se uma linha est� cheia
    private bool IsLineFull(int row)
    {
        // Obt�m os limites da t�bua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para verificar todas as colunas
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            // Obt�m a posi��o da coluna (x = Coluna, y = Linha)
            Vector3Int position = new Vector3Int(col, row, 0);

            // Verifica se n�o existe um quadrado na posi��o
            if (!tilemap.HasTile(position))
                // Se n�o existir, a fun��o retornar� falsa
                return false;
        }

        // Caso a linha esteja cheia, a fun��o retornar� verdadeira
        return true;
    }

    // Fun��o utilizada para limpar a linha e mover as linhas acima para baixo
    private void LineClear(int row)
    {
        // Obt�m os limites da t�bua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para limpar todas as colunas de uma linha espec�fica
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            // Obt�m a posi��o da coluna (x = Coluna, y = Linha)
            Vector3Int position = new Vector3Int(col, row, 0);
            // Retira a coluna do "Tilemap" 
            tilemap.SetTile(position, null);
        }

        // Utilizamos um ciclo "while" para verificar-mos todas as linhas de baixo para cima
        while (row < bounds.yMax)
        {
            // Utiliza um ciclo "for" para mover todas as colunas de uma linha espec�fica
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                // Obt�m a posi��o da coluna da linha acima (x = Coluna, y = Linha + 1)
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                // Obt�m o quadrado na linha acima
                TileBase above = tilemap.GetTile(position);
                // Obt�m a posi��o da coluna (x = Coluna, y = Linha)
                position = new Vector3Int(col, row, 0);
                // Desce o quadrado para a linha abaixo
                tilemap.SetTile(position, above);
            }

            // Avan�a para a pr�xima linha
            row++;
        }
    }
}