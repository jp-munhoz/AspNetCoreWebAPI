using System;

namespace smartschool.WebAPI.Dto
{
    public class ProfessorDto
    {
         public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataIni { get; set; }
        public Boolean Ativo { get; set; }
       
    }
}