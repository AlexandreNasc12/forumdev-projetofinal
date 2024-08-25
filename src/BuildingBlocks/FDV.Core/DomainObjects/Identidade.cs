using System;
using System.Security.Cryptography;
using System.Text;

namespace FDV.Core.DomainObjects;

public readonly struct Identidade
{
    private readonly Guid _id;

    public Identidade(params object[] keys)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.Default.GetBytes(string.Concat(keys)));
        _id = new Guid(hash);
    }

    public static implicit operator Guid(Identidade identidade) => identidade._id;
}
