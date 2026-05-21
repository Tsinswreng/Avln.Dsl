using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.IViewModelTests;

/// IViewModel 主測試器: 分開測兩個 Bind 重載。
public partial class TestIViewModel: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		RegisterBindByAvaloniaProperty(Node);
		RegisterBindByPropertySelector(Node);
		return Node;
	}
}
