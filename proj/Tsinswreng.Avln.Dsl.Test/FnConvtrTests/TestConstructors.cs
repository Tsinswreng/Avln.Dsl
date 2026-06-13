using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test.FnConvtrTests;

public partial class TestFnConvtr{
	/// 測試三個公開構造器與 IValConvtrWithErr 介面契約。
	public void RegisterConstructors(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestFnConvtr),
			[typeof(global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>)],
			[nameof(RegisterConstructors)],
			"FnConvtr_Ctor"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("Ctor_WithConverterOnly_AssignsFnConv", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>v.ToString());
			T(conv.FnConv is not null);
			T(conv.FnBack is null);
			return null;
		});

		R("Ctor_WithForwardAndBackward_AssignsBothDelegates", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>(
				(v, p)=>v.ToString(),
				(v, p)=>int.Parse(v)
			);
			T(conv.FnConv is not null);
			T(conv.FnBack is not null);
			return null;
		});

		R("Ctor_SimpleDelegates_WrapsParameterlessDelegates", async(o)=>{
			var conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>(
				v=>"#"+v,
				v=>int.Parse(v.TrimStart('#'))
			);
			var converted = conv.Convert(8, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture);
			var convertedBack = conv.ConvertBack("#8", typeof(int), null, System.Globalization.CultureInfo.InvariantCulture);
			T(converted is string s && s == "#8");
			T(convertedBack is int i && i == 8);
			return null;
		});

		R("IValConvtrWithErr_OnErrIsSettable", async(o)=>{
			global::Tsinswreng.Avln.Dsl.IValConvtrWithErr conv = new global::Tsinswreng.Avln.Dsl.FnConvtr<int, string>((v, p)=>v.ToString());
			var hit = false;
			conv.OnErr = e=>{
				hit = true;
				return null;
			};
			_ = conv.OnErr?.Invoke(new Exception("test"));
			T(hit);
			return null;
		});
	}
}
