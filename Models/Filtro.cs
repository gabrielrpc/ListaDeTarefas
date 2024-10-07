namespace ListaDeTarefas.Models
{
    public class Filtro
    {
        public Filtro(string filtroString)
        {
            FiltroString = filtroString ?? "todos-todos-todos";
            string[] filtros = FiltroString.Split("-");

            CategoriaId = filtros[0];
            Vencimento = filtros[1];
            StatusId = filtros[2];
        }

        public string FiltroString { get; set; }
        public string CategoriaId { get; set; }
        public string StatusId { get; set; }
        public string Vencimento { get; set; }


        public bool TemCategoria => CategoriaId.ToLower() != "todos";
        public bool TemVencimento => Vencimento.ToLower() != "todos";
        public bool TemStatus => StatusId.ToLower() != "todos";

        public static Dictionary<string, string> FiltroVencimentos => new Dictionary<string, string>
        {
            {"hoje", "Hoje"},
            {"futuro", "Futuro"},
            {"passado", "Passado"},
        };

        public bool Hoje => Vencimento.ToLower() == "hoje";
        public bool Futuro => Vencimento.ToLower() == "futuro";
        public bool Passado => Vencimento.ToLower() == "passado";
    }
}
