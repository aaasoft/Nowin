using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static class OwinTaskExtensions
{
    internal static Task ContinueWith(this Task task, Action<Task, object> continuationAction, object state)
    {
        return task.ContinueWith(t => continuationAction(t, state));
    }

    internal static Task ContinueWith<TResult>(this Task<TResult> task, Action<Task<TResult>, object> continuationAction, object state)
    {
        return task.ContinueWith(t => continuationAction(t, state));
    }

    internal static Task WaitAsync(this SemaphoreSlim semaphoreSlim, CancellationToken token)
    {
        return TaskEx.Run(() => semaphoreSlim.Wait(token), token);
    }

    internal static Task AuthenticateAsServerAsync(this SslStream sslStream, X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation)
    {
        return TaskEx.Run(() => sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSslProtocols, checkCertificateRevocation));
    }
}