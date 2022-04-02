using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WebApiBook.Models;

namespace WebApiBook.Controllers
{
    [ApiController]

    [Route("api/book")]

    public class BookController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var url = "https://fakerestapi.azurewebsites.net/api/v1/books";

            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    var respuestaString = await respuesta.Content.ReadAsStringAsync();
                    var listModel = JsonSerializer.Deserialize<List<Book>>(respuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Ok(listModel);
                }
                else
                {
                    var code = ReturnStatusCode(respuesta.StatusCode);
                    return code;
                }
            }
                  
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var url = "https://fakerestapi.azurewebsites.net/api/v1/books";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.GetAsync($"{url}/{id}");
                if (respuesta.IsSuccessStatusCode)
                {
                    var respuestaString = await respuesta.Content.ReadAsStringAsync();
                    var model = JsonSerializer.Deserialize<Book>(respuestaString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return Ok(model);
                }

                else
                {
                   var code = ReturnStatusCode(respuesta.StatusCode);
                    return code;
                }

            }
        }



        [HttpPost]
        public async Task<ActionResult> Post(Book book)
        {
            var url = "https://fakerestapi.azurewebsites.net/api/v1/books";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.PostAsJsonAsync(url, book);

                if (respuesta.IsSuccessStatusCode)
                {               
                    return Ok();
                }

                else
                {
                    var code = ReturnStatusCode(respuesta.StatusCode);
                    return code;
                }
            }




        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult> Put(Book book, int id) 
        {
            var url = "https://fakerestapi.azurewebsites.net/api/v1/books";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.PutAsJsonAsync($"{url}/{id}", book);
                if (respuesta.IsSuccessStatusCode)
                {
                    return Ok();
                }

                else
                {
                    var code = ReturnStatusCode(respuesta.StatusCode);
                    return code;
                }
            }
        }


        [HttpDelete("{id:int}")] 
        public async Task<ActionResult> Delete(int id)
        {
            var url = "https://fakerestapi.azurewebsites.net/api/v1/books";
            using (var httpClient = new HttpClient())
            {
                var respuesta = await httpClient.DeleteAsync($"{url}/{id}");
                if (respuesta.IsSuccessStatusCode)
                {
                    return Ok();
                }

                else
                {
                    var code = ReturnStatusCode(respuesta.StatusCode);
                    return code;
                }
            }



        }

        private ActionResult ReturnStatusCode(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.OK:
                    return Ok();
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case HttpStatusCode.NotFound:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }


    }
}
