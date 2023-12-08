using domain.Enums;

namespace domain.Entities
{
    public class Anabolizante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public TipoBase? Composicao { get; set; }
        public DateTime Vencimento { get; set; }

    }
}