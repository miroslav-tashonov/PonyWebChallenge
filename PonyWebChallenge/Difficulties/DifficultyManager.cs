using PonyWebChallenge.Helper;
using PonyWebChallenge.MazeGeneration;
using System;

namespace PonyWebChallenge.Difficulties
{
    public class DifficultyManager
    {
        public const int TOURIST_DIFFICULTY = 0;
        public const int NOVICE_DIFFICULTY = 1;
        public const int AVERAGE_DIFFICULTY = 2;
        public const int EXPERIENCED_DIFFICULTY = 3;
        public const int SKILLED_DIFFICULTY = 4;
        public const int ADEPT_DIFFICULTY = 5;
        public const int MASTERFUL_DIFFICULTY = 6;
        public const int INHUMAN_DIFFICULTY = 7;
        public const int NIGHTMARE_DIFFICULTY = 8;
        public const int MASOCHISTIC_DIFFICULTY = 9;
        public const int GODLIKE_DIFFICULTY = 10;

        public const int NORTH_MOVEMENT = 0;
        public const int SOUTH_MOVEMENT = 1;
        public const int EAST_MOVEMENT = 2;
        public const int WEST_MOVEMENT = 3;
        public const int STAY_MOVEMENT = 4;

        private const string DIRECTION_STAY = "stay";
        private const string DIRECTION_NORTH = "north";
        private const string DIRECTION_SOUTH = "south";
        private const string DIRECTION_WEST = "west";
        private const string DIRECTION_EAST = "east";

        public static string GetDomokunDirection(Maze maze)
        {
            int difficulty = maze.GetDifficulty();

            string direction = String.Empty;
            switch (difficulty)
            {
                case TOURIST_DIFFICULTY:
                    direction = GetTouristDifficultyMove();
                    break;
                case NOVICE_DIFFICULTY:
                    direction = GetNoviceDifficultyMove();
                    break;
                case AVERAGE_DIFFICULTY:
                    direction = GetAverageDifficultyMove();
                    break;
                case EXPERIENCED_DIFFICULTY:
                    direction = GetExperiencedDifficultyMove();
                    break;
                case SKILLED_DIFFICULTY:
                    direction = GetSkilledDifficultyMove();
                    break;
                case ADEPT_DIFFICULTY:
                    direction = GetAdeptDifficultyMove();
                    break;
                case MASTERFUL_DIFFICULTY:
                    direction = GetMasterfullDifficultyMove(maze.GetMazeId());
                    break;
                case INHUMAN_DIFFICULTY:
                    direction = GetInhumanDifficultyMove(maze.GetMazeId());
                    break;
                case NIGHTMARE_DIFFICULTY:
                    direction = GetNightmareDifficultyMove(maze.GetMazeId());
                    break;
                case MASOCHISTIC_DIFFICULTY:
                    direction = GetMasochisticDifficultyMove(maze.GetMazeId());
                    break;
                case GODLIKE_DIFFICULTY:
                    direction = GetGodlikeDifficultyMove(maze.GetMazeId());
                    break;
                default:
                    break;
            }
            return direction;
        }


        public static void DifficultyCheckForPrecalculation(Maze maze)
        {
            bool deadendsIncluded = false;
            bool endpointLocation = false;
            Guid mazeId = maze.GetMazeId();
            int difficulty = maze.GetDifficulty();
            switch (difficulty)
            {
                case MASTERFUL_DIFFICULTY:
                    deadendsIncluded = true;
                    MemoryCacher.AddDomokunMoves(BreadthFirstSearchAlgorithm.BFSAlgorithm(maze, deadendsIncluded, endpointLocation), mazeId);
                    break;
                case INHUMAN_DIFFICULTY:
                    deadendsIncluded = true;
                    endpointLocation = true;
                    MemoryCacher.AddDomokunMoves(BreadthFirstSearchAlgorithm.BFSAlgorithm(maze, deadendsIncluded, endpointLocation), mazeId);
                    break;
                case NIGHTMARE_DIFFICULTY:
                    MemoryCacher.AddDomokunMoves(BreadthFirstSearchAlgorithm.BFSAlgorithm(maze, deadendsIncluded, endpointLocation), mazeId);
                    break;
                case MASOCHISTIC_DIFFICULTY:
                    MemoryCacher.AddDomokunMoves(BreadthFirstSearchAlgorithm.BFSAlgorithm(maze, deadendsIncluded, endpointLocation), mazeId);
                    break;
                case GODLIKE_DIFFICULTY:
                    endpointLocation = true;
                    MemoryCacher.AddDomokunMoves(BreadthFirstSearchAlgorithm.BFSAlgorithm(maze, deadendsIncluded, endpointLocation), mazeId);
                    break;
                default:
                    break;
            }
        }


        public static string GetTouristDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0.8));
        }

        public static string GetNoviceDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0.65));
        }

        public static string GetAverageDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0.5));
        }

        public static string GetExperiencedDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0.35));
        }

        public static string GetSkilledDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0.2));
        }

        public static string GetAdeptDifficultyMove()
        {
            return ConvertNumberToDirection(DifficultiesBasedOnRandom.GetRandomMovement(0));
        }

        public static string GetMasterfullDifficultyMove(Guid mazeId)
        {
            return ConvertNumberToDirection(MemoryCacher.GetDomokunNextMove(mazeId));
        }

        public static string GetInhumanDifficultyMove(Guid mazeId)
        {
            return ConvertNumberToDirection(MemoryCacher.GetDomokunNextMove(mazeId));
        }

        public static string GetNightmareDifficultyMove(Guid mazeId)
        {
            return ConvertNumberToDirection(MemoryCacher.GetDomokunNextMove(mazeId));
        }

        public static string GetMasochisticDifficultyMove(Guid mazeId)
        {
            return ConvertNumberToDirection(MemoryCacher.GetDomokunNextMove(mazeId));
        }

        public static string GetGodlikeDifficultyMove(Guid mazeId)
        {
            return ConvertNumberToDirection(MemoryCacher.GetDomokunNextMove(mazeId));
        }

        public static bool CheckIfCurrentDifficultyIsAdaptible(Maze maze)
        {
            return maze.GetDifficulty() == MASOCHISTIC_DIFFICULTY;
        }


        public static string ConvertNumberToDirection(int number)
        {
            string direction = String.Empty;
            switch (number)
            {
                case NORTH_MOVEMENT:
                    direction = DIRECTION_NORTH;
                    break;
                case SOUTH_MOVEMENT:
                    direction = DIRECTION_SOUTH;
                    break;
                case EAST_MOVEMENT:
                    direction = DIRECTION_EAST;
                    break;
                case WEST_MOVEMENT:
                    direction = DIRECTION_WEST;
                    break;
                case STAY_MOVEMENT:
                    direction = DIRECTION_STAY;
                    break;
                default:
                    direction = DIRECTION_STAY;
                    break;
            }
            return direction;
        }

    }


}