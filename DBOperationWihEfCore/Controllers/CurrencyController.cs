using DBOperationWihEfCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DBOperationWihEfCore.Controllers
{
    [Route("api/Currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("GetAllCurrency")]
        public async Task<IActionResult> GetAllCurrency()
        {

            var result = await _appDbContext.CurrencyTypes
                //.Select(x => new CurrencyType()
                //{
                //    Id = x.Id,
                //    Description = x.Description

                //})
                .ToListAsync();

            return Ok(result);

        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var result = await _appDbContext.Languages.ToListAsync();
            return Ok(result);

        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetCurrencyById( [FromBody ] int id)
        {
            var result = await _appDbContext.CurrencyTypes.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(result);
        }


        [HttpGet("GetByName")]   // For Single Parameter
        public async Task<IActionResult> GetCurrencyByName( [FromBody] string title)
        {
            var result = await _appDbContext.CurrencyTypes.FirstOrDefaultAsync(x => x.Title == title);
            return Ok(result);
        }

        [HttpGet("GetByNameM")]   // With double parameter
        public async Task<IActionResult> GetCurrencyByNameM([FromBody]string title, string description)
        {
            var result = await _appDbContext.CurrencyTypes.FirstOrDefaultAsync(x => x.Title == title && x.Description == description);
            return Ok(result);
        }


        [HttpGet("GetByNameList")]   // With multiple parameter list data
        public async Task<IActionResult> GetCurrencyByName([FromBody] string title, string description)
        {
            var result = await _appDbContext.CurrencyTypes
                .Where(x => x.Title == title && (string.IsNullOrEmpty(description) || x.Description == description)).ToListAsync();
            return Ok(result);
        }

        [HttpGet("all")] // in all select aspecific data
        public async Task<IActionResult> GetallWithChoice()
        {
            var ids = new List<int> { 1, 2, 3 };

            var result = await _appDbContext.CurrencyTypes
                .Where(x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);

        }

        [HttpGet("SelectQuery")]
        public async Task<IActionResult> GetallWithSelectSpecificRecord()
        {
            var ids = new List<int> { 1, 2, 3 };

            var result = await _appDbContext.CurrencyTypes
                .Where(x => ids.Contains(x.Id))
                .Select(x => new CurrencyType()
                {
                    Id = x.Id,
                    Title = x.Title

                })

                .ToListAsync();
            return Ok(result);

        }
    }
}
