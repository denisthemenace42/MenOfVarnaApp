using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MenOFVarna.Web.Data
{
    public class MenOfVarnaAppDbContext : IdentityDbContext
    {
        public MenOfVarnaAppDbContext(DbContextOptions<MenOfVarnaAppDbContext> options)
            : base(options)
        {
        }
    }
}
