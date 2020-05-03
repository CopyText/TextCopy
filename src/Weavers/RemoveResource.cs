using System.Collections.Generic;
using System.Linq;
using Fody;

public class ModuleWeaver :
    BaseModuleWeaver
{
    public override void Execute()
    {
        var types = ModuleDefinition.Types;
        var resourceType = types.SingleOrDefault(x => x.Name == "Resource");
        if (resourceType != null)
        {
            types.Remove(resourceType);
        }
    }

    public override IEnumerable<string> GetAssembliesForScanning()
    {
        yield break;
    }
}