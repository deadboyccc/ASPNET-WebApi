// TESTING | NOT PART OF THE PROJECT
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

// I'm using the REST v1 Api of Frappe, v2 is 100% better but this is just simpler
[Route("api/ffc-customer")]
[ApiController]
public class FfcCustomerController : ControllerBase
{
    // the httpclient to make the requests
    private readonly HttpClient _httpClient;

    // the base url for the frappe api and the api key and secret
    private const string FrappeApiBaseUrl = "http://loan.localhost:8000/api/resource/ffc-customer";

    // your api key and secret [Replace these] | i'm using my local secrets and my local url
    private const string ApiKey = "4b039d6c82d7cff";
    private const string ApiSecret = "363db236e523699";

    public FfcCustomerController(HttpClient httpClient)
    {
        _httpClient = httpClient;

        // Set the Authorization header in the correct format
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("token", $"{ApiKey}:{ApiSecret}");
    }


    // GET: api/ffc-customer/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var response = await _httpClient.GetAsync($"{FrappeApiBaseUrl}/{id}");

        // if not found
        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());


        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    // POST: api/ffc-customer
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] object customerData)
    {
        // no validation, repo pattern or auto mapper, just proof of concept
        var jsonContent = new StringContent(JsonSerializer.Serialize(customerData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(FrappeApiBaseUrl, jsonContent);

        // in case of response error
        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    // PUT: api/ffc-customer/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(string id, [FromBody] object updatedData)
    {
        var jsonContent = new StringContent(JsonSerializer.Serialize(updatedData), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"{FrappeApiBaseUrl}/{id}", jsonContent);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    // DELETE: api/ffc-customer/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        var response = await _httpClient.DeleteAsync($"{FrappeApiBaseUrl}/{id}");

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

        return Ok("Deleted successfully");
    }
}