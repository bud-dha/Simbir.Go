﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simbir.Go.BLL.DTO;
using Simbir.Go.BLL.Services.Admin;
using Simbir.Go.DAL.Models;

namespace Simbir.Go.Controllers.AdminArea
{
    [ApiController, Route("api/Admin")]
    public class AdminRentController : ControllerBase
    {
        private AdminRentService _adminRentService;


        public AdminRentController(AdminRentService adminRentService)
        {
            _adminRentService = adminRentService;
        }


        [HttpGet("Rent/{rentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Rent>> GetRentById(int rentId)
        {
            try
            {
                return await _adminRentService.RentById(rentId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("UserHistory/{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rent>>> GetUserHistory(int userId)
        {
            try
            {
                return await _adminRentService.UserHistory(userId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet, Route("TransportHistory/{transportId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Rent>>> GetTransportHistory(int transportId)
        {
            try
            {
                return await _adminRentService.TransportHistory(transportId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost, Route("Rent"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromBody] AdminRentDTO dto)
        {
            try
            {
                await _adminRentService.CreateRent(dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Rent/End/{rentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post(long rentId, double latitude, double longitude)
        {
            try
            {
                await _adminRentService.EndRent(rentId, latitude, longitude);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpPut("Rent/{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Put(int id, [FromBody] AdminRentDTO dto)
        {
            try
            {
                await _adminRentService.UpdateRent(id, dto);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("Rent/{rentId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int rentId)
        {
            try
            {
                await _adminRentService.DeleteRent(rentId);
            }
            catch (ArgumentException ex)
            {
                return Problem(ex.Message);
            }
            return Ok();
        }
    }
}