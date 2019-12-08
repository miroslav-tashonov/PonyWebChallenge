using PonyWebChallenge.Difficulties;
using PonyWebChallenge.Helper;
using PonyWebChallenge.MazeGeneration;
using PonyWebChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static PonyWebChallenge.Models.CellStateEnumModel;

namespace PonyWebChallenge.Controllers
{
    public class MazeController : ApiController
    {
        public readonly string ERRORMESSAGE_MAZEID_INVALID = "Maze Id is invalid";
        public readonly string ERRORMESSAGE_PONY_INVALID = "Pony name is invalid";
        public readonly string ERRORMESSAGE_DIFFICULTY_INVALID = "Difficulty is invalid";

        public MazeController() { }

        // CreateMazeCharacters: pony-challenge/Maze/GUID
        [System.Web.Http.Route("pony-challenge/Maze/{mazeId:guid}")]
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public MazeModel CreateMazeCharacters(Guid mazeId)
        {
            try
            {
                if (mazeId != null && mazeId != Guid.Empty)
                {
                    Random randomGenerator = new Random();

                    Maze maze = MemoryCacher.GetMazeFromCache(mazeId);
                    MemoryCacher.DeleteDomokunMovesFromCache(mazeId);

                    //clear characters if new game is choosed
                    MazeHelper.ClearMazeCharacters(maze);
                    int mazeHeight = maze.GetMazeHeight();
                    int mazeWidth = maze.GetMazeWidth();

                    int randomPonyId = 0;
                    int randomDomokunId = 0;
                    int randomEndpointId = 0;
                    do
                    {
                        randomPonyId = randomGenerator.Next(mazeHeight * mazeWidth);
                        randomDomokunId = randomGenerator.Next(mazeHeight * mazeWidth);
                        randomEndpointId = randomGenerator.Next(mazeHeight * mazeWidth);
                    }
                    while (CheckIfTwoSameRandoms(randomPonyId, randomDomokunId, randomEndpointId));

                    maze.SetDomokunId(randomDomokunId);
                    maze.SetPonyId(randomPonyId);
                    maze.SetEndpointId(randomEndpointId);

                    MemoryCacher.UpdateMazeInCache(mazeId, maze);
                    DifficultyManager.DifficultyCheckForPrecalculation(maze);

                    return new MazeModel
                    {
                        Pony = randomPonyId,
                        Domokun = randomDomokunId,
                        Endpoint = randomEndpointId,
                        Maze = maze,
                        Difficulty = maze.GetDifficulty(),
                        Size = new List<int> { mazeHeight, mazeWidth },
                        GameState = new GameState { State = State.Active, StateResult = StateResult.SuccesfullyCreated },
                        MazeId = mazeId
                    };
                }
                else
                {
                    HttpResponseException exception = CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                HttpResponseException exception = CreateResponseException(HttpStatusCode.InternalServerError, ex.Message);
                throw exception;
            }

        }


        [System.Web.Http.Route("pony-challenge/Maze/{mazeId:guid}")]
        [System.Web.Http.AcceptVerbs("POST")]
        [System.Web.Http.HttpPost]
        // GET: pony-challenge/Maze/GUID
        public GameState MoveCharacters(Guid mazeId, MoveCharactersInMazeModel moveModel)
        {
            string direction = moveModel.Direction;
            try
            {
                if (mazeId != null && mazeId != Guid.Empty)
                {
                    Maze maze = MemoryCacher.GetMazeFromCache(mazeId);
                    CellState[] mazeCells = maze.GetCells();
                    int ponyLocation = maze.GetPonyId();
                    int mazeWidth = maze.GetMazeWidth();
                    int mazeHeight = maze.GetMazeHeight();

                    StateResult result = MazeHelper.MoveCharacter(maze, direction, CellState.Pony);
                    if (result == StateResult.MoveAccepted)
                    {
                        if (DifficultyManager.CheckIfCurrentDifficultyIsAdaptible(maze))
                        {
                            MemoryCacher.AppendDomokunNextMove(mazeId, direction);
                        }

                        string DomokunDirection = DifficultyManager.GetDomokunDirection(maze);
                        result = MazeHelper.MoveCharacter(maze, DomokunDirection, CellState.Domokun);
                    }
                    MemoryCacher.UpdateMazeInCache(mazeId, maze);

                    return new GameState
                    {
                        State = State.Active,
                        StateResult = result
                    };
                }
                else
                {
                    HttpResponseException exception = CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                HttpResponseException exception = CreateResponseException(HttpStatusCode.InternalServerError, ex.Message);
                throw exception;
            }
        }


        [System.Web.Http.Route("pony-challenge/Maze/{mazeId:guid}/print")]
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        // GET: pony-challenge/Maze/GUID/print
        public HttpResponseMessage PrintAction(Guid mazeId)
        {
            string mazeString = String.Empty;

            try
            {
                if (mazeId != null && mazeId != Guid.Empty)
                {
                    var maze = MemoryCacher.GetMazeFromCache(mazeId);
                    mazeString = MazeHelper.PrintMazeAsHTML(maze);
                }
                else
                {
                    HttpResponseException exception = CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);
                    throw exception;
                }
            }
            catch (Exception ex)
            {
                HttpResponseException exception = CreateResponseException(HttpStatusCode.InternalServerError, ex.Message);
                throw exception;
            }

            return Request.CreateResponse(HttpStatusCode.OK, mazeString);
        }



        // CreateNewMaze: pony-challenge/Maze
        // creation of the maze
        [System.Web.Http.AcceptVerbs("Post")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateNewMaze(MazeInitModel model)
        {
            Guid mazeId = Guid.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var maze = new Maze(model);
                    mazeId = MemoryCacher.AddMazeInCache(maze);
                }
                catch(Exception ex)
                {
                    throw CreateResponseException(HttpStatusCode.InternalServerError, ex.Message);
                }

                return Request.CreateResponse(HttpStatusCode.OK, mazeId.ToString());
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                    .SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }

        #region Helper Methods
        public HttpResponseException CreateResponseException(HttpStatusCode code, string message)
        {
            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(message)
            };

            return new HttpResponseException(HttpStatusCode.InternalServerError);
        }

        public bool CheckIfTwoSameRandoms(int pony, int gokumon, int endpoint)
        {
            if (pony != gokumon && pony != endpoint && gokumon != endpoint)
                return false;

            return true;
        }

        #endregion
    }
}
