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
    [Route("api")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet("alunos")]
        public IActionResult Get()
        {
            var alunos = repository.GetAllAlunos(true);
            return Ok(mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("aluno/{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno = repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Nao foi possivel encontrar o Aluno!");

            var dto = mapper.Map<AlunoDto>(aluno);

            return Ok(dto);
        }

        [HttpPost("cadastrar")]
        public IActionResult Post(AlunoDto model)
        {
            var aluno = mapper.Map<Aluno>(model);

            repository.Add(aluno);
            if (repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", mapper.Map<AlunoDto>(aluno));
            }

            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alu = repository.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno nao encontrado");

            repository.Update(aluno);
            if (repository.SaveChanges())
            {
                return Ok("Aluno atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar aluno");
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = repository.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno nao encontrado");

            repository.Delete(aluno);
            if (repository.SaveChanges())
            {
                return Ok("Aluno deletado com sucesso");
            }
            return BadRequest("Falha ao excluir aluno");
        }

        [HttpPatch("atualizar/parcial/{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            var alu = repository.GetAlunoById(id);
            if (alu == null) return BadRequest("Aluno nao encontrado");

            repository.Update(aluno);
            if (repository.SaveChanges())
            {
                return Ok("Aluno atualizado com sucesso");
            }
            return BadRequest("Falha ao atualizar aluno");
        }
    }
}