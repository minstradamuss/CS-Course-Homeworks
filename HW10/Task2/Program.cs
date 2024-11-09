namespace SheepAndWolfGame
{
    public class Game
    {
        private const int N = 10;
        private char[,] field = new char[N, N];
        private List<Sheep> sheeps = new List<Sheep>();
        private Wolf wolf;
        private Random random = new Random();

        public Game()
        {
            wolf = new Wolf(this);
            sheeps.Add(new Sheep(this));
            sheeps.Add(new Sheep(this));
            sheeps.Add(new Sheep(this));

            Thread wolfThread = new Thread(wolf.Move);
            wolfThread.Start();

            foreach (var sheep in sheeps)
            {
                Thread sheepThread = new Thread(sheep.Move);
                sheepThread.Start();
            }
        }

        public void UpdateField()
        {
            lock (field)
            {
                Array.Clear(field, 0, field.Length);
                field[wolf.X, wolf.Y] = 'W';

                foreach (var sheep in sheeps)
                {
                    if (sheep.IsAlive)
                    {
                        field[sheep.X, sheep.Y] = 'S';
                    }
                }

                PrintField();
            }
        }

        private void PrintField()
        {
            Console.Clear();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(field[i, j] == '\0' ? '.' : field[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void CheckCollision()
        {
            lock (sheeps)
            {
                foreach (var sheep in sheeps)
                {
                    if (sheep.X == wolf.X && sheep.Y == wolf.Y)
                    {
                        sheep.IsAlive = false;
                        Console.WriteLine("Баран съеден!");
                    }
                }

                for (int i = 0; i < sheeps.Count; i++)
                {
                    for (int j = i + 1; j < sheeps.Count; j++)
                    {
                        if (sheeps[i].X == sheeps[j].X && sheeps[i].Y == sheeps[j].Y)
                        {
                            sheeps.Add(new Sheep(this));
                            Console.WriteLine("Появился новый баран!");
                        }
                    }
                }
            }
        }

        public (int, int) GetNewPosition(int x, int y)
        {
            return (Math.Clamp(x, 0, N - 1), Math.Clamp(y, 0, N - 1));
        }
    }

    public class Wolf
    {
        private Game game;
        private Random random = new Random();
        public int X { get; private set; }
        public int Y { get; private set; }

        public Wolf(Game game)
        {
            this.game = game;
            (X, Y) = (random.Next(10), random.Next(10));
        }

        public void Move()
        {
            while (true)
            {
                Thread.Sleep(random.Next(500, 1000));
                var (dx, dy) = GetRandomDirection();
                (X, Y) = game.GetNewPosition(X + dx, Y + dy);

                game.CheckCollision();
                game.UpdateField();
            }
        }

        private (int, int) GetRandomDirection()
        {
            var directions = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
            return directions[random.Next(directions.Length)];
        }
    }

    public class Sheep
    {
        private Game game;
        private Random random = new Random();
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsAlive { get; set; } = true;

        public Sheep(Game game)
        {
            this.game = game;
            (X, Y) = (random.Next(10), random.Next(10));
        }

        public void Move()
        {
            while (IsAlive)
            {
                Thread.Sleep(random.Next(500, 1500));
                var (dx, dy) = GetRandomDirection();
                (X, Y) = game.GetNewPosition(X + dx, Y + dy);

                game.CheckCollision();
                game.UpdateField();
            }
        }

        private (int, int) GetRandomDirection()
        {
            var directions = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };
            return directions[random.Next(directions.Length)];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Console.ReadKey();
        }
    }
}
