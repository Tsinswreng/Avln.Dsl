using System.Globalization;
using Avalonia;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.FnConvtrTests;

public partial class TestFnConvtr{
	/// 測試 ConvertBack 的正常、空委託與異常回調分支。
	public void RegisterConvertBack(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestFnConvtr),
			[typeof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>)],
			[nameof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>.ConvertBack)],
			nameof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>.ConvertBack)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("ConvertBack_ReturnsConvertedValue", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>(
				(v, p)=>v.ToString(),
				(v, p)=>int.Parse(v)+1
			);
			var result = conv.ConvertBack("9", typeof(int), null, CultureInfo.InvariantCulture);
			T(result is int i && i == 10);
			return null;
		});

		R("ConvertBack_WhenFnBackNull_ReturnsUnsetValue", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>v.ToString());
			var result = conv.ConvertBack("1", typeof(int), null, CultureInfo.InvariantCulture);
			T(ReferenceEquals(result, AvaloniaProperty.UnsetValue));
			return null;
		});

		R("ConvertBack_WhenDelegateThrows_InvokesOnErrAndReturnsUnsetValue", async(o)=>{
			var hit = false;
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>(
				(v, p)=>v.ToString(),
				(v, p)=>{
					throw new InvalidOperationException("back");
				}
			);
			conv.OnErr = e=>{
				hit = e is InvalidOperationException;
				return null;
			};
			var result = conv.ConvertBack("x", typeof(int), null, CultureInfo.InvariantCulture);
			T(hit);
			T(ReferenceEquals(result, AvaloniaProperty.UnsetValue));
			return null;
		});
	}
}
