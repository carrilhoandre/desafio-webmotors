namespace Anuncios.Domain.Entities
{
    public class Anuncio : Entity
    {
        public Anuncio(string marca,
                       string modelo,
                       string versao, 
                       int ano, 
                       int quilometragem, 
                       string observacao)
        {
            Marca = marca;
            Modelo = modelo;    
            Versao = versao;
            Ano = ano;
            Quilometragem = quilometragem;
            Observacao = observacao;
        }

        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Versao { get; private set; }
        public int Ano { get; private set; }
        public int Quilometragem { get; private set; }
        public string Observacao { get; private set; }

        public void Alterar(string marca,
                       string modelo,
                       string versao,
                       int ano,
                       int quilometragem,
                       string observacao)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Versao = versao;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.Observacao = observacao;
        }
    }
}
