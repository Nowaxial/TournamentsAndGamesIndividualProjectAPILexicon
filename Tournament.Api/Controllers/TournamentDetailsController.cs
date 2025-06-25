using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetailsDto>>> GetTournamentDetails(bool includeGames = false)
        {
            //var tournaments = await unitOfWork.TournamentRepository.GetAllAsync();
            //var dto = mapper.Map<IEnumerable<TournamentDetailsDto>>(tournaments);

            var dto = includeGames ? mapper.Map<IEnumerable<TournamentDetailsDto>>(await unitOfWork.TournamentRepository.GetAllAsync(true)) 
                : mapper.Map<IEnumerable<TournamentDetailsDto>>(await unitOfWork.TournamentRepository.GetAllAsync());

            return Ok(dto);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournamentDetails(int id)
        {
            //var tournamentDetailsDto = mapper.ConfigurationProvider.CreateMapper().Map<TournamentDetailsDto>(await unitOfWork.TournamentRepository.GetAsync(id));
            //TournamentDetails tournamentDetails = await unitOfWork.TournamentRepository.GetAsync(id);

            var tournament = await unitOfWork.TournamentRepository.GetAsync(id);

            if (tournament == null)
            {
                return NotFound();
            }

            var dto = mapper.Map<TournamentDetailsDto>(tournament);
            return Ok(dto);
        }

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, TournamentDetailsUpdateDto tournamentDetailsUpdateDto)
        {
            if (id != tournamentDetailsUpdateDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTournament = await unitOfWork.TournamentRepository.GetAsync(id);
            if (existingTournament == null)
            {
                return NotFound("Tournament not found");
            }

            var tournament = mapper.Map<TournamentDetailsUpdateDto, TournamentDetails>(tournamentDetailsUpdateDto, existingTournament);

            unitOfWork.TournamentRepository.Update(tournament);
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.TournamentRepository.AnyAsync(id))
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
        public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(TournamentDetailsDto dto)
        {
            //unitOfWork.TournamentRepository.Add(tournamentDetails);

            //await unitOfWork.CompleteAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tournamentDetails = mapper.Map<TournamentDetails>(dto);
            unitOfWork.TournamentRepository.Add(tournamentDetails);
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to save tournament");
            }

            return CreatedAtAction(nameof(GetTournamentDetails), new { id = tournamentDetails.Id }, "Tournament created successfully");
            //var entity = mapper.Map<TournamentDetails>(dto);
            //unitOfWork.TournamentRepository.Add(entity);
            //await unitOfWork.CompleteAsync();

            //var createdDto = mapper.Map<TournamentDetailsDto>(entity);
            //return CreatedAtAction(nameof(GetTournamentDetails), new { id = entity.Id }, createdDto);
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

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete tournament");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournamentDetails(int id, JsonPatchDocument<TournamentDetailsUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var tournamentToUpdate = await unitOfWork.TournamentRepository.GetAsync(id);
            if (tournamentToUpdate == null)
            {
                return NotFound("Tournament does not exist");
            }

            var tournamentDetailsdto = mapper.Map<TournamentDetailsUpdateDto>(tournamentToUpdate);
            patchDoc.ApplyTo(tournamentDetailsdto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(tournamentDetailsdto, tournamentToUpdate);
            unitOfWork.TournamentRepository.Update(tournamentToUpdate);
            
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.TournamentRepository.AnyAsync(id))
                {
                    return NotFound("Tournament does not exist");
                }
                else
                {
                    return StatusCode(500, "Failed to update tournament");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update tournament");
            }
            return NoContent();
        }
    }
}