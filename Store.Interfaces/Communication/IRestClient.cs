using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Tiny.RestClient;

namespace Store.Interfaces.Communication
{
    public interface IRestClient
    {
        //
        // Summary:
        //     Create a new DELETE request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest DeleteRequest(string route = null);
        //
        // Summary:
        //     Create a new GET request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest GetRequest(string route = null);
        //
        // Summary:
        //     Create a new request.
        //
        // Parameters:
        //   httpMethod:
        //     The httpMethod.
        //
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest NewRequest(HttpMethod httpMethod, string route = null);
        //
        // Summary:
        //     Create a new PATCH request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest PatchRequest(string route = null);
        //
        // Summary:
        //     Create a new PATCH request.
        //
        // Parameters:
        //   content:
        //     The content of the request.
        //
        //   serializer:
        //     The serializer use to serialize it.
        //
        //   compression:
        //     Add compresion system use to compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PatchRequest<TContent>(TContent content, IFormatter serializer = null, ICompression compression = null);
        //
        // Summary:
        //     Create a new PATCH request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        //   content:
        //     The content of the request.
        //
        //   serializer:
        //     The serializer use to serialize it.
        //
        //   compression:
        //     Add compresion system use ton compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PatchRequest<TContent>(string route, TContent content, IFormatter serializer = null, ICompression compression = null);
        //
        // Summary:
        //     Create a new POST request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest PostRequest(string route = null);
        //
        // Summary:
        //     Create a new POST request.
        //
        // Parameters:
        //   content:
        //     The content of the request.
        //
        //   formatter:
        //     The formatter use to serialize the content.
        //
        //   compression:
        //     Add compresion system use to compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PostRequest<TContent>(TContent content, IFormatter formatter = null, ICompression compression = null);
        //
        // Summary:
        //     Create a new POST request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        //   content:
        //     The content of the request.
        //
        //   formatter:
        //     The formatter use to serialize the content.
        //
        //   compression:
        //     Add compresion system use to compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PostRequest<TContent>(string route, TContent content, IFormatter formatter = null, ICompression compression = null);
        //
        // Summary:
        //     Create a new PUT request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        // Returns:
        //     The new request.
        IRequest PutRequest(string route = null);
        //
        // Summary:
        //     Create a new PUT request.
        //
        // Parameters:
        //   content:
        //     The content of the request.
        //
        //   formatter:
        //     The formatter use to serialize the content.
        //
        //   compression:
        //     Add compresion system use to compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PutRequest<TContent>(TContent content, IFormatter formatter = null, ICompression compression = null);
        //
        // Summary:
        //     Create a new PUT request.
        //
        // Parameters:
        //   route:
        //     The route.
        //
        //   content:
        //     The content of the request.
        //
        //   formatter:
        //     The formatter use to serialize the content.
        //
        //   compression:
        //     Add compresion system use to compress content.
        //
        // Returns:
        //     The new request.
        IParameterRequest PutRequest<TContent>(string route, TContent content, IFormatter formatter = null, ICompression compression = null);
    }
}
