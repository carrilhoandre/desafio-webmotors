namespace Anuncios.Domain.Commands
{
    public class ConsultarCommand
    {
        public ConsultarCommand()
        {
            Pagina = 1;
        }

        public int Pagina { get; set; }
    }
}
