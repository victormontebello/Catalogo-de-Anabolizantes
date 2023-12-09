using domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Anabolizante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public TipoBase? Composicao { get; set; }
        public DateTime Vencimento { get; set; }
        public bool Injetavel { get; set; }
    }
}
