using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorsApi.Models;
using AuthorsApi.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorRepository _authorRepo;

        public AuthorsController()
        {
            _authorRepo = new AuthorRepository();
        }


        [HttpGet]
        [Route("{genre}/{id}")]// authors/detective/12345
        public async Task<IActionResult> GetAuthorByGenre([FromRoute] string genre, [FromRoute] string id)
        {
            var result = await _authorRepo.GetByIdAsync(id, genre);

            return Ok(result);
        }



        [HttpGet]
        [Route("{genre}")]
        public async Task<IActionResult> GetAuthorsByGenre([FromRoute] string genre)
        {
            var result = await _authorRepo.GetAllAsync(genre);
            return Ok(result);
        }


        [HttpGet]
        [Route("lavan")]
        public async Task<IActionResult> GetAuthorsByLavan([FromRoute] string genre)
        {
            var result = await _authorRepo.GetAllAsync(genre);
            return Ok(result);
        }

        [HttpPut] //idempotency 
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            var result = await _authorRepo.UpdateAsync(author);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            var result = await _authorRepo.CreateAsync(author);
            return Ok(result);
        }


        [HttpPost]
        [Route("bulk-load")]
        public async Task<IActionResult> BulkLoadAuthors([FromBody] List<Author> authors)
        {
            foreach (var author in authors)
            {
                await _authorRepo.CreateAsync(author);
            }
            return Ok("Created");
        }


    }
}
