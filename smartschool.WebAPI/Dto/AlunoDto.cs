using System;
using System.Collections.Generic;
using smartschool.WebAPI.Models;

namespace smartschool.WebAPI.Dto
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataIni { get; set; }
        public Boolean Ativo { get; set; }
    }
}