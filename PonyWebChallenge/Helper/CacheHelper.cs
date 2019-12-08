using PonyWebChallenge.MazeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace PonyWebChallenge.Helper
{
    public static class MemoryCacher
    {
        public const string DOKOMUN_MOVES_CACHE_KEY = "Domokun";

        private const string DIRECTION_STAY = "stay";
        private const string DIRECTION_NORTH = "north";
        private const string DIRECTION_SOUTH = "south";
        private const string DIRECTION_WEST = "west";
        private const string DIRECTION_EAST = "east";

        public static Maze GetMazeFromCache(Guid key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return (Maze)memoryCache.Get(key.ToString());
        }

        public static Guid AddMazeInCache(Maze maze)
        {
            Guid mazeId = Guid.NewGuid();
            maze.SetMazeId(mazeId);

            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Add(mazeId.ToString(), maze, GetCacheItemPolicy());
            return mazeId;
        }

        public static void UpdateMazeInCache(Guid key, Maze value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key.ToString()))
            {
                memoryCache[key.ToString()] = value;
            }
        }

        public static void DeleteMazeFromCache(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public static int GetDomokunNextMove(Guid mazeId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            Stack<int> moves = (Stack<int>)memoryCache.Get(mazeId.ToString() + DOKOMUN_MOVES_CACHE_KEY);
            int nextMove = 4;
            if (moves.Count > 0)
            {
                nextMove = moves.Pop();
            }

            UpdateDomokunMovesInCache(mazeId, moves);
            return nextMove;
        }

        public static void AppendDomokunNextMove(Guid mazeId, string direction)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            Stack<int> moves = (Stack<int>)memoryCache.Get(mazeId.ToString() + DOKOMUN_MOVES_CACHE_KEY);
            if (moves != null)
            {
                List<int> tempMoves = new List<int>();
                int nextMove = ConvertDirectionToNumber(direction);
                tempMoves = moves.ToList();
                tempMoves.Add(nextMove);
                tempMoves.Reverse();
                moves = new Stack<int>(tempMoves);
            }

            UpdateDomokunMovesInCache(mazeId, moves);
        }

        public static void AddDomokunMoves(Stack<int> moves, Guid mazeId)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(mazeId.ToString()))
            {
                memoryCache.Add(mazeId.ToString() + DOKOMUN_MOVES_CACHE_KEY, moves, GetCacheItemPolicy());
            }
        }

        public static void UpdateDomokunMovesInCache(Guid key, Stack<int> moves)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key.ToString() + DOKOMUN_MOVES_CACHE_KEY))
            {
                memoryCache[key.ToString() + DOKOMUN_MOVES_CACHE_KEY] = moves;
            }
        }

        public static void DeleteDomokunMovesFromCache(Guid mazeId)
        {
            MemoryCache memoryCache = MemoryCache.Default;

            if (memoryCache.Contains(mazeId.ToString() + DOKOMUN_MOVES_CACHE_KEY))
            {
                memoryCache.Remove(mazeId.ToString() + DOKOMUN_MOVES_CACHE_KEY);
            }
        }

        private static CacheItemPolicy GetCacheItemPolicy()
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(5);
            return policy;
        }


        private static int ConvertDirectionToNumber(string directionString)
        {
            int direction = 4;
            switch (directionString)
            {
                case DIRECTION_NORTH:
                    direction = 0;
                    break;
                case DIRECTION_SOUTH:
                    direction = 1;
                    break;
                case DIRECTION_EAST:
                    direction = 2;
                    break;
                case DIRECTION_WEST:
                    direction = 3;
                    break;
                case DIRECTION_STAY:
                    direction = 4;
                    break;
                default:
                    direction = 4;
                    break;
            }
            return direction;
        }

    }
}