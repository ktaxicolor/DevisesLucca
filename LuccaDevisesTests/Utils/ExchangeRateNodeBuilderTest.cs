using LuccaDevises;
using NUnit.Framework;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace LuccaDevisesTests.Utils
{
    [TestFixture]
    public class ExchangeRateNodeBuilderTest
    {
        private FileData data;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            data = TestHelper.GetFileData();    
        }

        [Test]
        public void Should_return_root_node_of_built_node_tree()
        {
            var rootOfBuiltTree = ExchangeRateNodeBuilder.BuildFromFileData(data);

            Assert.IsNull(rootOfBuiltTree.Root);
            Assert.IsNotEmpty(rootOfBuiltTree.Children);
        }


        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Should_create_node_tree_given_data_file()
        {
            Approvals.Verify(TestHelper.PrintExchangeNodeTree(ExchangeRateNodeBuilder.BuildFromFileData(data)));
        }
        
}
}
