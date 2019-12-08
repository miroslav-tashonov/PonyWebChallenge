using PonyWebChallenge.Helper;
using PonyWebChallenge.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PonyWebChallenge.Controllers
{
    public class MazeViewController : Controller
    {
        public readonly string ERRORMESSAGE_MAZEID_INVALID = "Maze Id is invalid";

        public MazeViewController() { }

        public ActionResult Index()
        {
            try
            {
                ViewBag.Ponies = PoniesHelper.GetPonies();
                ViewBag.Difficulties = DifficultiesHelper.GetDifficulties();
            }
            catch (Exception ex)
            {
                HttpResponseException exception = CreateResponseException(HttpStatusCode.InternalServerError, ex.Message);
                throw exception;
            }

            return View();
        }

        public ActionResult MazeAction(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);

            return View(
                new MazeActionModel()
                {
                    MazeId = id
                }
            );
        }

        public ActionResult SuccessfullEndgame(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);

            return View(new MazeActionModel()
            {
                MazeId = id
            });
        }

        public ActionResult FailedEndgame(Guid id)
        {
            if (id == null || id == Guid.Empty)
                throw CreateResponseException(HttpStatusCode.BadRequest, ERRORMESSAGE_MAZEID_INVALID);

            return View(new MazeActionModel()
            {
                MazeId = id
            });
        }

        public HttpResponseException CreateResponseException(HttpStatusCode code, string message)
        {
            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(message)
            };

            return new HttpResponseException(HttpStatusCode.InternalServerError);
        }
    }
}
