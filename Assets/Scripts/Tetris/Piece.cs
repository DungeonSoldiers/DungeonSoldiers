using UnityEngine;

public class Piece : MonoBehaviour
{
    // Vari�vel que cont�m a t�bua
    public Board board { get; private set; }
    // Vari�vel com a data relacionada com os "Tetrominoes"
    public TetrominoData data { get; private set; }
    // Vari�vel com a posi��o da pe�a
    public Vector3Int Position { get; private set; }

    // Lista com as c�lulas do "Tetromino"
    public Vector3Int[] cells { get; private set; }

    // Obt�m a rota��o do "Tetromino"
    public int rotationIndex { get; private set; }

    // Tempo de espera para cada "step"
    public float stepDelay = 1f;
    // Tempo de espera para cada "lock"
    public float lockDelay = 0.5f;

    // Vari�vel que conta o tempo entre cada "step"
    private float stepTime;
    // Vari�vel que deteta quando � para fazer o "lock"
    private float lockTime;

    // Fun��o inicial 
    public void Initialize(Board board, Vector3Int Position, TetrominoData data)
    {
        // Aplica os argumentos da fun��o nas vari�veis do "script"
        this.board = board;
        this.data = data;
        this.Position = Position;

        // Predefine o valor de 0 no �ndice de rota��o
        rotationIndex = 0;

        // Define o pr�ximo tempo para aplicar o "Step"
        stepTime = Time.time + stepDelay;
        // Muda a vari�vel "lockTime" para 0
        lockTime = 0f;

        // Verifica se a vari�vel est� nula
        if (cells == null)
            /* Caso esteja, ir� defenir a vari�vel como uma lista
             * com o tamanho defenido na data acerca dos "Tetrominoes" */
            cells = new Vector3Int[data.cells.Length];

        // Utiliza um ciclo "for" para obter a posi��o de cada c�lula da pe�a
        for (int i = 0; i < data.cells.Length; i++)
            // Copia o valor na data do "Tetromino" para a nova vari�vel
            cells[i] = (Vector3Int)data.cells[i];
    }

    // A fun��o � chamada a cada frame
    private void Update()
    {
        // Limpa a pe�a da t�bua
        board.Clear(this);

        // Adiciona o tempo delta atual � vari�vel "lockTime"
        lockTime += Time.deltaTime;

        // Caso detete que a letra Q seja pressionada                                                   //||
        if (Input.GetKeyDown(KeyCode.Q))                                                                //||\
            // Rota��o para a esquerda                                                                  //||-\
            Rotate(-1);                                                                                 //||--\ As rota��es presentes entre a direita e esquerda
        // Caso detete que a letra E seja pressionada                                                   //||--/ somente rodam a pe�a 90�
        else if (Input.GetKeyDown(KeyCode.E))                                                           //||-/
            // Rota��o para a direita                                                                   //||/
            Rotate(1);                                                                                  //||

        // Caso detete que a letra A seja pressionada
        if (Input.GetKeyDown(KeyCode.A))
            // Move a pe�a para a esquerda
            Move(Vector2Int.left);
        // Caso detete que a letra D seja pressionada
        else if (Input.GetKeyDown(KeyCode.D))
            // Move a pe�a para a direita
            Move(Vector2Int.right);
        
        // Caso detete que a letra S seja pressionada
        if (Input.GetKeyDown(KeyCode.S))
            // Move a pe�a para baixo
            Move(Vector2Int.down);

        // Caso detete que a barra de espa�o seja pressionada
        if (Input.GetKeyDown(KeyCode.Space))
            // Move a pe�a para o fundo da t�bua que esteja vazio
            HardDrop();

        // Verifica se � tempo para aplicar o "Step"
        if (Time.time >= stepTime)
            // Caso seja, ent�o este ser� aplicado
            Step();

        // Insere a pe�a na t�bua
        board.Set(this);
    }

    // Fun��o utilizada para descer automaticamente o "Tetromino" a cada x segundos
    private void Step()
    {
        // Obt�m o novo tempo para aplicar o "Step"
        stepTime = Time.time + stepDelay;
        // Desce a pe�a
        Move(Vector2Int.down);

        // Verifica se � para aplicar o "Lock" a pe�a
        if (lockTime >= lockDelay)
            // Caso seja, ent�o esta ser� aplicada
            Lock();
    }

    // Fun��o para mover a pe�a para o fundo da t�bua que esteja vazio
    private void HardDrop()
    {
        // Utiliza um ciclo "while" para verificar se � poss�vel mover a pe�a para baixo
        while (Move(Vector2Int.down))
            continue;

        // Aplica o "Lock" � pe�a
        Lock();
    }

    // Fun��o utilizada para trocar de pe�a
    private void Lock()
    {
        // Insere a pe�a na t�bua
        board.Set(this);
        // Limpa as linhas cheias caso exista alguma
        board.ClearLines();
        // Insere uma nova pe�a ativa
        board.SpawnPiece();
    }

    // Fun��o utilizada para mover o "Tetromino"
    private bool Move(Vector2Int translation)
    {
        // Obt�m a posi��o da pe�a
        Vector3Int newPosition = Position;
        // Adiciona um valor ao eixo X
        newPosition.x += translation.x;
        // Adiciona um valor ao eixo Y
        newPosition.y += translation.y;

        // Verifica se a posi��o � v�lida
        bool valid = board.IsValidPosition(this, newPosition);

        if (valid)
        {
            // Caso a posi��o seja v�lida, o "Tetromino" ser� movido para a nova posi��o
            Position = newPosition;
            // O tempo para fazer um "Lock" � reiniciado
            lockTime = 0f;
        }

        // Retorna se a posi��o era v�lida ou n�o
        return valid;
    }

    // Fun��o utilizada para rodar o "Tetromino"
    private void Rotate(int direction)
    {
        // Grava o valor do index original
        int originalRotation = rotationIndex;
        // Atualiza o index com o valor original e uma adi��o do total da fun��o "Wrap"
        rotationIndex += Wrap(rotationIndex + direction, 0, 4);

        // Aplica a rota��o
        ApplyRotationMatrix(direction);

        // Verifica se a rota��o n�o � v�lida
        if (!TestWallKicks(rotationIndex, direction))
        {
            // Caso n�o seja, reverte o index
            rotationIndex = originalRotation;
            // Reverte a rota��o
            ApplyRotationMatrix(-direction);
        }
    }

    // Fun��o utilizada para aplicar a rota��o ao "Tetromino"
    private void ApplyRotationMatrix(int direction)
    {
        // Utiliza um ciclo "for" para aplicar a nova posi��o a cada c�lula
        for (int i = 0; i < cells.Length; i++)
        {
            /* Ser� utilizada um "Vector3" em vez de um "Vector3Int"
             * por causa da possibilidade da implementa��o de alguns valores do tipo "float" */
            Vector3 cell = cells[i];

            // Cria vari�veis para as novas coordenadas
            int x, y;

            /* Iremos utilizar uma instru��o "Switch" para podermos aplicar
             * l�gica diferente aos "Tetrominoes" que n�o utilizam as mesmas rota��es */
            switch (data.tetromino)
            {
                // Caso a figura seja ou I ou O, iremos aplicar rota��es diferentes
                case Tetromino.I:
                case Tetromino.O:
                    // Iremos aplicar um deslocamento negativo de 0.5 em ambos os eixos
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    // Faz os c�lculos para obter os novos eixos X e Y
                    // Para arredondar os valores, vamos utilizar a fun��o Mathf.CeilToInt
                    break;
                // Caso seja outra figura, ir� aplicar a l�gica seguinte:
                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
                    // Faz os c�lculos para obter os novos eixos X e Y
                    /* Para arredondar os valores, vamos utilizar Mathf.RoundToInt para que,
                     * dependendo do resultado, este seja arredondado para baixo ou para cima */
                    break;
            }

            // Aplica a nova posi��o � c�lula
            cells[i] = new Vector3Int(x, y, 0);
        }
    }

    // Testa os "Wall Kicks" do "Tetromino"
    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        // Obt�m o ind�ce do "Wall kick"
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);

        // Utiliza um ciclo "for" para aplicar todos os testes
        for (int i = 0; i < data.wallKicks.GetLength(1); i++)
        {
            // Obt�m a posi��o definida na vari�vel "i" no ind�ce de "Wall kicks" espec�fico para o "Tetromino"
            Vector2Int translation = data.wallKicks[wallKickIndex, i];

            // Verifica se a posi��o � v�lida
            if (Move(translation))
                // Caso seja, retorna verdadeiro
                return true;
        }

        // Caso contr�rio, a fun��o retornar� falso
        return false;
    }

    // Obt�m o �ndice dos "Wall kicks"
    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        // Multiplica o �ndice de rota��o por 2
        int wallKickIndex = rotationIndex * 2;

        // Verifica se a rota��o � negativa
        if (rotationDirection < 0)
            // Caso esta seja verdade, o �ndice dos "Wall kicks" ser� reduzido por um valor
            wallKickIndex--;

        // Retorna o �ndice dos "Wall kicks" ap�s de ter aplicado a fun��o "Wrap"
        return Wrap(wallKickIndex, 0, data.wallKicks.GetLength(0));
    }

    /* Fun��o matem�tica utilizada para verificar se o "input"
     * n�o sai fora das bordas definidas nas vari�veis "min" e "max" */
    private int Wrap(int input, int min, int max)
    {
        // Deteta se o "input" � menor que o m�nimo
        if (input < min)
            // Caso seja, a fun��o ir� retornar o valor m�ximo com uma subtra��o de 1
            return max - (min - input) % (max - min);
        else
            // Caso contr�rio, ir� retornar um valor entre o m�nimo e o m�ximo com uma subtra��o de 1
            return min + (input - min) % (max - min);
    }
}