namespace GrupoArchicentroWebAppTest.Models
{
    public class Persona
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        public string? Ciudad { get; set; }

        public string DNI { get; set; } = string.Empty;
    }
}
