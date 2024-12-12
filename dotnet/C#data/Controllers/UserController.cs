using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        //[HttpPut("{driverId}")]
        //public ActionResult<UserFavoriteDriver> MakeFavoriteDriver(int driverId, string username)
        //{
        //    UserFavoriteDriver userFavoriteDriver = userDao.MakeFavoriteDriver(driverId, username);
        //    if (userFavoriteDriver == null)
        //    {
        //        return BadRequest();
        //    }
        //    else
        //    {
        //        return Ok(userFavoriteDriver);
        //    }

        //}
        [HttpPut("{driverId}/{username}")]
        public ActionResult<UserFavoriteDriver> MakeFavoriteDriver(int driverId, string username)
        {
            try
            {
                // Attempt to mark the driver as favorite for the user
                UserFavoriteDriver userFavoriteDriver = userDao.MakeFavoriteDriver(driverId, username);
                if (userFavoriteDriver == null)
                {
                    // Return 400 Bad Request if the operation fails logically
                    return BadRequest("Unable to mark the driver as favorite. Please check the input.");
                }
                else
                {
                    // Return 200 OK with the updated favorite driver
                    return Ok(userFavoriteDriver);
                }
            }
            catch (SqlException ex)
            {
                // Log the SQL exception and return a 500 Internal Server Error response
                // Replace this comment with actual logging if implemented
                return StatusCode(500, "An error occurred while accessing the database. Please try again later.");
            }
            catch (Exception ex)
            {
                // Handle any other unforeseen exceptions
                // Replace this comment with actual logging if implemented
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        //the next put request is just a place holder. fix later
        [HttpPut("unfavorite/{username}")]
        public void UnselectFavoriteDriver(string username)
        {
            userDao.UnselectFavoriteDriver(username);
        }

        [HttpGet("favoritedriver/{username}")]
        public ActionResult<int> GetFavoriteDriverId(string username)
        {
            return Ok(userDao.GetFavoriteDriverId(username));
        }
    }
}
