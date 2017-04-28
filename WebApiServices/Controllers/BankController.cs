using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using System.Web.Http.Cors;

namespace WebServices.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BankController : ApiController
    {
        // GET api/values
        public int Get()
        {
            return 500;
        }

        // GET api/values/5
        public int? Get(string session)
        {
            using (BlackJackContext dbContext = new BlackJackContext())
            {
                var model = dbContext.PlayLog.Where(pl => pl.SessionID == session)?.FirstOrDefault();
                if (model == null)
                {
                    PlayLog data = new PlayLog();
                    data.Cash = 500;
                    data.CreatedDate = DateTime.Now;
                    data.PutCache = 0;
                    data.SessionID = session;
                    data.Win = true;
                    dbContext.PlayLog.Add(data);
                    dbContext.SaveChanges();
                }
                return model != null ? model.Cash : 500;
            }
        }
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public void Post(Data _data)
        {
            using (BlackJackContext dbContext = new BlackJackContext())
            {
                var model = dbContext.PlayLog.Where(pl => pl.SessionID == _data.session)?.FirstOrDefault();
                model.PutCache = _data.putCache;
                model.CreatedDate = DateTime.Now;
                if (_data.isWin)
                {
                    if (_data.putCache <= model.Cash)
                    {
                        model.Cash += _data.putCache;
                    }                    
                }
                else
                {
                    if (_data.putCache <= model.Cash)
                    {
                        model.Cash -= _data.putCache;
                    }
                    else
                    {
                        model.Cash = 0;
                    }
                }
                model.Win = _data.isWin;
                dbContext.SaveChanges();
            }
        }
    }
    public class Data
    {
        public int putCache { get; set; }
        public string session { get; set; }
        public bool isWin { get; set; }
    }
}
