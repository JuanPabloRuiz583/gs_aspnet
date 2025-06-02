using System.ComponentModel.DataAnnotations;

namespace gs.dtos
{
    public class AbrigoDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório")]
        [MaxLength(255, ErrorMessage = "O endereço deve ter no máximo 255 caracteres")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "A capacidade máxima é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "A capacidade máxima deve ser no mínimo 1")]
        public int CapacidadeMaxima { get; set; }

        [Required(ErrorMessage = "A capacidade atual é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "A capacidade atual deve ser no mínimo 0")]
        public int CapacidadeAtual { get; set; }

        public bool Ativo { get; set; }
    }
}
