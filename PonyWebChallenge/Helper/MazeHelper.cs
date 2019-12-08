using PonyWebChallenge.Extensions;
using PonyWebChallenge.MazeGeneration;
using PonyWebChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Helper
{
    public static class MazeHelper
    {
        public static readonly int NORTH_MOVEMENT = 0;
        public static readonly int SOUTH_MOVEMENT = 1;
        public static readonly int EAST_MOVEMENT = 2;
        public static readonly int WEST_MOVEMENT = 3;
        public static readonly int STAY_MOVEMENT = 4;

        private const string DIRECTION_STAY = "stay";
        private const string DIRECTION_NORTH = "north";
        private const string DIRECTION_SOUTH = "south";
        private const string DIRECTION_WEST = "west";
        private const string DIRECTION_EAST = "east";

        public static IEnumerable<RemoveWallAction> GetNeighbours(int xyPoint, int _width, int _height)
        {
            if (!CheckIfLeftBorder(_width, xyPoint)) yield return new RemoveWallAction { Neighbour = xyPoint - 1, Wall = CellState.West };
            if (!CheckIfUpperBorder(_width, xyPoint)) yield return new RemoveWallAction { Neighbour = xyPoint - _width, Wall = CellState.North };
            if (!CheckIfRightBorder(_width, xyPoint)) yield return new RemoveWallAction { Neighbour = xyPoint + 1, Wall = CellState.West };
            if (!CheckIfLowerBorder(_width, _height, xyPoint)) yield return new RemoveWallAction { Neighbour = xyPoint + _width, Wall = CellState.North };
        }

        public static void VisitCell(Maze maze, int xy, Random _rng)
        {
            int _width = maze.GetMazeWidth();
            int _height = maze.GetMazeHeight();
            CellState[] array = maze.GetCells();


            array[xy] |= CellState.Visited;
            foreach (var p in GetNeighbours(xy, _width, _height).Shuffle(_rng).Where(z => !(array[z.Neighbour].HasFlag(CellState.Visited))))
            {
                if (xy > p.Neighbour)
                {
                    array[xy] -= p.Wall;
                }
                else
                {
                    array[p.Neighbour] -= p.Wall;
                }

                VisitCell(maze, p.Neighbour, _rng);
            }
        }

        public static string PrintMazeAsHTML(Maze maze)
        {
            List<string> displayString = new List<string>();
            int _width = maze.GetMazeWidth();
            int _height = maze.GetMazeHeight();
            CellState[] array = maze.GetCells();

            var firstLine = string.Empty;
            for (var x = 0; x < _height; x++)
            {
                var sbTop = new StringBuilder();
                var sbMid = new StringBuilder();
                for (var y = 0; y < _width; y++)
                {
                    sbTop.Append(array[(x * _width) + y].HasFlag(CellState.North) ? HtmlHelper.GenerateTopBorder() : HtmlHelper.GenerateBlankTopBorder());
                    sbMid.Append(array[(x * _width) + y].HasFlag(CellState.West) ? HtmlHelper.GenerateWestBorder(array[(x * _width) + y]) :
                                                                                    HtmlHelper.GenerateBlankWestBorder(array[(x * _width) + y]));
                }
                if (firstLine == string.Empty)
                    firstLine = sbTop.ToString();
                displayString.Add(sbTop + HtmlHelper.GenerateInterection());
                displayString.Add(sbMid + HtmlHelper.GenerateBorder());
            }
            displayString.Add(firstLine);

            int counter = displayString.Count;
            string completeString = String.Empty;
            foreach (string dString in displayString)
            {
                if (counter == 1)
                {
                    completeString += dString + HtmlHelper.GenerateInterection() + HtmlHelper.GenerateBreak();
                }
                else
                {
                    completeString += dString + HtmlHelper.GenerateBreak();
                }
                counter--;
            }


            return completeString;
        }

        public static StateResult MoveCharacter(Maze maze, string direction, CellState character)
        {
            CellState[] mazeCells = maze.GetCells();
            int characterLocation;
            int mazeWidth = maze.GetMazeWidth();
            int mazeHeight = maze.GetMazeHeight();
            StateResult result = StateResult.MoveAccepted;

            CellState counterCharacter;
            if (character == CellState.Domokun)
            {
                characterLocation = maze.GetDomokunId();
                counterCharacter = CellState.Pony;
            }
            else
            {
                characterLocation = maze.GetPonyId();
                counterCharacter = CellState.Domokun;
            }

            switch (direction)
            {
                case DIRECTION_STAY:
                    result = StateResult.MoveAccepted;
                    break;
                case DIRECTION_NORTH:
                    if (mazeCells[characterLocation].HasFlag(CellState.North) ||
                        CheckIfUpperBorder(mazeWidth, characterLocation))
                    {
                        result = StateResult.MoveRejected;
                    }
                    else if (mazeCells[characterLocation - mazeWidth].HasFlag(counterCharacter))
                    {
                        result = StateResult.Lose;
                    }
                    else
                    {
                        if (mazeCells[characterLocation - mazeWidth].HasFlag(CellState.Endpoint))
                        {
                            if (character == CellState.Pony)
                                result = StateResult.EndGame;
                        }

                        mazeCells[characterLocation] -= character;
                        maze.SetCharacterId(character, characterLocation - mazeWidth);
                    }


                    break;
                case DIRECTION_SOUTH:
                    if (CheckIfLowerBorder(mazeWidth, mazeHeight, characterLocation) ||
                        mazeCells[characterLocation + mazeWidth].HasFlag(CellState.North))
                    {
                        result = StateResult.MoveRejected;
                    }
                    else if (mazeCells[characterLocation + mazeWidth].HasFlag(counterCharacter))
                    {
                        result = StateResult.Lose;
                    }
                    else
                    {
                        if (mazeCells[characterLocation + mazeWidth].HasFlag(CellState.Endpoint))
                        {
                            if (character == CellState.Pony)
                                result = StateResult.EndGame;
                        }

                        mazeCells[characterLocation] -= character;
                        maze.SetCharacterId(character, characterLocation + mazeWidth);
                    }
                    break;
                case DIRECTION_WEST:
                    if (mazeCells[characterLocation].HasFlag(CellState.West) ||
                        CheckIfLeftBorder(mazeWidth, characterLocation))
                    {
                        result = StateResult.MoveRejected;
                    }
                    else if (mazeCells[characterLocation - 1].HasFlag(counterCharacter))
                    {
                        result = StateResult.Lose;
                    }
                    else
                    {
                        if (mazeCells[characterLocation - 1].HasFlag(CellState.Endpoint))
                        {
                            if (character == CellState.Pony)
                                result = StateResult.EndGame;
                        }

                        mazeCells[characterLocation] -= character;
                        maze.SetCharacterId(character, characterLocation - 1);
                    }
                    break;
                case DIRECTION_EAST:
                    if (CheckIfRightBorder(mazeWidth, characterLocation) ||
                        mazeCells[characterLocation + 1].HasFlag(CellState.West))
                    {
                        result = StateResult.MoveRejected;
                    }
                    else if (mazeCells[characterLocation + 1].HasFlag(counterCharacter))
                    {
                        result = StateResult.Lose;
                    }
                    else
                    {
                        if (mazeCells[characterLocation + 1].HasFlag(CellState.Endpoint))
                        {
                            if (character == CellState.Pony)
                                result = StateResult.EndGame;
                        }

                        mazeCells[characterLocation] -= character;
                        maze.SetCharacterId(character, characterLocation + 1);
                    }
                    break;
                default:
                    result = StateResult.InvalidMove;
                    break;
            }

            return result;
        }

        public static void ClearMazeCharacters(Maze maze)
        {
            int _width = maze.GetMazeWidth();
            int _height = maze.GetMazeHeight();
            CellState[] array = maze.GetCells();

            for (var x = 0; x < _height; x++)
                for (var y = 0; y < _width; y++)
                {
                    if (array[(x * _width) + y].HasFlag(CellState.Domokun))
                    {
                        array[(x * _width) + y] -= CellState.Domokun;
                    }
                    if (array[(x * _width) + y].HasFlag(CellState.Pony))
                    {
                        array[(x * _width) + y] -= CellState.Pony;
                    }
                    if (array[(x * _width) + y].HasFlag(CellState.Endpoint))
                    {
                        array[(x * _width) + y] -= CellState.Endpoint;
                    }
                }

        }

        public static bool CheckIfUpperBorder(int mazeWidth, int location)
        {
            if (location < mazeWidth)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfLeftBorder(int mazeWidth, int location)
        {
            if (location % mazeWidth == 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfLowerBorder(int mazeWidth, int mazeHeight, int location)
        {
            if (location < mazeWidth * mazeHeight && location > (mazeHeight * mazeWidth) - mazeWidth - 1)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfRightBorder(int mazeWidth, int location)
        {
            if (location % mazeWidth == mazeWidth - 1)
            {
                return true;
            }
            return false;
        }
    }
}