using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _21Games.Controllers
{
    public class HomeController : Controller
    {
        static Random rnd = new Random();
        // GET: Home
        public async Task<ActionResult> Index()
        {
            //Card model = GetRandomCard();
            //return View(model);
            Session.Remove("Player_1");
            Session.Remove("Player_2");
            ViewBag.Session = HttpContext.Session.SessionID;
            BankRestServices services = new BankRestServices();
            var model = await services.GetCache(HttpContext.Session.SessionID);
            return View(int.Parse(model));
        }
        /*public Card GetRandomCard()
        {
            int r = rnd.Next(AllCards().Count);
            return (Card)AllCards()[r];
        }*/
        public string GetRandomCardHtml(int Player)
        {
            Player = Player == 3 ? 1 :Player;
            bool result = true;
            string html = "";
            while (result)
            {
                //return "<image class='spritesA' value='1'></image>&nbsp";            
                int r = rnd.Next(AllCards().Count);
                var card = (Card)AllCards()[r];
                if (CheckBeforeCards(Player, card.Css))
                {
                    result = false;
                    html= "<image class='" + card.Css + "' value='" + card.Value + "'></image>&nbsp";
                }
                else
                {
                    result = true;
                }                
            }
            return html;
        }
        public bool CheckBeforeCards(int Player, string Css)
        {
            if (Session["Player_" + Player] == null)
            {
                List<string> Cards = new List<string>();
                Cards.Add(Css);
                Session["Player_" + Player] = Cards;
                return true;
            }
            else
            {
                List<string> Cards = (List<string>)Session["Player_" + Player];
                if (Cards.Contains(Css))
                {
                    return false;
                }
                else {
                    Cards.Add(Css);
                    Session["Player_" + Player] = Cards;
                    return true;
                }
            }            
        }
        public static List<Card> AllCards()
        {
            List<Card> CardList = new List<Card>();
            CardList.Add(new Card(1, "spritesA"));
            CardList.Add(new Card(2, "sprites2"));
            CardList.Add(new Card(3, "sprites3"));
            CardList.Add(new Card(4, "sprites4"));
            CardList.Add(new Card(5, "sprites5"));
            CardList.Add(new Card(6, "sprites6"));
            CardList.Add(new Card(7, "sprites7"));
            CardList.Add(new Card(8, "sprites8"));
            CardList.Add(new Card(9, "sprites9"));
            CardList.Add(new Card(10, "spritesB"));
            CardList.Add(new Card(10, "spritesK"));
            CardList.Add(new Card(10, "spritesP"));

            CardList.Add(new Card(1, "spritekA"));
            CardList.Add(new Card(2, "spritek2"));
            CardList.Add(new Card(3, "spritek3"));
            CardList.Add(new Card(4, "spritek4"));
            CardList.Add(new Card(5, "spritek5"));
            CardList.Add(new Card(6, "spritek6"));
            CardList.Add(new Card(7, "spritek7"));
            CardList.Add(new Card(8, "spritek8"));
            CardList.Add(new Card(9, "spritek9"));
            CardList.Add(new Card(10, "spritekB"));
            CardList.Add(new Card(10, "spritekK"));
            CardList.Add(new Card(10, "spritekP"));

            CardList.Add(new Card(1, "spritemA"));
            CardList.Add(new Card(2, "spritem2"));
            CardList.Add(new Card(3, "spritem3"));
            CardList.Add(new Card(4, "spritem4"));
            CardList.Add(new Card(5, "spritem5"));
            CardList.Add(new Card(6, "spritem6"));
            CardList.Add(new Card(7, "spritem7"));
            CardList.Add(new Card(8, "spritem8"));
            CardList.Add(new Card(9, "spritem9"));
            CardList.Add(new Card(10, "spritemB"));
            CardList.Add(new Card(10, "spritemK"));
            CardList.Add(new Card(10, "spritemP"));

            CardList.Add(new Card(1, "spritekaA"));
            CardList.Add(new Card(2, "spriteka2"));
            CardList.Add(new Card(3, "spriteka3"));
            CardList.Add(new Card(4, "spriteka4"));
            CardList.Add(new Card(5, "spriteka5"));
            CardList.Add(new Card(6, "spriteka6"));
            CardList.Add(new Card(7, "spriteka7"));
            CardList.Add(new Card(8, "spriteka8"));
            CardList.Add(new Card(9, "spriteka9"));
            CardList.Add(new Card(10, "spritekaB"));
            CardList.Add(new Card(10, "spritekaK"));
            CardList.Add(new Card(10, "spritekaP"));

            return CardList;
        }
    }
    public class Card
    {
        public Card(int value, string css)
        {
            Value = value;
            Css = css;
        }
        public int Value { get; set; }
        public string Css { get; set; }
    }
    public class BankRestServices
    {

        public async Task<string> GetCache(string session)
        {
            string uri = "http://blackjackservice.azurewebsites.net/api/bank?session=" + session;
            using (HttpClient httpClient = new HttpClient())
            {

                HttpResponseMessage response = await httpClient.GetAsync(uri);

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}