using PonyWebChallenge.Helper;
using PonyWebChallenge.MazeGeneration;
using System.Collections.Generic;
using System.Linq;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Difficulties
{
    public static class BreadthFirstSearchAlgorithm
    {
        public static readonly int NORTH_MOVEMENT = 0;
        public static readonly int SOUTH_MOVEMENT = 1;
        public static readonly int EAST_MOVEMENT = 2;
        public static readonly int WEST_MOVEMENT = 3;
        public static readonly int STAY_MOVEMENT = 4;

        public static List<int> listFinalMoves;
        public static Stack<int> finalMoves;

        //endpointLocation flag - if we like to chase the pony or the endpoint
        //deadendsIncluded flag - if false the algorithm chooses shortest route
        public static Stack<int> BFSAlgorithm(Maze maze,
            bool deadEndsIncluded,
            bool endpointLocation)
        {
            Stack<int> movesList = new Stack<int>();
            finalMoves = new Stack<int>();
            listFinalMoves = new List<int>();

            Maze movesMaze = maze;
            int mazeHeight = movesMaze.GetMazeHeight();
            int mazeWidth = movesMaze.GetMazeWidth();
            int mazeDomokunLocation = movesMaze.GetDomokunId();
            int characterLocation = endpointLocation ? movesMaze.GetEndpointId() : movesMaze.GetPonyId();
            CellState[] cellsArray = movesMaze.GetCells();

            for (var x = 0; x < mazeHeight; x++)
                for (var y = 0; y < mazeWidth; y++)
                    cellsArray[(x * mazeWidth) + y] |= CellState.NotVisited;

            
            if (deadEndsIncluded)
            {
                VisitCellBFSWithDeadendsIncluded(movesMaze.GetCells(), mazeDomokunLocation, characterLocation, mazeWidth, mazeHeight);
                //reversing the list because the moves are getting from stack in cache
                listFinalMoves.Reverse();
                movesList = new Stack<int>(listFinalMoves);
            }
            else
            {
                movesList = VisitCellBFS(movesMaze.GetCells(), mazeDomokunLocation, characterLocation, mazeWidth, mazeHeight);
            }
            return movesList;
        }

        public static Stack<int> VisitCellBFS(CellState[] array, int xy, int finalLocation, int width, int height)
        {
            array[xy] -= CellState.NotVisited;
            foreach (var neighbour in GetNeighboursBFS(array, xy, width, height).Where(z => (array[z.NeighbourLocation].HasFlag(CellState.NotVisited))))
            {
                if (neighbour.NeighbourLocation == finalLocation)
                {
                    finalMoves.Push(neighbour.Direction);
                    return finalMoves;
                }

                finalMoves.Concat(VisitCellBFS(array, neighbour.NeighbourLocation, finalLocation, width, height)).ToList();
                if (finalMoves.Count != 0)
                {
                    finalMoves.Push(neighbour.Direction);
                    break;
                }
            }

            return finalMoves;
        }

        public static bool VisitCellBFSWithDeadendsIncluded(CellState[] array, int xy, int finalLocation, int width, int height)
        {
            array[xy] -= CellState.NotVisited;

            foreach (var neighbour in GetNeighboursBFS(array, xy, width, height).Where(z => (array[z.NeighbourLocation].HasFlag(CellState.NotVisited))))
            {
                listFinalMoves.Add(neighbour.Direction);
                if (neighbour.NeighbourLocation == finalLocation)
                    return true;

                if (VisitCellBFSWithDeadendsIncluded(array, neighbour.NeighbourLocation, finalLocation, width, height))
                    break;
                else
                    listFinalMoves.Add(GetOppositeDirection( neighbour.Direction));
            }

            return false;
        }

        public static IEnumerable<Neighbours> GetNeighboursBFS(CellState[] array, int xyPoint, int _width, int _height)
        {
            if (!MazeHelper.CheckIfLeftBorder(_width, xyPoint) && !array[xyPoint].HasFlag(CellState.West))
                yield return new Neighbours { NeighbourLocation = xyPoint - 1, Direction = WEST_MOVEMENT };
            if (!MazeHelper.CheckIfUpperBorder(_width, xyPoint) && !array[xyPoint].HasFlag(CellState.North))
                yield return new Neighbours { NeighbourLocation = xyPoint - _width, Direction = NORTH_MOVEMENT }; ;
            if (!MazeHelper.CheckIfRightBorder(_width, xyPoint) && !array[xyPoint + 1].HasFlag(CellState.West))
                yield return new Neighbours { NeighbourLocation = xyPoint + 1, Direction = EAST_MOVEMENT };
            if (!MazeHelper.CheckIfLowerBorder(_width, _height, xyPoint) && !array[xyPoint + _width].HasFlag(CellState.North))
                yield return new Neighbours { NeighbourLocation = xyPoint + _width, Direction = SOUTH_MOVEMENT };
        }

        public static int GetOppositeDirection(int direction)
        {
            if (direction == NORTH_MOVEMENT)
                return SOUTH_MOVEMENT;
            else if (direction == SOUTH_MOVEMENT)
                return NORTH_MOVEMENT;
            else if (direction == EAST_MOVEMENT)
                return WEST_MOVEMENT;
            else if (direction == WEST_MOVEMENT)
                return EAST_MOVEMENT;
            else
                return STAY_MOVEMENT;
        }
    }
}