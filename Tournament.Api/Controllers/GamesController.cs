﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        // GET: api/Games
        [HttpGet()]
        public async Task<ActionResult<GameCreateDto>> GetGame(string? sortField = null, bool ascending = true)
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "title":
                        games = ascending ? games.OrderBy(g => g.Title).ToList() : games.OrderByDescending(g => g.Title).ToList();
                        break;
                    case "time":
                        games = ascending ? games.OrderBy(g => g.Time).ToList() : games.OrderByDescending(g => g.Time).ToList();
                        break;
                    case "tournamentDetailsId":
                        games = ascending ? games.OrderBy(g => g.TournamentDetailsId).ToList() : games.OrderByDescending(g => g.TournamentDetailsId).ToList();
                        break;
                    default:
                        return BadRequest();
                }
            }

            // Map entities to DTOs and return
            var gameDtos = mapper.Map<List<GameCreateDto>>(games);
            return Ok(gameDtos);
        }

        //GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(int id)
        {
            var game = await unitOfWork.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound("Game doesn't exist");
            }
            var gameDto = mapper.Map<GameDto>(game);
            return Ok(gameDto);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutGame(int id, GameUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGame = await unitOfWork.GameRepository.GetAsync(id);
            if (existingGame == null)
            {
                return NotFound("Game doesn't exist");
            }

            var gameToUpdate = mapper.Map<GameUpdateDto, Game>(dto, existingGame);
            unitOfWork.GameRepository.Update(gameToUpdate);

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.GameRepository.AnyAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Failed to update game");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update game");
            }

            return NoContent();
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameCreateDto gameCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gameToCreate = mapper.Map<Game>(gameCreateDto);
            unitOfWork.GameRepository.Add(gameToCreate);

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to save game");
            }

            return CreatedAtAction("GetGame", new { id = gameToCreate.Id }, gameToCreate);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await unitOfWork.GameRepository.GetAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            unitOfWork.GameRepository.Remove(game);
            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to delete game");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchGame(int id, JsonPatchDocument<GameUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var gameToUpdate = await unitOfWork.GameRepository.GetAsync(id);
            if (gameToUpdate == null)
            {
                return NotFound("Game does not exist");
            }

            var gameDetailsdto = mapper.Map<GameUpdateDto>(gameToUpdate);
            patchDoc.ApplyTo(gameDetailsdto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(gameDetailsdto, gameToUpdate);
            unitOfWork.GameRepository.Update(gameToUpdate);

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.GameRepository.AnyAsync(id))
                {
                    return NotFound("Game does not exist");
                }
                else
                {
                    return StatusCode(500, "Failed to update game");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to update game");
            }
            return NoContent();
        }

        // GET: title/{title}
        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGameByTitle(string title)
        {
            var games = await unitOfWork.GameRepository.GetAllAsync();
            var filteredGames = games.Where(g => g.Title == title).ToList();
            var gameDtos = mapper.Map<IEnumerable<GameDto>>(filteredGames);
            return Ok(gameDtos);
        }
    }
}