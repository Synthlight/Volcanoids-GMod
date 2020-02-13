using System;
using System.Linq;
using System.Reflection;

namespace Test {
    public static class Program {
        [STAThread]
        public static void Main() {
            var patches = Assembly.GetAssembly(typeof(GMod.GMod))
                                  .GetTypes()
                                  .Where(type => type.Namespace == "GMod.Patches" && type.Name.EndsWith("Patch"))
                                  .ToList();

            if (patches.Count == 0) throw new Exception("No patches found.");

            if (patches.Select(patch => patch.GetMethod("TargetMethod", BindingFlags.Static | BindingFlags.Public)?.Invoke(null, null))
                       .Any(methodInfo => methodInfo == null)) {
                throw new Exception("No patches found.");
            }
        }
    }
}