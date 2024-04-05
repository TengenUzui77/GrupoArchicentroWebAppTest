namespace GrupoArchicentroWebAppTest.Models
{
    public class Cliente : Persona
    {
        public int NumeroCliente { get; set; }
        public string CorreoElectronico { get; set; }
        public Cliente( int numeroCliente, string correoElectronico)
           
        {
            NumeroCliente = numeroCliente;
            CorreoElectronico = correoElectronico;
        }
    }
}
