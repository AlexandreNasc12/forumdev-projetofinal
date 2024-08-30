using System;
using FDV.Core.Data;

namespace FDV.Usuarios.App.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>, IDisposable
{

}
