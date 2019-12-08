using PonyWebChallenge.Helper;
using PonyWebChallenge.MazeGeneration;
using System;
using System.Collections.Generic;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Difficulties
{
    public static class DifficultiesBasedOnRandom
    {
        public static readonly int NORTH_MOVEMENT = 0;
        public static readonly int SOUTH_MOVEMENT = 1;
        public static readonly int EAST_MOVEMENT = 2;
        public static readonly int WEST_MOVEMENT = 3;
        public static readonly int STAY_MOVEMENT = 4;

        //probability range between 0.0 and 1.0
        public static int GetRandomMovement(double probability)
        {
            Random random = new Random();
            if (random.NextDouble() < probability)
                return STAY_MOVEMENT;

            return random.Next(NORTH_MOVEMENT, STAY_MOVEMENT);
        }

        public static int GetRandomLegalMovement(Maze maze)
        {
            List<int> legalMoves = new List<int>();

            Random random = new Random();
            int DomokunId = maze.GetDomokunId();
            int mazeWidth = maze.GetMazeWidth();
            int mazeHeight = maze.GetMazeHeight();
            CellState[] cells = maze.GetCells();

            if (!MazeHelper.CheckIfUpperBorder(mazeWidth, DomokunId) && !cells[DomokunId].HasFlag(CellState.North))
            {
                legalMoves.Add(NORTH_MOVEMENT);
            }
            if (!MazeHelper.CheckIfRightBorder(mazeWidth, DomokunId) && !cells[DomokunId].HasFlag(CellState.West))
            {
                legalMoves.Add(WEST_MOVEMENT);
            }
            if (!MazeHelper.CheckIfLowerBorder(mazeWidth, mazeHeight, DomokunId) && !cells[DomokunId + mazeWidth].HasFlag(CellState.North))
            {
                legalMoves.Add(SOUTH_MOVEMENT);
            }
            if (!MazeHelper.CheckIfLeftBorder(mazeWidth, DomokunId) && !cells[DomokunId + 1].HasFlag(CellState.West))
            {
                legalMoves.Add(EAST_MOVEMENT);
            }

            return legalMoves[random.Next(legalMoves.Count)];
        }
    }
}