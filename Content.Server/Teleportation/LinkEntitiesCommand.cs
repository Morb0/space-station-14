using Content.Server.Administration;
using Content.Shared.Administration;
using Content.Shared.Teleportation.Systems;
using Robust.Shared.Toolshed;

namespace Content.Server.Teleportation;

[ToolshedCommand, AdminCommand(AdminFlags.Admin)]
public sealed class LinkEntitiesCommand : ToolshedCommand
{
    [Dependency] private readonly IEntitySystemManager _systemManager = default!;

    [CommandImplementation]
    public bool LinkEntities([PipedArgument] EntityUid firstEntity, [PipedArgument] EntityUid secondEntity, [CommandInverted] bool shouldDelete)
    {
        var linkSystem = _systemManager.GetEntitySystem<LinkedEntitySystem>();
        return linkSystem.TryLink(firstEntity, secondEntity, shouldDelete);
    }
}
