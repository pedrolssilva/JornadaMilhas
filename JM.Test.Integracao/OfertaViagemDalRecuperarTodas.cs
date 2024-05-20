using JornadaMilhas.Dados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace JM.Test.Integracao
{
    [Collection(nameof(ContextoCollection))]
    public class OfertaViagemDalRecuperarTodas : IDisposable
    {
        private readonly ContextoFixture fixture;
       
        public OfertaViagemDalRecuperarTodas(ITestOutputHelper output, ContextoFixture fixture)
        {
            this.fixture = fixture;
            output.WriteLine(this.fixture.GetHashCode().ToString());
        }

        public async void Dispose()
        {
            await fixture.LimpaDadosDoBanco();
        }

        [Fact]
        public void RetornaListaDeOferta()
        {
            //Arrange 
            fixture.CriaDadosFake();
            var dal = new OfertaViagemDAL(fixture.Context);

            //Act 
            var ofertaRecuperada = dal.RecuperarTodas();

            //Assert    
            Assert.NotEmpty(ofertaRecuperada);
        }
    }
}
