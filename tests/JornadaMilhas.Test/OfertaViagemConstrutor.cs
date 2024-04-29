using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData("OrigemTeste", "DestinoTeste", "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(null, "São Paulo", "2024-02-01", "2024-01-05", -1, false)]
        [InlineData("Vitória", "São Paulo", "2024-02-01", "2024-01-05", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-02-01", "2024-01-05", -500, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino,
            string dataIda, string dataVolta, double preco, bool validacao)
        {
            // Arrange
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            // Act
            OfertaViagem oferta = new(rota, periodo, preco);

            // Assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaOuPeriodoInvalidosQuandoRotaNula()
        {
            //Arrange 
            Rota rota = null;
            Periodo periodo = new(new(2024, 2, 1), new(2024, 02, 05));
            double preco = 100.0;

            //Act 
            OfertaViagem oferta = new(rota, periodo, preco);

            //Assert    
            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(-250)]
        [InlineData(0)]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorOuIgualAZero(double preco)
        {
            //Arrange 
            Rota rota = new Rota("Origem1", "Destino1");
            Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));

            //Act 
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert    
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }


        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            //Arrange 
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 6, 1), new DateTime(2024, 5, 10));
            double preco = -100;
            int quantidadeEsperada = 3;

            //Act 
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert    
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }
    }
}