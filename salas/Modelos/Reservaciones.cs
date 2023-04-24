namespace salas.Modelos
{
    public class Reservaciones
    {
        public int Id { get; set; }
        public int IdRervacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public Boolean Liberado { get; set; }

    }
}
