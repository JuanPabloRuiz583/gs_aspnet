using System.ComponentModel.DataAnnotations;

namespace gs.dtos
{
    public class RotaSeguraDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A distância é obrigatória")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "A distância deve ser maior que 0")]
        public double DistanciaKm { get; set; }

        [MaxLength(255, ErrorMessage = "A observação deve ter no máximo 255 caracteres")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "O ID do abrigo é obrigatório")]
        public long AbrigoId { get; set; }

        [Required(ErrorMessage = "O ID do alerta é obrigatório")]
        public long AlertaId { get; set; }
    }
}
