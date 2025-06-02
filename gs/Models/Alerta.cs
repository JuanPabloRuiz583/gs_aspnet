using System.ComponentModel.DataAnnotations;

namespace gs.Models
{
    public class Alerta
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(255, ErrorMessage = "A descrição deve ter no máximo 255 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A latitude é obrigatória")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "A longitude é obrigatória")]
        public double Longitude { get; set; }

        [Required(ErrorMessage = "O tipo de evento é obrigatório")]
        public TipoEvento TipoEvento { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório")]
        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<RotaSegura> RotasSeguras { get; set; }
    }
}
