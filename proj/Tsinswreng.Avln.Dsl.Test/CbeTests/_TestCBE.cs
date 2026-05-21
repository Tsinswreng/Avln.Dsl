using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.CbeTests;

/// CBE 主測試器: 只負責收編各個 API 的測試分片。
public partial class TestCBE: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = false;
		Node.IsParallelRecursive = false;
		RegisterPth(Node);
		RegisterMk(Node);
		return Node;
	}
}
