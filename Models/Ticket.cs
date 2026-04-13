namespace TicketManagement.Models
{
    public class Ticket
    {
        public int IdTicket { get; set; }
        public string Curp { get; set; } = string.Empty;
        public int NumTurno { get; set; }
        public int IdMunicipio { get; set; }
        public string Tramitante { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public int IdNivel { get; set; }
        public int IdAsunto { get; set; }
        public int Resuelto { get; set; }
    }
}
