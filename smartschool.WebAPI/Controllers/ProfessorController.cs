using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartschool.WebAPI.Data;
using smartschool.WebAPI.Dto;
using smartschool.WebAPI.Models;

namespace smartschool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/professores")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var professores = repository.GetAllProfessores(true);
            return Ok(mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = mapper.Map<Professor>(professorRegistrarDto);

            repository.Add(professor);
            if (repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Nao foi possivel cadastrar o professor");
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var professor = repository.GetProfessorById(id, false);
            if (professor == null) return BadRequest("Nao foi possivel localizar o professor");

            var professorDto = mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }


        [HttpPut("atualizar/{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto professorRegistrarDto)
        {
            var professor = repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor nao encontrado");

            mapper.Map(professorRegistrarDto, professor);

            repository.Update(professor);
            if (repository.SaveChanges())
            {
                return Ok("Atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar");
        }

        [HttpPatch("atualizar/parcial/{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto professorRegistrarDto)
        {

            var professor = repository.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor nao encontrado");

            mapper.Map(professorRegistrarDto, professor);

            repository.Update(professor);
            if (repository.SaveChanges())
            {
                return Ok("Atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var prof = repository.GetProfessorById(id);
            repository.Delete(prof);

            if (repository.SaveChanges())
            {
                return Ok("Excluido com sucesso");
            }
            return BadRequest("Falha ao excluir");
        }

    }
}