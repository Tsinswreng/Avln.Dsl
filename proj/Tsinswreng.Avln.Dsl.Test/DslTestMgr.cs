using Tsinswreng.Avln.Dsl.Test.CbeTests;
using Tsinswreng.Avln.Dsl.Test.ExtnItemsControlTests;
using Tsinswreng.Avln.Dsl.Test.ExtnTests;
using Tsinswreng.Avln.Dsl.Test.FnConvtrTests;
using Tsinswreng.Avln.Dsl.Test.IViewModelTests;
using Tsinswreng.Avln.Dsl.Test.IViewTests;
using Tsinswreng.Avln.Dsl.Test.PsdClsTests;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test;

/// 測試管理器: 收編 Tsinswreng.Avln.Dsl.Test 下全部 tester。
public class DslTestMgr: DiEtTestMgr{
	public static DslTestMgr Inst = new();

	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		Node.Ordered = false;
		Node.IsParallelRecursive = false;
		this.RegisterTester<TestCBE>();
		this.RegisterTester<TestExtn>();
		this.RegisterTester<TestExtnItemsControl>();
		this.RegisterTester<TestFnConvtr>();
		this.RegisterTester<TestIView>();
		this.RegisterTester<TestIViewModel>();
		this.RegisterTester<TestPsdCls>();
		return Node;
	}
}
