using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    // Variável utilizada para conectar ao "Tilemap"
    public Tilemap tilemap { get; private set; }
    // Variável utilizada para obter a peça que está a ser controlada pelo jogador
    public Piece activePiece { get; private set; }
    // Lista que contêm data sobre "Tetrominoes" (Forma geométrica constituída por 4 quadradros de forma ortogonalmente)
    public TetrominoData[] tetrominoes;
    // Variável que possuirá a posição para gerar um "Tetromino"
    public Vector3Int spawnPosition;
    // Variável com o tamanho da tábua
    public Vector2Int boardSize = new Vector2Int(10, 20);

    // Calcula os limites da tábua
    public RectInt Bounds
    {
        get
        {
            // Obtêm a posição da tábua e aplica um deslocamento de metade
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            // Retorna a posição com o deslocamento e o tamanho da tábua
            return new RectInt(position, boardSize);
        }
    }

    // Esta função é chamada quando o jogo começa
    private void Awake()
    {
        // Obtêm o componente "Tilemap" dentro do objeto onde o "Script" está localizado
        tilemap = GetComponentInChildren<Tilemap>();
        // Obtêm o componente "Tilemap" dentro do objeto onde o "Script" está localizado
        activePiece = GetComponentInChildren<Piece>();

        // Utiliza um ciclo "for" para utilizar a função "Initialize" em todos os "Tetrominoes"
        for (int i = 0; i < tetrominoes.Length; i++)
            // Utiliza a função "Initialize" que forma as figuras dos "Tetrominoes"
            tetrominoes[i].Initialize();
    }

    // A função é chamada antes da atualização do primeiro frame
    private void Start()
    {
        // Executa a função para gerar um "Tetromino"
        SpawnPiece();
    }

    // Função utilizada para gerar um "Tetromino"
    public void SpawnPiece()
    {
        // Obtêm um número aleatório na lista de "Tetrominoes"
        int random = Random.Range(0, tetrominoes.Length);
        // Obtêm a peça aleatória
        TetrominoData data = tetrominoes[random];

        // Executa a função "Initialize" localizada no "Script" Piece.cs
        activePiece.Initialize(this, spawnPosition, data);

        // Verifica se a posição do "Tetromino" é válida
        if (IsValidPosition(activePiece, spawnPosition))
            // Se esta for valida então o "Tetromino" será inserido no "Tilemap"
            Set(activePiece);
        else
            // Caso contrário, o jogo será reiniciado
            GameOver();
    }

    // Função utilizada para reiniciar o jogo
    private void GameOver()
    {
        // Limpa a tábua
        tilemap.ClearAllTiles();
    }

    // Função utilizada para inserir as peças do "Tetromino" no "Tilemap"
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obtêm a posição da peça
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            // Insere a peça no "Tilemap"
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    // Função utilizada para limpar uma peça do "Tilemap"
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obtêm a posição da peça
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            // Retira a peça do "Tilemap"
            tilemap.SetTile(tilePosition, null);
        }
    }

    // Verifica se a nova posição da peça é válida
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        // Obtêm os limites da tábua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para verificar a posição de cada quadrado do "Tetromino"
        for (int i = 0; i < piece.cells.Length; i++)
        {
            // Obtêm a nova posição
            Vector3Int tilePosition = piece.cells[i] + position;

            // Verifica se a peça está fora dos limites da tábua ou se está a sobrepor outra
            if (!Bounds.Contains((Vector2Int)tilePosition) || tilemap.HasTile(tilePosition))
                // Caso alguma destes testes seja verdadeiro, a função retornará falsa
                return false;
        }

        // Caso ambos os casos sejam falsos, então a função retornará verdadeiro
        return true;
    }

    // Função utilizada para limpar as linhas que estão cheias
    public void ClearLines()
    {
        // Obtêm os limites da tábua
        RectInt bounds = Bounds;
        // Associa a última linha com a variável
        int row = bounds.yMin;

        // Utilizamos um ciclo "while" para verificar-mos todas as linhas de baixo para cima
        while (row < bounds.yMax)
        {
            // Verifica se a linha está cheia
            if (IsLineFull(row))
                // Se estiver cheia, a linha será limpa
                LineClear(row);
                /* Quando a linha for limpa, não iremos aumentar o valor da variável "row"
                 * porque a função de limpar move as linhas acima para baixo */
            else
                // Avança para a próxima linha
                row++;
        }
    }

    // Verifica se uma linha está cheia
    private bool IsLineFull(int row)
    {
        // Obtêm os limites da tábua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para verificar todas as colunas
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            // Obtêm a posição da coluna (x = Coluna, y = Linha)
            Vector3Int position = new Vector3Int(col, row, 0);

            // Verifica se não existe um quadrado na posição
            if (!tilemap.HasTile(position))
                // Se não existir, a função retornará falsa
                return false;
        }

        // Caso a linha esteja cheia, a função retornará verdadeira
        return true;
    }

    // Função utilizada para limpar a linha e mover as linhas acima para baixo
    private void LineClear(int row)
    {
        // Obtêm os limites da tábua
        RectInt bounds = Bounds;

        // Utiliza um ciclo "for" para limpar todas as colunas de uma linha específica
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            // Obtêm a posição da coluna (x = Coluna, y = Linha)
            Vector3Int position = new Vector3Int(col, row, 0);
            // Retira a coluna do "Tilemap" 
            tilemap.SetTile(position, null);
        }

        // Utilizamos um ciclo "while" para verificar-mos todas as linhas de baixo para cima
        while (row < bounds.yMax)
        {
            // Utiliza um ciclo "for" para mover todas as colunas de uma linha específica
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                // Obtêm a posição da coluna da linha acima (x = Coluna, y = Linha + 1)
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                // Obtêm o quadrado na linha acima
                TileBase above = tilemap.GetTile(position);
                // Obtêm a posição da coluna (x = Coluna, y = Linha)
                position = new Vector3Int(col, row, 0);
                // Desce o quadrado para a linha abaixo
                tilemap.SetTile(position, above);
            }

            // Avança para a próxima linha
            row++;
        }
    }
}