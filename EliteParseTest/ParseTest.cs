using EliteModels;
using EliteParse.Repository;
using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EliteParseTest
{
    public class ParseFixture: IDisposable {
        public ParseFixture() {
            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");
            ParseClient.Initialize(appKey, netKey);

        }

        public void Dispose() {
            return;
        }
    }

    public class ParseTest : IClassFixture<ParseFixture>
    {
        ParseFixture _parseFixture;

        public ParseTest(ParseFixture parseFixture) {
            _parseFixture = parseFixture;
        }

        [Fact]
        public void ShouldOpenKeys() {
            var appKey = Environment.GetEnvironmentVariable("elitelog_appkey");
            var netKey = Environment.GetEnvironmentVariable("elitelog_netkey");

            Assert.True(appKey.Length > 0);
            Assert.True(netKey.Length > 0);

        }

        [Fact]
        public async void ShouldReturnCurrentExpedition() {
            var parseObject = new ParseObject("Expedition");
            var expRepository = new ExpeditionRespository();
        
            Expedition current = await expRepository.GetCurrent(); 

            Assert.NotNull(current);
            Assert.True(current.Current);
        }
        [Fact]
        public async void ShouldReturnAllExpeditions() {
            var parseObject = new ParseObject("Expedition");
            var expRepository = new ExpeditionRespository();

            IList<Expedition> expeditions = await expRepository.GetAll();

            Assert.NotNull(expeditions);
            Assert.True(expeditions.Count > 0);
        }
    }
}
