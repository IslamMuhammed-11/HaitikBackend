using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.Auth.Common;

public sealed record TokensResponse(string AccessToken, string RefreshToken);
