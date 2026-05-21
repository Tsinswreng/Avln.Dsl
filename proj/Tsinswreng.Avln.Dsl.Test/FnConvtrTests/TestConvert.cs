using System.Globalization;
using Avalonia;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.FnConvtrTests;

public partial class TestFnConvtr{
	/// 測試 Convert 的正常、空委託與異常回調分支。
	public void RegisterConvert(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestFnConvtr),
			[typeof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>)],
			[nameof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>.Convert)],
			nameof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>.Convert)
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Convert_ReturnsConvertedValue", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>$"{v}:{p}");
			var result = conv.Convert(12, typeof(string), "ok", CultureInfo.InvariantCulture);
			T(result is string s && s == "12:ok");
			return null;
		});

		R("Convert_WhenFnConvNull_ReturnsUnsetValue", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>v.ToString()){
				FnConv = null
			};
			var result = conv.Convert(1, typeof(string), null, CultureInfo.InvariantCulture);
			T(ReferenceEquals(result, AvaloniaProperty.UnsetValue));
			return null;
		});

		R("Convert_WhenDelegateThrows_InvokesOnErrAndReturnsUnsetValue", async(o)=>{
			var hit = false;
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>{
				throw new InvalidOperationException("boom");
			});
			conv.OnErr = e=>{
				hit = e is InvalidOperationException;
				return null;
			};
			var result = conv.Convert(1, typeof(string), null, CultureInfo.InvariantCulture);
			T(hit);
			T(ReferenceEquals(result, AvaloniaProperty.UnsetValue));
			return null;
		});
	}
}
