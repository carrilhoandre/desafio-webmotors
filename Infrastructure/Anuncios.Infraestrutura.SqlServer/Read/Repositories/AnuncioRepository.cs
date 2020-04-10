using Anuncios.Domain.Commands;
using Anuncios.Domain.Queries;
using Anuncios.Domain.Repositories.Read;
using Anuncios.Infrastructure.SqlServer.Read.DbConnections;
using Dapper;
using SharedKernel.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Anuncios.Infrastructure.SqlServer.Read.Repositories
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private AnunciosDbConnection _connection;

        public AnuncioRepository(AnunciosDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<PaginatedResult<AnuncioQuery>> ConsultarAsync(ConsultarCommand consultarCommand)
        {
            var salto = 0;
            if (consultarCommand.Pagina > 1)
                salto = (consultarCommand.Pagina - 1) * 10;
            var query = await _connection
                .Connection
                .QueryAsync<AnuncioQuery>($@"SELECT ID,marca,modelo,versao,ano
                                             FROM tb_AnuncioWebmotors (nolock)
                                             ORDER BY marca,modelo,versao
                                             OFFSET {salto} ROWS FETCH NEXT 10 ROWS ONLY");


            var result = new PaginatedResult<AnuncioQuery>();
            result.Itens = query;
            result.Total = await _connection.Connection.QuerySingleAsync<int>($@"SELECT count(ID)
                                                                                 FROM tb_AnuncioWebmotors(nolock)");
            result.Pagina = consultarCommand.Pagina;
            result.TotalPaginas = result.Total / 10 < 1 ? 1 : (int)Decimal.Ceiling(result.Total / 10);
            return result;
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _connection
                .Connection
                .QuerySingleAsync<int>($@"SELECT COUNT(ID)
                                             FROM tb_AnuncioWebmotors (nolock)
                                             WHERE ID = {id}") > 0;
        }

        public async Task<AnuncioQuery> ObterPeloIdAsync(int id)
        {
            return await _connection
                 .Connection
                 .QuerySingleAsync<AnuncioQuery>($@"SELECT ID,marca,modelo,versao,ano,quilometragem,observacao
                                             FROM tb_AnuncioWebmotors (nolock)
                                             WHERE ID = {id}");
        }
    }
}
