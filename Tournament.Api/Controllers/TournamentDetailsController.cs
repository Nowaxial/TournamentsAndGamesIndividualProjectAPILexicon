﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController(IUnitOfWork unitOfWork) : ControllerBase
    {
        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<IEnumerable<TournamentDetails>> GetTournamentDetails()
        {
            return await unitOfWork.TournamentRepository.GetAllAsync();
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetails>> GetTournamentDetails(int id)
        {
            TournamentDetails tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);

            if (tournamentDetails == null)
            {
                return NotFound();
            }

            return tournamentDetails;
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentDetails tournamentDetails)
        {
            if (id != tournamentDetails.Id)
            {
                return BadRequest();
            }

            unitOfWork.TournamentRepository.Add(tournamentDetails);

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TournamentDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetails tournamentDetails)
        {
            unitOfWork.TournamentRepository.Add(tournamentDetails);

            await unitOfWork.CompleteAsync();

            return CreatedAtAction("GetTournamentDetails", new { id = tournamentDetails.Id }, tournamentDetails);
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            TournamentDetails tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);
            if (tournamentDetails == null)
            {
                return NotFound();
            }

            unitOfWork.TournamentRepository.Remove(tournamentDetails);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        private Task<bool> TournamentDetailsExists(int id)
        {
            return unitOfWork.TournamentRepository.AnyAsync(id);
        }
    }
}
