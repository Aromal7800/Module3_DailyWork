using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEx;
using RestSharp;


string baseUrl = "https://reqres.in/api/";
var client=new RestClient(baseUrl);
/*
GetUser(client);
CreateUser(client);
DeleteUser(client);
UpdateUser(client);
static void GetUser(RestClient client)
{
    var getUserRequest = new RestRequest("users/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine(getUserResponse.Content);

}

static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("users", Method.Post);

    createUserRequest.AddParameter("name", "John Doe");
    createUserRequest.AddParameter("job", "Software Developer");
    var createUserResponse = client.Execute(createUserRequest);

    Console.WriteLine("POST User Response :\n");
    Console.WriteLine(createUserResponse.Content);
}

static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("user/2", Method.Put);
    updateUserRequest.AddParameter("name", "John doe");
    updateUserRequest.AddParameter("job", "Senior Software Developer");

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update Response :");
    Console.WriteLine(updateUserResponse.Content);
}
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("user/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete User Response");
    Console.WriteLine(deleteUserResponse.Content);
}
GetUsers(client);
createUsers(client);   
UpdateUsers(client);
DeleteUser(client);




static void GetUsers(RestClient client)
{

    var getUserRequest = new RestRequest("users/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
        JObject? userJson = JObject.Parse(getUserResponse.Content);
        // string? pageno = userJson["page"]?.ToString();
        string? userName = userJson["data"]?["first_name"]?.ToString();
        string? userLastName = userJson["data"]?["last_name"]?.ToString();

        Console.WriteLine($"User Name : {userName} {userLastName}");

    }
    else
    {
        Console.WriteLine($"Error : {getUserResponse.ErrorMessage}");
    }

}







static void createUsers(RestClient client)
{
    var createUserRequest = new RestRequest("users", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { name = "John Doe", job = "Software Developer" });
    //createUserRequest.AddParameter("name", "John Doe");
    //createUserRequest.AddParameter("job", "Software Developer");
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("POST User Response :\n");
    Console.WriteLine(createUserResponse.Content);
}

static void UpdateUsers(RestClient client)
{

    var updateUserRequest = new RestRequest("users/2", Method.Put);

    updateUserRequest.AddHeader("Content-Type", "application/json");

    updateUserRequest.AddJsonBody(new { name = "John Doe updated ", job = "Senior Software Developer" });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update Response :");
    Console.WriteLine(updateUserResponse.Content);

}

static void deleteUsers(RestClient client   )
{
    var deleteUserRequest = new RestRequest("users/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete User Response");
    Console.WriteLine(deleteUserResponse.Content);
}
*/
GetUsers(client);
static void GetUsers(RestClient client)
{

    var getUserRequest = new RestRequest("users/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        var response=JsonConvert.DeserializeObject<UserDataResponse>(getUserResponse.Content);
        UserData? user = response?.Data;
        // string? pageno = userJson["page"]?.ToString();

        Console.WriteLine($"User ID : {user?.Id}");
        Console.WriteLine($"User Email :{user?.Email}");
        Console.WriteLine($"User Name : {user?.FirstName} {user?.LastName}");
        Console.WriteLine($"User Avatar : {user?.Avatar}");

    }
    else
    {
        Console.WriteLine($"Error : {getUserResponse.ErrorMessage}");
    }

}
