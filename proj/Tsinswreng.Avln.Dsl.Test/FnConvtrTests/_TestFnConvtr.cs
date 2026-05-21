using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.FnConvtrTests;

/// FnConvtr 主測試器: 分別測構造器、Convert、ConvertBack。
public partial class TestFnConvtr: ITester{
	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = false;
		RegisterConstructors(Node);
		RegisterConvert(Node);
		RegisterConvertBack(Node);
		return Node;
	}
}
