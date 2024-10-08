using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FDV.Identidade.App.Data;

public class AutenticacaoDbContext : IdentityDbContext
{
    public AutenticacaoDbContext(DbContextOptions<AutenticacaoDbContext> options) : base(options) { }
}