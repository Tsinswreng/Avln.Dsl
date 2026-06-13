using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.ExtnTests;

/// Extn 主測試器: 每個分片各管一組 API。
public partial class TestExtn: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = false;
		Node.IsParallelRecursive = false;
		RegisterProp(Node);
		RegisterCollectionAdds(Node);
		RegisterContentAndStyle(Node);
		RegisterAlign(Node);
		RegisterBindingAndGrid(Node);
		return Node;
	}
}
