#if false
dotnet publish -c Release -r win-x64
./bin/Release/net10.0/win-x64/publish/Tsinswreng.Avln.Dsl.Test.exe
#endif
using Microsoft.Extensions.DependencyInjection;
using Tsinswreng.CsTreeTest;

namespace Tsinswreng.Avln.Dsl.Test;

public class Program{
	public static IServiceCollection SvcColct = new ServiceCollection();
	public static IServiceProvider SvcProvdr = null!;

	public static async Task Main(string[] args){
		var mgr = DslTestMgr.Inst;
		SvcProvdr = mgr.InitSvc(SvcColct, sc=>sc.BuildServiceProvider());
		ITestExecutor executor = new TreeTestExecutor();
		await executor.RunEtPrint(mgr.TestNode);
		throw new Exception("Test AOT Exception");
	}
}
