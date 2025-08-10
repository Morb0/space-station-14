using System.Diagnostics.CodeAnalysis;
using System.Text;
using Content.Shared.Humanoid;
using Content.Shared.Preferences;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared.Roles;

/// <summary>
/// Requires a character to have a specific sex for role
/// </summary>
[UsedImplicitly]
[Serializable, NetSerializable]
public sealed partial class SexRequirement : JobRequirement
{
    [DataField(required: true)]
    public Sex Sex = Sex.Unsexed;

    public override bool Check(IEntityManager entManager,
        IPrototypeManager protoManager,
        HumanoidCharacterProfile? profile,
        IReadOnlyDictionary<string, TimeSpan> playTimes,
        [NotNullWhen(false)] out FormattedMessage? reason)
    {
        reason = new FormattedMessage();

        if (profile is null) //the profile could be null if the player is a ghost. In this case we don't need to block the role selection for ghostrole
            return true;

        // Just any
        if (profile.Sex == Sex.Unsexed)
            return true;

        reason = FormattedMessage.FromMarkupPermissive(Loc.GetString("role-timer-specific-sex", ("sex", Sex)));

        return profile.Sex == Sex;
    }
}
