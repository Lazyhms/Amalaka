﻿using Amalaka.EntityFrameworkCore.Infrastructure;
using Amalaka.EntityFrameworkCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure.Internal;

public class NoneRelationalOptions : INoneRelationalOptions
{
    public bool NoneForeignKey { get; set; }

    public SoftDeleteOptions SoftDelete { get; set; }

    public NoneRelationalOptions()
    {
        NoneForeignKey = false;
        SoftDelete = new SoftDeleteOptions();
    }

    public void Initialize(IDbContextOptions options)
    {
        var optionsExtension = (NoneRelationalOptionsExtension?)options.Extensions.FirstOrDefault(f => f is NoneRelationalOptionsExtension) ?? new NoneRelationalOptionsExtension();
        NoneForeignKey = optionsExtension!.NoneForeignKey;
        SoftDelete = optionsExtension!.SoftDelete;
    }

    public void Validate(IDbContextOptions options)
    {
        var optionsExtension = (NoneRelationalOptionsExtension?)options.Extensions.FirstOrDefault(f => f is NoneRelationalOptionsExtension) ?? new NoneRelationalOptionsExtension();
        if (!Equals(NoneForeignKey, optionsExtension!.NoneForeignKey))
        {
        }
    }
}