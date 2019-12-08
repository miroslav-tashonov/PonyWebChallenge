using PonyWebChallenge.MazeGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PonyWebChallenge.Models
{
    public class MazeModel
    {
        [Required(ErrorMessage = "Pony is required")]
        public int Pony { get; set; }
        [Required(ErrorMessage = "Domokun is required")]
        public int Domokun { get; set; }
        [Required(ErrorMessage = "Endpoint is required")]
        public int Endpoint { get; set; }

        public Maze Maze { get; set; }

        public List<int> Size { get; set; }

        public int Difficulty { get; set; }

        public GameState GameState { get; set; }

        public Guid MazeId { get; set; }
    }

    public class GameState
    {
        public State State { get; set; }

        public StateResult StateResult { get; set; }
    }

    public enum State
    {
        Active = 0,
        Inactive = 1
    }

    public enum StateResult
    {
        MoveAccepted = 0,
        MoveRejected = 1,
        InvalidMove = 2,
        EndGame = 3,
        Lose = 4,
        SuccesfullyCreated = 5
    }
}