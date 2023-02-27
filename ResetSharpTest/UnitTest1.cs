using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace ResetSharpTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class RestSharpTestCase
        {
            RestClient client;

            [TestInitialize]
            public void Setup()
            {
                client = new RestClient("http://localhost:3000");
            }

            private IRestResponse getEmployeeList()
            {
                RestRequest request = new RestRequest("/employees", Method.GET);

                //act

                IRestResponse response = client.Execute(request);
                return response;
            }

            [TestMethod]
            public void onCallingGETApi_ReturnEmployeeList()
            {
                IRestResponse response = getEmployeeList();

                //assert
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
                List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
                Assert.AreEqual(6, dataResponse.Count);
                foreach (var item in dataResponse)
                {
                    System.Console.WriteLine("id: " + item.id + "Name: " + item.name + "Salary: " + item.Salary);
                }
            }


            [TestMethod]
            public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
            {
                RestRequest request = new RestRequest("/employees", Method.POST);
                JObject jObjectbody = new JObject();
                jObjectbody.Add("name", "Clark");
                jObjectbody.Add("Salary", "15000");
                request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

                //act
                IRestResponse response = client.Execute(request);
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual("Clark", dataResponse.name);
                Assert.AreEqual(15000, dataResponse.Salary);

            }

        }



    }
}