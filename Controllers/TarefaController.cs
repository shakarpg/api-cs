using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Enums;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController : ControllerBase
{
    private readonly OrganizadorContext _context;

    public TarefaController(OrganizadorContext context)
    {
        _context = context;
    }

    /// <summary>
    /// GET /Tarefa/{id}
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Tarefa>> ObterPorId([FromRoute] int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();
        return tarefa;
    }

    /// <summary>
    /// GET /Tarefa/ObterTodos
    /// </summary>
    [HttpGet("ObterTodos")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodos()
    {
        var dados = await _context.Tarefas.AsNoTracking().OrderBy(t => t.Id).ToListAsync();
        return Ok(dados);
    }

    /// <summary>
    /// GET /Tarefa/ObterPorTitulo?titulo=abc
    /// </summary>
    [HttpGet("ObterPorTitulo")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorTitulo([FromQuery] string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return BadRequest("Informe um título.");
        var dados = await _context.Tarefas.AsNoTracking()
            .Where(t => t.Titulo.ToLower().Contains(titulo.ToLower()))
            .ToListAsync();
        return Ok(dados);
    }

    /// <summary>
    /// GET /Tarefa/ObterPorData?data=2025-01-31
    /// </summary>
    [HttpGet("ObterPorData")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorData([FromQuery] DateTime data)
    {
        // compara apenas por data (sem hora)
        var dt = data.Date;
        var dados = await _context.Tarefas.AsNoTracking()
            .Where(t => t.Data.Date == dt)
            .ToListAsync();
        return Ok(dados);
    }

    /// <summary>
    /// GET /Tarefa/ObterPorStatus?status=Pendente
    /// </summary>
    [HttpGet("ObterPorStatus")]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterPorStatus([FromQuery] StatusTarefa status)
    {
        var dados = await _context.Tarefas.AsNoTracking()
            .Where(t => t.Status == status)
            .ToListAsync();
        return Ok(dados);
    }

    /// <summary>
    /// POST /Tarefa
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Tarefa>> Criar([FromBody] Tarefa tarefa)
    {
        if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            return BadRequest("Título é obrigatório.");

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
    }

    /// <summary>
    /// PUT /Tarefa/{id}
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] Tarefa tarefa)
    {
        if (id != tarefa.Id) return BadRequest("Id do recurso e do corpo precisam ser iguais.");

        var existente = await _context.Tarefas.FindAsync(id);
        if (existente is null) return NotFound();

        existente.Titulo = tarefa.Titulo;
        existente.Descricao = tarefa.Descricao;
        existente.Data = tarefa.Data;
        existente.Status = tarefa.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// DELETE /Tarefa/{id}
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar([FromRoute] int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa is null) return NotFound();

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
