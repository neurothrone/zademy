using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Entities;

namespace Zademy.Persistence.Data;

public class ZademyIdentityDbContext(DbContextOptions options) : IdentityDbContext<UserEntity>(options)
{
}