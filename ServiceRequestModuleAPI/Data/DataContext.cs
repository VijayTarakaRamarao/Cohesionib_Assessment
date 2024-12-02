using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext {
    public DbSet<ServiceRequest> ServiceRequests { 
        get; 
        set; 
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {
        
     }
}