using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            // Arrange
            Rota rota = new Rota("OrigemTeste", "DestinoTeste");
            Periodo periodo = new(new(2024, 2, 1), new(2024, 02, 05));
            double preco = 100.0;
            var validacao = true;

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

        [Fact]
        public  void etornaMensagemDeErroDePrecoInvalidoQuandoPrecoMenorQueZero()
        {
            //Arrange 
            Rota rota = new Rota("Origem1", "Destino1");
            Periodo periodo = new Periodo(new DateTime(2024, 8, 20), new DateTime(2024, 8, 30));
            double preco = -250;

            //Act 
            OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);

            //Assert    
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
        }
    }
}