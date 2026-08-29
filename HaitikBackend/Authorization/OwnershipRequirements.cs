using Microsoft.AspNetCore.Authorization;

namespace HaitikBackend.Authorization;

public sealed class OrderOwnershipRequirement : IAuthorizationRequirement;

public sealed class AgencyOwnershipRequirement : IAuthorizationRequirement;
