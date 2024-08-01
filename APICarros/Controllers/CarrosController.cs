using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace APICarros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrosController : Controller
    {
        [HttpGet]//Verbo http do EndPoint
        public async Task<ActionResult> Get()//EndPoint
        {
            try
            {
                var listaCarros = new List<Carros>();


                var connectionString = "Host=localhost;Username=postgres;Password=12345;Database=postgres";
                await using var dataSource = NpgsqlDataSource.Create(connectionString);

                // Retrieve all rows
                await using (var cmd = dataSource.CreateCommand("SELECT * FROM carros"))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var carro = new Carros
                        {
                            Id = reader.GetInt32(0),// Read 1st collum
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Ano = reader.GetInt32(3),
                            Preco = reader.GetDecimal(4)
                        };

                        listaCarros.Add(carro);
                    }
                }



                return Ok(listaCarros);
            }
            catch (Exception ex)
            {
                //return StatusCode(500); 
                return BadRequest(ex);
            }
        }


        [HttpGet("GetOrdered")]//Verbo http do EndPoint
        public async Task<ActionResult> GetOrdered()// EndPoint
        {
            try
            {
                var listaCarros = new List<Carros>();


                var connectionString = "Host=localhost;Username=postgres;Password=12345;Database=postgres";
                await using var dataSource = NpgsqlDataSource.Create(connectionString);

                // Retrieve all rows
                await using (var cmd = dataSource.CreateCommand("SELECT * FROM carros ORDER BY preco"))
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var carro = new Carros
                        {
                            Id = reader.GetInt32(0),//Read 1st collum
                            Marca = reader.GetString(1),
                            Modelo = reader.GetString(2),
                            Ano = reader.GetInt32(3),
                            Preco = reader.GetDecimal(4)
                        };

                        listaCarros.Add(carro);
                    }
                }



                return Ok(listaCarros);
            }
            catch (Exception ex)
            {
                //return StatusCode(500); 
                return BadRequest(ex);
            }
        }
    }
}
