namespace EmployeeManagementSystem.Common
{
    public class Credentials
    {
        public static readonly string CosmoDBUrl = Environment.GetEnvironmentVariable("url"); 
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName"); 
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName"); 
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        internal static readonly string VisitorUrl = Environment.GetEnvironmentVariable("visitorUrl");
        internal static readonly string AddVisitorEndPoint = "/api/Visitor/AddVisitor";
        internal static readonly string GetAllVisitorEndPoint = "api/Visitor/GetAllVisitors";
    }
}
