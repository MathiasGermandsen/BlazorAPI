namespace BlazorAPI.Classes;

public class ApiClass 
{
    
    public class User : Common
    {
        public string Name { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
    
        public List<Car> Cars { get; set; } = new List<Car>();
    }
    
    public class Car : Common
    {
        public string Vin { get; set; } 
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
    
    public class UserResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public List<ApiClass.User> Users { get; set; }
    }

}