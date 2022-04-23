using UnityEngine;

public class Piece : MonoBehaviour
{
    // Variável que contêm a tábua
    public Board board { get; private set; }
    // Variável com a data relacionada com os "Tetrominoes"
    public TetrominoData data { get; private set; }
    // Variável com a posição da peça
    public Vector3Int Position { get; private set; }

    // Lista com as células do "Tetromino"
    public Vector3Int[] cells { get; private set; }

    // Obtêm a rotação do "Tetromino"
    public int rotationIndex { get; private set; }

    // Tempo de espera para cada "step"
    public float stepDelay = 1f;
    // Tempo de espera para cada "lock"
    public float lockDelay = 0.5f;

    // Variável que conta o tempo entre cada "step"
    private float stepTime;
    // Variável que deteta quando é para fazer o "lock"
    private float lockTime;

    // Função inicial 
    public void Initialize(Board board, Vector3Int Position, TetrominoData data)
    {
        // Aplica os argumentos da função nas variáveis do "script"
        this.board = board;
        this.data = data;
        this.Position = Position;

        // Predefine o valor de 0 no índice de rotação
        rotationIndex = 0;

        // Define o próximo tempo para aplicar o "Step"
        stepTime = Time.time + stepDelay;
        // Muda a variável "lockTime" para 0
        lockTime = 0f;

        // Verifica se a variável está nula
        if (cells == null)
            /* Caso esteja, irá defenir a variável como uma lista
             * com o tamanho defenido na data acerca dos "Tetrominoes" */
            cells = new Vector3Int[data.cells.Length];

        // Utiliza um ciclo "for" para obter a posição de cada célula da peça
        for (int i = 0; i < data.cells.Length; i++)
            // Copia o valor na data do "Tetromino" para a nova variável
            cells[i] = (Vector3Int)data.cells[i];
    }

    // A função é chamada a cada frame
    private void Update()
    {
        // Limpa a peça da tábua
        board.Clear(this);

        // Adiciona o tempo delta atual à variável "lockTime"
        lockTime += Time.deltaTime;

        // Caso detete que a letra Q seja pressionada                                                   //||
        if (Input.GetKeyDown(KeyCode.Q))                                                                //||\
            // Rotação para a esquerda                                                                  //||-\
            Rotate(-1);                                                                                 //||--\ As rotações presentes entre a direita e esquerda
        // Caso detete que a letra E seja pressionada                                                   //||--/ somente rodam a peça 90º
        else if (Input.GetKeyDown(KeyCode.E))                                                           //||-/
            // Rotação para a direita                                                                   //||/
            Rotate(1);                                                                                  //||

        // Caso detete que a letra A seja pressionada
        if (Input.GetKeyDown(KeyCode.A))
            // Move a peça para a esquerda
            Move(Vector2Int.left);
        // Caso detete que a letra D seja pressionada
        else if (Input.GetKeyDown(KeyCode.D))
            // Move a peça para a direita
            Move(Vector2Int.right);
        
        // Caso detete que a letra S seja pressionada
        if (Input.GetKeyDown(KeyCode.S))
            // Move a peça para baixo
            Move(Vector2Int.down);

        // Caso detete que a barra de espaço seja pressionada
        if (Input.GetKeyDown(KeyCode.Space))
            // Move a peça para o fundo da tábua que esteja vazio
            HardDrop();

        // Verifica se é tempo para aplicar o "Step"
        if (Time.time >= stepTime)
            // Caso seja, então este será aplicado
            Step();

        // Insere a peça na tábua
        board.Set(this);
    }

    // Função utilizada para descer automaticamente o "Tetromino" a cada x segundos
    private void Step()
    {
        // Obtêm o novo tempo para aplicar o "Step"
        stepTime = Time.time + stepDelay;
        // Desce a peça
        Move(Vector2Int.down);

        // Verifica se é para aplicar o "Lock" a peça
        if (lockTime >= lockDelay)
            // Caso seja, então esta será aplicada
            Lock();
    }

    // Função para mover a peça para o fundo da tábua que esteja vazio
    private void HardDrop()
    {
        // Utiliza um ciclo "while" para verificar se é possível mover a peça para baixo
        while (Move(Vector2Int.down))
            continue;

        // Aplica o "Lock" à peça
        Lock();
    }

    // Função utilizada para trocar de peça
    private void Lock()
    {
        // Insere a peça na tábua
        board.Set(this);
        // Limpa as linhas cheias caso exista alguma
        board.ClearLines();
        // Insere uma nova peça ativa
        board.SpawnPiece();
    }

    // Função utilizada para mover o "Tetromino"
    private bool Move(Vector2Int translation)
    {
        // Obtêm a posição da peça
        Vector3Int newPosition = Position;
        // Adiciona um valor ao eixo X
        newPosition.x += translation.x;
        // Adiciona um valor ao eixo Y
        newPosition.y += translation.y;

        // Verifica se a posição é válida
        bool valid = board.IsValidPosition(this, newPosition);

        if (valid)
        {
            // Caso a posição seja válida, o "Tetromino" será movido para a nova posição
            Position = newPosition;
            // O tempo para fazer um "Lock" é reiniciado
            lockTime = 0f;
        }

        // Retorna se a posição era válida ou não
        return valid;
    }

    // Função utilizada para rodar o "Tetromino"
    private void Rotate(int direction)
    {
        // Grava o valor do index original
        int originalRotation = rotationIndex;
        // Atualiza o index com o valor original e uma adição do total da função "Wrap"
        rotationIndex += Wrap(rotationIndex + direction, 0, 4);

        // Aplica a rotação
        ApplyRotationMatrix(direction);

        // Verifica se a rotação não é válida
        if (!TestWallKicks(rotationIndex, direction))
        {
            // Caso não seja, reverte o index
            rotationIndex = originalRotation;
            // Reverte a rotação
            ApplyRotationMatrix(-direction);
        }
    }

    // Função utilizada para aplicar a rotação ao "Tetromino"
    private void ApplyRotationMatrix(int direction)
    {
        // Utiliza um ciclo "for" para aplicar a nova posição a cada célula
        for (int i = 0; i < cells.Length; i++)
        {
            /* Será utilizada um "Vector3" em vez de um "Vector3Int"
             * por causa da possibilidade da implementação de alguns valores do tipo "float" */
            Vector3 cell = cells[i];

            // Cria variáveis para as novas coordenadas
            int x, y;

            /* Iremos utilizar uma instrução "Switch" para podermos aplicar
             * lógica diferente aos "Tetrominoes" que não utilizam as mesmas rotações */
            switch (data.tetromino)
            {
                // Caso a figura seja ou I ou O, iremos aplicar rotações diferentes
                case Tetromino.I:
                case Tetromino.O:
                    // Iremos aplicar um deslocamento negativo de 0.5 em ambos os eixos
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    // Faz os cálculos para obter os novos eixos X e Y
                    // Para arredondar os valores, vamos utilizar a função Mathf.CeilToInt
                    break;
                // Caso seja outra figura, irá aplicar a lógica seguinte:
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    // Faz os cálculos para obter os novos eixos X e Y
                    /* Para arredondar os valores, vamos utilizar Mathf.RoundToInt para que,
                     * dependendo do resultado, este seja arredondado para baixo ou para cima */
                    break;
            }

            // Aplica a nova posição à célula
            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    // Testa os "Wall Kicks" do "Tetromino"
    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        // Obtêm o indíce do "Wall kick"
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        // Utiliza um ciclo "for" para aplicar todos os testes
        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            // Obtêm a posição definida na variável "i" no indíce de "Wall kicks" específico para o "Tetromino"
            Vector2Int translation = data.wallKicks[wallKickIndex, i];

            // Verifica se a posição é válida
            if (Move(translation))
                // Caso seja, retorna verdadeiro
                return true;
        }

        // Caso contrário, a função retornará falso
        return false;
    }

    // Obtêm o índice dos "Wall kicks"
    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        // Multiplica o índice de rotação por 2
        int wallKickIndex = rotationIndex * 2;

        // Verifica se a rotação é negativa
        if (rotationDirection < 0)
            // Caso esta seja verdade, o índice dos "Wall kicks" será reduzido por um valor
            wallKickIndex--;

        // Retorna o índice dos "Wall kicks" após de ter aplicado a função "Wrap"
        return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
    }

    /* Função matemática utilizada para verificar se o "input"
     * não sai fora das bordas definidas nas variáveis "min" e "max" */
    private int Wrap(int input, int min, int max)
    {
        // Deteta se o "input" é menor que o mínimo
        if (input < min)
            // Caso seja, a função irá retornar o valor máximo com uma subtração de 1
            return max - (min - input) % (max - min);
        else
            // Caso contrário, irá retornar um valor entre o mínimo e o máximo com uma subtração de 1
            return min + (input - min) % (max - min);
    }
}