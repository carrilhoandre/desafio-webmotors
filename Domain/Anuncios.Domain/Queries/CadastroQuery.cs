using SharedKernel.Domain.Queries;

namespace Anuncios.Domain.Queries
{
    public class CadastroQuery : ICommandResult
    {
        public string Mensagem { get; set; }
    }
}
