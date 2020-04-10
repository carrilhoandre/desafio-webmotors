using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Repositories
{
    public interface IRepository <TEntity> : IDisposable where TEntity : class
    {
        void Adicionar(TEntity obj);
        void AdicionarVarios(IEnumerable<TEntity> obj);
        Task<TEntity> ObterPeloIdAsync(int id);
        void Excluir(int id);
        void ExcluirVarios(IEnumerable<TEntity> obj);
        Task<int> SalvarAsync();
        void Alterar(TEntity obj);
    }
}
