using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<DAL.EF.GymDbContext>();
optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GymManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true");

using var context = new DAL.EF.GymDbContext(optionsBuilder.Options);
var ptPayments = context.Payments.Where(p => p.PackageName != null && p.PackageName.StartsWith("PT Fee")).ToList();
foreach(var p in ptPayments)
{
    Console.WriteLine($"Payment {p.Id}: Status={p.PaymentStatus}, AmountPaid={p.AmountPaid}, MemberId={p.MemberId}, Package={p.PackageName}, DueDate={p.DueDate}");
}
