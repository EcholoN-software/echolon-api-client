// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0.

using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Abstraction for token endpoint operations
    /// </summary>
    public interface ITokenEndpointService
    {
        /// <summary>
        ///     Requests a client access token.
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TokenResponse> RequestClientAccessToken(string clientName = default,
            ClientAccessTokenParameters parameters = default,
            CancellationToken cancellationToken = default);
    }
}