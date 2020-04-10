using System.Collections.Generic;

namespace SharedKernel.Domain.Queries
{
    public class PaginatedResult<T> where T : class
    {
        public int TotalPaginas { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }
}
