using NUnit.Framework;
using PonyWebChallenge.Controllers;
using PonyWebChallenge.MazeGeneration;
using PonyWebChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PonyWebChallenge.Tests.Controllers
{
    [TestFixture()]
    public class TestPonyChallengeController
    {
        private const string DIRECTION_STAY = "stay";
        private const string DIRECTION_NORTH = "north";
        private const string DIRECTION_SOUTH = "south";
        private const string DIRECTION_WEST = "west";
        private const string DIRECTION_EAST = "east";

        public MazeController controller;
        public static List<MazeInitModel> difficultiesModels;
        public static List<Guid> mazeIds ;
        public static List<string> allPossibleMoves;

        [SetUp]
        public void Init()
        {
            controller = new MazeController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            mazeIds = new List<Guid>();

            allPossibleMoves = new List<string> { DIRECTION_STAY, DIRECTION_NORTH, DIRECTION_SOUTH, DIRECTION_EAST, DIRECTION_WEST };

            difficultiesModels = new List<MazeInitModel>();

            MazeInitModel initModel = new MazeInitModel()
            {
                Difficulty = DifficultiesEnum.Difficulty.Godlike,
                MazeHeight = 15,
                MazeWidth = 20,
                PlayerName = "Fluttershy"
            };
            difficultiesModels.Add(initModel);
            initModel = new MazeInitModel()
            {
                Difficulty = DifficultiesEnum.Difficulty.Masochistic,
                MazeHeight = 15,
                MazeWidth = 20,
                PlayerName = "Fluttershy"
            };
            difficultiesModels.Add(initModel);
            initModel = new MazeInitModel()
            {
                Difficulty = DifficultiesEnum.Difficulty.Tourist,
                MazeHeight = 15,
                MazeWidth = 20,
                PlayerName = "Fluttershy"
            };
            difficultiesModels.Add(initModel);
        }

        [Test, Order(1)]
        public void CreateMazeTest()
        {
            foreach (var initModel in difficultiesModels)
            {
                var message = controller.CreateNewMaze(initModel);
                Guid mazeId = Guid.Parse(message.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty));
                mazeIds.Add(mazeId);
                Assert.True(message.StatusCode == System.Net.HttpStatusCode.OK);
            }
        }

        [Test, Order(2)]
        public void CreateMazeCharactersTest()
        {
            var controller = new MazeController();
            foreach (var mazeId in mazeIds)
            {
                MazeModel model = controller.CreateMazeCharacters(mazeId);
                Assert.True(model.GameState.StateResult == StateResult.SuccesfullyCreated);
            }
        }

        [Test, Order(3)]
        public void PrintMazeTest()
        {
            foreach (var mazeId in mazeIds)
            {
                var message = controller.PrintAction(mazeId);
                Assert.True(message.StatusCode == System.Net.HttpStatusCode.OK);
            }
        }

        [Test, Order(4)]
        public void MoveCharacterTest()
        {
            var controller = new MazeController();
            foreach (var mazeId in mazeIds)
                foreach (string possibleMove in allPossibleMoves)
                {
                    var message = controller.MoveCharacters(mazeId, new MoveCharactersInMazeModel
                    {
                        Direction = possibleMove
                    });

                    Assert.True(message.State == State.Active);
                }

            Assert.True(true);
        }
    }
}
