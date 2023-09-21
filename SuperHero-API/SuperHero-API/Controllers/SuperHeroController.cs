using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero_API.DataAccess;
using SuperHero_API.Models;

namespace SuperHero_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{
    private readonly AppDbContext _context;

    public SuperHeroController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
    {
        return Ok(await _context.SuperHeros.ToListAsync());
    }
    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero newSuperHero)
    {
        await _context.SuperHeros.AddAsync(newSuperHero);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero existSuperHero)
    {
        var superHero = await _context.SuperHeros.FindAsync(existSuperHero.Id);
        if (superHero is null)
            return NotFound();
        superHero.Name = existSuperHero.Name;
        superHero.FirstName = existSuperHero.FirstName;
        superHero.LastName = existSuperHero.LastName;
        superHero.Place = existSuperHero.Place;
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
    {
        var dbHero = await _context.SuperHeros.FindAsync(id);
        if (dbHero == null)
            return BadRequest("Hero not found.");

        _context.SuperHeros.Remove(dbHero);
        await _context.SaveChangesAsync();

        return Ok(await _context.SuperHeros.ToListAsync());
    }
}
