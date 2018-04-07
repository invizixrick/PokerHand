using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cards;

namespace PokerHand.Controllers
{
    public class PokerHandController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetWinner(GameViewModel game)
        {
            string message = "Error: ";
            HttpResponseMessage resp;
            if (game != null && game.Hand1 != null && game.Hand2 != null)
            {
                try
                {
                    PokerGame pokergame = new PokerGame();
                    pokergame.AddPlayer(new Player(game.Hand1.playername, game.Hand1.c1, game.Hand1.c2, game.Hand1.c3, game.Hand1.c4, game.Hand1.c5));
                    pokergame.AddPlayer(new Player(game.Hand2.playername, game.Hand2.c1, game.Hand2.c2, game.Hand2.c3, game.Hand2.c4, game.Hand2.c5));

                    message = pokergame.GetWinner();
                    resp = Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    message += ex.Message;
                    resp = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
                }
            }
            else
            {
                message += "Player game or hands not provided.";
                resp = Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }

            resp.Content = new StringContent(message, System.Text.Encoding.Unicode);

            return resp;
        }
    }
    public class HandViewModel
    {
        public HandViewModel() { }
        public string playername { get; set; }
        public string c1 { get; set; }
        public string c2 { get; set; }
        public string c3 { get; set; }
        public string c4 { get; set; }
        public string c5 { get; set; }
    }
    public class GameViewModel
    {
        public HandViewModel Hand1 { get; set; }
        public HandViewModel Hand2 { get; set; }
    }
}

