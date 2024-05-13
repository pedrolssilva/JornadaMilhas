using Bogus;
using JornadaMilhas.Dados;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDalRecuperaMaiorDesconto
    {
        private readonly JornadaMilhasContext context;
        private readonly ContextoFixture fixture;

        public OfertaViagemDalRecuperaMaiorDesconto(ContextoFixture fixture)
        {
            context = fixture.Context;
            this.fixture = fixture;
        }

        [Fact]
        // destino = são paulo, desconto = 40, preco 80
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
        {
            //Arrange 
            var rota = new Rota("Curitiba", "São Paulo");
            Periodo periodo = new(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
            fixture.CriaDadosFake();

            var ofertaEscolhida = new OfertaViagem(rota, periodo, 80)
            {
                Desconto = 40,
                Ativa = true
            };

            var dal = new OfertaViagemDAL(context);
            dal.Adicionar(ofertaEscolhida);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
            var precoEsperado = 40;

            //Act 
            var oferta = dal.RecuperaMaiorDesconto(filtro);

            //Assert
            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
        }
    }
}
