using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SNAKE_GAME
{
    public class GameState
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool IsOver { get; private set; }


        private readonly LinkedList<Direction> directions = new LinkedList<Direction>();

        private readonly LinkedList<Position> snake = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[Rows, Columns];
            Direction = Direction.Right;
            AddSnake();
            AddFood();

        }
        private void AddSnake()
        {
            int r = Rows / 2;



            for (int c = 0; c <= 3; c++)

            {
                snake.AddFirst(new Position(r, c));
                Grid[r, c] = GridValue.Snake;
            }
        }
        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0)
            {

                return;
            }
            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;

        }
        public Position HeadPosition()
        {
            return snake.First.Value;
        }

        public Position TailPosition()
        {
            return snake.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snake;
        }

        private void AddHead(Position position)
        {
            snake.AddFirst(position);
            Grid[position.Row, position.Column] = GridValue.Snake;
        }
        private void RemoveTail()
        {
            Position tail = snake.Last.Value;
            snake.RemoveLast();
            Grid[tail.Row, tail.Column] = GridValue.Empty;
        }

        private Direction GetDirection()
        {
            if (directions.Count == 0)
            {
                return Direction;
            }
          
            return directions.Last.Value;
        }
        public void ChangeDirection(Direction direction)
        {
            if (CanChangeDirection(direction))
            {
                Direction = direction;
            }
            directions.AddLast(direction);

        }

        private bool CanChangeDirection(Direction direction)
        {
            if (directions.Count == 0)
            {
                return false;
            }
            Direction direction1 = directions.Last.Value;
            return direction != direction1 && direction != direction.Opposite();
        }


        private bool OutsideGrid(Position position)
        {
            return position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns;
        }
        private GridValue WillHit(Position position)
        {
            if (OutsideGrid(position))
            {
                return GridValue.Outside;
            }

            if (position == TailPosition())
            {
                return GridValue.Empty;
            }

            return Grid[position.Row, position.Column];
        }
        public void Move()
        {
            if (directions.Count > 0)
            {
                Direction = directions.First.Value;
                directions.RemoveFirst();
            }



            Position position = HeadPosition().OffsetBy(Direction);
            GridValue hit = WillHit(position);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                IsOver = true;
            }
            else
            {
                if (hit == GridValue.Food)
                {
                    Score++;
                    AddFood();
                    AddHead(position);
                }
                else if (hit == GridValue.Empty)
                {
                    RemoveTail();
                    AddHead(position);
                }

            }


        }
    }
}
