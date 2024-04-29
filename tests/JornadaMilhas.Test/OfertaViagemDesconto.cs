using JornadaMilhasV1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Theory]
        [InlineData(100, 20)]
        [InlineData(100, 0)]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto(double precoOriginal, double desconto)
        {
            //Arrange 
            Rota rota = new Rota("OrigemA", "DestinoB");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
            double precoComDesconto = precoOriginal - desconto;

            OfertaViagem oferta = new(rota, periodo, precoOriginal);

            //Act 
            oferta.Desconto = desconto;

            //Assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Theory]
        [InlineData(120, 30)]
        [InlineData(100, 30)]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualAoPreco(double desconto, double precoComDesconto)
        {
            //Arrange 
            Rota rota = new Rota("OrigemA", "DestinoB");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
            double precoOriginal = 100.00;

            OfertaViagem oferta = new(rota, periodo, precoOriginal);

            //Act 
            oferta.Desconto = desconto;

            //Assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Theory]
        [InlineData(-120, 100)]
        [InlineData(0, 100)]
        public void RetornaPrecoOriginalQuandoDescontoMenorIgualAZero(double desconto, double precoOriginal)
        {
            //Arrange 
            Rota rota = new Rota("OrigemA", "DestinoB");
            Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));

            OfertaViagem oferta = new(rota, periodo, precoOriginal);

            //Act 
            oferta.Desconto = desconto;

            //Assert
            Assert.Equal(precoOriginal, oferta.Preco, 0.001);
        }
    }
}
