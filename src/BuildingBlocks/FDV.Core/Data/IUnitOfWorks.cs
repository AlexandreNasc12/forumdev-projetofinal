using System;

namespace FDV.Core.Data;

public interface IUnitOfWorks
{
    Task<bool> Commit();
}