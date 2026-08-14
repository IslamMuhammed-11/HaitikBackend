using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Application.Features.Auth.Login;

public sealed record LoginResponse(string AccessToken, string RefreshToken);
