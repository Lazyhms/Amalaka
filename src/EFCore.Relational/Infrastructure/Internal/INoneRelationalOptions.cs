﻿namespace Microsoft.EntityFrameworkCore.Infrastructure.Internal;

public interface INoneRelationalOptions : ISingletonOptions
{
    public bool NoneForeignKey { get; set; }

    public SoftDeleteOptions SoftDeleteOptions { get; set; }
}
