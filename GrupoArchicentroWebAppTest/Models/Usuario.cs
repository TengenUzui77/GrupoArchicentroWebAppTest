namespace GrupoArchicentroWebAppTest.Models
{
    public class Usuario : Persona
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario(string username, string password)           
        {
            Username = username;
            Password = password;
        }
    }
}
