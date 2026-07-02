using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories;
using DAL.EF;
using BLL.Services;
using DAL.EF.Models;

var optionsBuilder = new DbContextOptionsBuilder<GymDbContext>();
optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GymManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true");

using var context = new GymDbContext(optionsBuilder.Options);

var p = context.Payments.FirstOrDefault(p => p.PaymentStatus == "Paid" && p.PackageName != null && p.PackageName.StartsWith("PT Fee"));
if (p != null)
{
    Console.WriteLine($"Found paid payment {p.Id}. Reverting to Unpaid...");
    
    // Test the repository method directly
    var repo = new PaymentRepo(context);
    
    var pToUpdate = await repo.GetByIdAsync(p.Id);
    pToUpdate.AmountPaid = 0;
    pToUpdate.PaymentStatus = "Unpaid";
    pToUpdate.PaymentDate = null;
    
    await repo.UpdateAsync(pToUpdate);
    
    Console.WriteLine("UpdateAsync completed.");
    
    // Verify
    using var context2 = new GymDbContext(optionsBuilder.Options);
    var pVerify = context2.Payments.FirstOrDefault(x => x.Id == p.Id);
    Console.WriteLine($"Verified Status: {pVerify.PaymentStatus}");
}
else
{
    Console.WriteLine("No Paid PT payments found.");
}
