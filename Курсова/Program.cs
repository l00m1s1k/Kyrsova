using System;

class Maze
{
    private const int MAX_ROWS = 5;
    private const int MAX_COLS = 10;

    private char[,] maze;
    private Tuple<int, int> start;
    private Tuple<int, int> end;
    private Tuple<int, int> currentPosition;

    public Maze()
    {
        maze = new char[MAX_ROWS, MAX_COLS];
    }

    public void Create()
    {
        // Ініціалізація лабіринту
        for (int i = 0; i < MAX_ROWS; ++i)
        {
            for (int j = 0; j < MAX_COLS; ++j)
            {
                // Заповнення лабіринту "стінами" та порожніми клітинками
                if (new Random().Next(10) < 3)
                {
                    maze[i, j] = '#';
                }
                else
                {
                    maze[i, j] = ' ';
                }
            }
        }

        start = Tuple.Create(0, 0);  // Встановлення початкової позиції гравця
        end = Tuple.Create(MAX_ROWS - 1, MAX_COLS - 1);  // Встановлення кінцевої позиції гравця
        maze[start.Item1, start.Item2] = 'A';  // Позначення початкової позиції на лабіринті
        maze[end.Item1, end.Item2] = 'X';  // Позначення кінцевої позиції на лабіринті

        currentPosition = start;  // Початкова позиція гравця
    }

    public void Print()
    {
        Console.Clear();  // Очищення консольного вікна
        for (int i = 0; i < MAX_ROWS; ++i)
        {
            for (int j = 0; j < MAX_COLS; ++j)
            {
                Console.Write(maze[i, j] + " ");  // Виведення символів лабіринту
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool Move(string direction)
    {
        Tuple<int, int> newPosition = currentPosition;

        // Логіка переміщення гравця в залежності від напрямку
        if (direction == "UP" && currentPosition.Item1 > 0 && maze[currentPosition.Item1 - 1, currentPosition.Item2] != '#')
        {
            newPosition = Tuple.Create(currentPosition.Item1 - 1, currentPosition.Item2);
        }
        else if (direction == "DOWN" && currentPosition.Item1 < MAX_ROWS - 1 && maze[currentPosition.Item1 + 1, currentPosition.Item2] != '#')
        {
            newPosition = Tuple.Create(currentPosition.Item1 + 1, currentPosition.Item2);
        }
        else if (direction == "LEFT" && currentPosition.Item2 > 0 && maze[currentPosition.Item1, currentPosition.Item2 - 1] != '#')
        {
            newPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 - 1);
        }
        else if (direction == "RIGHT" && currentPosition.Item2 < MAX_COLS - 1 && maze[currentPosition.Item1, currentPosition.Item2 + 1] != '#')
        {
            newPosition = Tuple.Create(currentPosition.Item1, currentPosition.Item2 + 1);
        }

        if (!newPosition.Equals(currentPosition))
        {
            maze[currentPosition.Item1, currentPosition.Item2] = ' ';  // Залишити попередню позначку
            currentPosition = newPosition;  // Оновлення позиції гравця
            maze[currentPosition.Item1, currentPosition.Item2] = 'A';  // Позначення нової позиції гравця
            return true;  // Повідомлення про успішний хід
        }

        return false;  // Повідомлення про невдалий хід
    }

    public bool IsGameOver()
    {
        return currentPosition.Equals(end);  // Перевірка, чи гравець досяг кінцевої позиції
    }
}

class Player
{
    private Maze gameMaze;

    public Player(Maze maze)
    {
        gameMaze = maze;
    }

    public void MakeMove(string direction)
    {
        gameMaze.Move(direction);  // Виклик методу переміщення гравця в лабіринті
    }
}

class Program
{
    static void Main()
    {
        Maze gameMaze = new Maze();  // Створення об'єкту лабіринту
        Player

 player = new Player(gameMaze);  // Створення об'єкту гравця, якому передається лабіринт

        gameMaze.Create();  // Ініціалізація лабіринту

        // Головний цикл гри
        while (!gameMaze.IsGameOver())
        {
            gameMaze.Print();  // Виведення поточного стану лабіринту

            Console.Write("Choose your direction: (UP/DOWN/LEFT/RIGHT): ");
            string userInput = Console.ReadLine();

            if (userInput == "QUIT")
            {
                break;  // Вихід з циклу при введенні "QUIT"
            }

            player.MakeMove(userInput);  // Здійснення ходу гравця згідно з введеним напрямком
        }

        // Виведення повідомлення про завершення гри
        if (gameMaze.IsGameOver())
        {
            Console.WriteLine("Congratulations! You've reached your destination!");
        }
    }
}