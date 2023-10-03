//using SilverSolutions1151.Data;

//namespace SilverSolutions1151
//{
//    public class misc
//    {
//        private static void CreateDbIfNotExists(IHost host)
//        {
//            using (var scope = host.Services.CreateScope())
//            {
//                var services = scope.ServiceProvider;

//                try
//                {
//                    var context = services.GetRequiredService<ApplicationDbContext>();
//                    context.Database.EnsureCreated();
//                }
//                catch (Exception ex)
//                {
//                    var logger = services.GetRequiredService<ILogger<Program>>();
//                    logger.LogError(ex, "An error occurred creating the DB.");
//                }
//            }
//        }
//    }
   

//}
