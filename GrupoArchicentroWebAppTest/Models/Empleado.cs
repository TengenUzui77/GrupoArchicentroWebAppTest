namespace GrupoArchicentroWebAppTest.Models
{
    public class Empleado : Persona
    {
        public int Id { get; set; }

        public string Cargo { get; set; } = string.Empty;

        public string? Salario { get; set; }
    }

}
