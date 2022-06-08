// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0.

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Abstraction for caching client access tokens.
    /// </summary>
    public interface IClientAccessTokenCache
    {
        /// <summary>
        ///     Caches a client access token
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="accessToken"></param>
        /// <param name="expiresIn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void SetToken(string clientName, string accessToken, int expiresIn, ClientAccessTokenParameters parameters);

        /// <summary>
        ///     Retrieves a client access token from the cache
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ClientAccessToken GetToken(string clientName, ClientAccessTokenParameters parameters);

        /// <summary>
        ///     Deletes a client access token from the cache
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void DeleteToken(string clientName, ClientAccessTokenParameters parameters);
    }
}