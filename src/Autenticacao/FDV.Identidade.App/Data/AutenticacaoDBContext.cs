using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FDV.Identidade.App.Data;

public class AutenticacaoDBContext : IdentityDbContext
{
    public AutenticacaoDBContext(DbContextOptions<AutenticacaoDBContext> options) : base(options){}
}
