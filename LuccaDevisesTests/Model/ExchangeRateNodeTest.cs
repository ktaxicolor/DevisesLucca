using LuccaDevises;
using NUnit.Framework;

namespace LuccaDevisesTests.Model
{
    [TestFixture]
    public class ExchangeRateNodeTest
    {
        private ExchangeRateNode parent;



        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            parent = TestHelper.GetRootExchangeRateNode();
        }

        #region Adding child
        [Test]
        public void Should_add_child_in_node_children_collection()
        {
            var childToAdd = TestHelper.GetExchangeRateNode();
            parent.AddChild(childToAdd);

            Assert.Contains(childToAdd, parent.Children);
        }
        #endregion

        #region Siblings
        [Test]
        public void Should_return_collection_of_siblings_if_parent_exists()
        {
            var child1 = TestHelper.GetChildExchangeRateNode(parent);
            var child2 = TestHelper.GetChildExchangeRateNode(parent);
            parent.AddChild(child1);
            parent.AddChild(child2);

            Assert.IsNotNull(child1.Siblings);
            Assert.IsNotEmpty(child1.Siblings);

            var sibling = child1.Siblings.Find(x => x==child2);
            Assert.IsNotNull(sibling);
        }

        [Test]
        public void Should_return_empty_collection_if_parent_doesnt_exist()
        {
            Assert.IsNotNull(parent.Siblings);
        }
        #endregion

        #region Marking as visited
        [Test]
        public void Should_mark_node_as_visited()
        {
            var node = TestHelper.GetExchangeRateNode();
            node.MarkAsVisited();

            Assert.IsFalse(node.IsNotVisitedYet());
        }

        [Test]
        public void Should_mark_a_new_node_as_not_visited()
        {
            var node = TestHelper.GetExchangeRateNode();

            Assert.IsTrue(node.IsNotVisitedYet());
        }

        #endregion


    }
}
