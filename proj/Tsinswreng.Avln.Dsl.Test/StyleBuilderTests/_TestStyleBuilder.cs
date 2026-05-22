using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.StyleBuilderTests;

/// StyleBuilder test manager: collects Sty and StyleBuilder test parts.
public partial class TestStyleBuilder: ITester{
	/// Registers all StyleBuilder test parts.
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = false;
		Node.IsParallelRecursive = false;
		RegisterIs(Node);
		RegisterOfType(Node);
		RegisterSetValue(Node);
		RegisterSetBinding(Node);
		return Node;
	}
}
