using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;

namespace JuevesanoDapperAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientAPI : ControllerBase
    {
        private readonly SqliteConnection _connection;

        public ClientAPI()
        {
            _connection = new SqliteConnection("Data Source=juevesanoData.db");
        }

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            const string query = "SELECT * FROM Client";
            var result = await _connection.QueryAsync<Client>(query);
            return Ok(result);
        }

        [HttpPost("SaveClient")]
        public async Task<IActionResult> SaveClientAsync(Client client)
        {
            const string query = "INSERT INTO Client (ClientName, Residency) VALUES (@ClientName, @Residency); SELECT * FROM Client ORDER BY Id DESC LIMIT 1";
            var result = await _connection.QuerySingleAsync<Client>(query, client);
            return Ok(result);
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> UpdateClientAsync(int id, Client client)
        {
            const string query = "UPDATE Client SET ClientName = @ClientName, Residency = @Residency WHERE Id = @Id";
            var affectedRows = await _connection.ExecuteAsync(query, new { Id = id, ClientName = client.ClientName, Residency = client.Residency });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return Ok(await _connection.QuerySingleAsync<Client>("SELECT * FROM Client WHERE Id = @Id", new { Id = id }));
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> DeleteClientAsync(int id)
        {
            const string query = "DELETE FROM Client WHERE Id = @Id";
            var affectedRows = await _connection.ExecuteAsync(query, new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return Ok();
        }
    }

    public class Client
    {
        public int Id { get; set; }
        public string? ClientName { get; set; }
        public string? Residency { get; set; }
    }
}
