﻿using Polly;
using Polly.Extensions.Http;

namespace EmployeesApi;

public static  class SrePolicies
{
    public static IAsyncPolicy<HttpResponseMessage> GetDefaultRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public static IAsyncPolicy<HttpResponseMessage> GetDefaultCircuitBreaker()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
    }
}
