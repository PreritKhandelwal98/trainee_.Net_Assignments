namespace EmployeeManagementSystem.Common
{
    public class Credentials
    {
        public static readonly string CosmoDBUrl = Environment.GetEnvironmentVariable("url"); 
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("containerName"); 
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName"); 
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        internal static readonly string EmployeeUrl = Environment.GetEnvironmentVariable("employeeUrl");
        internal static readonly string AddEmployeeEndPoint = "/api/EmployeeBasicDetails/AddBasicDetail";
        internal static readonly string GetAllEmployeesEndPoint = "api/Employee/GetAllEmployees";
    }
}
