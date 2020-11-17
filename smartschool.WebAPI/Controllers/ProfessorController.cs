using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartschool.WebAPI.Data;
using smartschool.WebAPI.Models;

namespace smartschool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/professores")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repository;

        public ProfessorController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAllProfessores(true));
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(Professor professor)
        {
            repository.Add(professor);
            if (repository.SaveChanges())
            {
                return Ok("Professor cadastrado com sucesso");
            }
            return BadRequest("Nao foi possivel cadastrar o professor");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var prof = repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Nao foi possivel localizar o professor");
            return Ok(prof);
        }


        [HttpPut("atualizar/{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = repository.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor nao encontrado");

            repository.Update(professor);
            if (repository.SaveChanges())
            {
                return Ok("Atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar");
        }

        [HttpPatch("atualizar/parcial/{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = repository.GetProfessorById(id);
            if (prof == null) return BadRequest("Professor nao encontrado");
            
            repository.Update(professor);
            if(repository.SaveChanges()){
                return Ok("Atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var prof = repository.GetProfessorById(id);
            repository.Delete(prof);

            if(repository.SaveChanges()){
                return Ok("Excluido com sucesso");
            } 
            return BadRequest("Falha ao excluir");
        }

    }
}