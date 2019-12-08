using System;

namespace PonyWebChallenge.Models
{
    public class CellStateEnumModel
    {
        [Flags]
        public enum CellState
        {
            North = 1,
            West = 8,
            Visited = 128,
            Initial = North | West,
            Pony = 1024,
            Domokun = 2048,
            Endpoint = 4096,
            NotVisited = 8192
        }

        public struct RemoveWallAction
        {
            public int Neighbour;
            public CellState Wall;
        }

        public struct Neighbours
        {
            public int NeighbourLocation;
            public int Direction;
        }
    }


}