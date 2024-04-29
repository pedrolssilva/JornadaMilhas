using JornadaMilhas.Dados;
using JornadaMilhasV1.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JM.Test.Integracao
{
    public class OfertaViagemDalAdicionar
    {
        private readonly JornadaMilhasContext context;

        public OfertaViagemDalAdicionar()
        {
            var options = new DbContextOptionsBuilder<JornadaMilhasContext>()
           .UseSqlServer("Server = localhost, 1433; Database = JornadaMilhas; User Id = sa; Password = UoyN^bW61K; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
           .Options;

            context = new JornadaMilhasContext(options);
        }

        private OfertaViagem CriaOfertaViagem()
        {
            Rota rota = new Rota("S�o Paulo", "Fortaleza");
            Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
            double preco = 350;
            return new OfertaViagem(rota, periodo, preco);
        }

        [Fact]
        public void RegistraOfertaNoBanco()
        {
            //Arrange 
            var oferta = CriaOfertaViagem();
            var dal = new OfertaViagemDAL(context);
            
            //Act 
            dal.Adicionar(oferta);

            //Assert
            var ofertaIncluida = dal.RecuperarPorId(oferta.Id);
            Assert.NotNull(ofertaIncluida);
            Assert.Equal(ofertaIncluida.Preco, oferta.Preco, 0.001);
        }

        [Fact]
        public void RegistraOfertaNoBancoComInformacoesCorretas()
        {
            //Arrange 
            var oferta = CriaOfertaViagem();
            var dal = new OfertaViagemDAL(context);

            //Act 
            dal.Adicionar(oferta);

            //Assert
            var ofertaIncluida = dal.RecuperarPorId(oferta.Id);
            Assert.Equal(ofertaIncluida.Rota.Origem, oferta.Rota.Origem);
            Assert.Equal(ofertaIncluida.Rota.Destino, oferta.Rota.Destino);
            Assert.Equal(ofertaIncluida.Periodo.DataInicial, oferta.Periodo.DataInicial);
            Assert.Equal(ofertaIncluida.Periodo.DataFinal, oferta.Periodo.DataFinal);
            Assert.Equal(ofertaIncluida.Preco, oferta.Preco, 0.001);
        }
    }
}