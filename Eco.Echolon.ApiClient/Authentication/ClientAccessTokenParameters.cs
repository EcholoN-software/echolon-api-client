// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0.

namespace Eco.Echolon.ApiClient.Authentication
{
    /// <summary>
    ///     Additional optional parameters for a client access token request
    /// </summary>
    public class ClientAccessTokenParameters
    {
        /// <summary>
        ///     Force renewal of token.
        /// </summary>
        public bool ForceRenewal { get; set; }
    }
}