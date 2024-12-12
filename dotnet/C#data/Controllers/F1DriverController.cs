using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class F1DriverController : ControllerBase
    {
        private readonly IF1DriverDao f1DriverDao;
        //private readonly IUserDao userDao;

        public F1DriverController(IF1DriverDao f1DriverDao/*, IUserDao userDao*/)
        {
            this.f1DriverDao = f1DriverDao;
            //this.userDao = userDao;
        }

        //Gets list of all F1 Drivers
        [HttpGet()]
        public ActionResult<List<F1Driver>> GetF1Drivers()
        {
            return Ok(f1DriverDao.GetF1Drivers());
        }

        [HttpGet("{driverId}")]
        public ActionResult<F1Driver> GetF1DriverById(int driverId)
        {
            return Ok(f1DriverDao.GetF1DriverById(driverId));
        }

        [HttpPost("searchname")]
        public ActionResult<List<F1Driver>> GetF1DriverByLikeName(string name)
        {
            return Ok(f1DriverDao.GetF1DriverByLikeName(name));
        }

        [HttpPost()]
        public ActionResult<F1Driver> AddF1Driver(F1Driver driver)
        {
            F1Driver newF1Driver = f1DriverDao.AddF1Driver(driver);
            if (newF1Driver == null || newF1Driver.DriverId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newF1Driver);
            }
        }

        [HttpPut("favorite/{driverId}")]
        public ActionResult<F1Driver> AddOneToDriverPopularity(int driverId)
        {
            F1Driver f1Driver = f1DriverDao.AddOneToDriverPopularity(driverId);
            if (f1Driver == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(f1Driver);
            }
        }

        [HttpPut("unfavorite/{driverId}")]
        public ActionResult<F1Driver> UndoAddOneToDriverPopularity(int driverId)
        {
            F1Driver f1Driver = f1DriverDao.UndoAddOneToDriverPopularity(driverId);
            if (f1Driver == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(f1Driver);
            }
        }

    }
}
