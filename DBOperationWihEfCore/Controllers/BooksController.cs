using DBOperationWihEfCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DBOperationWihEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        //  Insert Start from here
        // here inserting on single record
        //===========================================================

        [HttpPost("AdddNewBook")]
        public async Task<IActionResult> AdddNewBook([FromBody] Book model)
        {
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        // Here  we are inserting multiple record at a time in to table

        [HttpPost("Bulk")]
        public async Task<IActionResult> AddingMultipleRecordAtATime([FromBody] List<Book> model)
        {
            _ = appDbContext.Books.AddRangeAsync(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        // Inserting data into multiple table a time
        [HttpPost("DataInsertIntoMTable")]
        public async Task<IActionResult> AddDataintoMtble([FromBody] Book model)
        {
            //var Author = new Author()
            //{
            //    Name = "Alam",
            //    Email = "mdJahangi0782@gmail.com"
            //};
            //model.Author = Author;
            // you can  send data using above concept  or u can send data by using postman/swagger

            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        // Insert end here
        //=============================================================================


        /// <summary>
        /// Start update from Here
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="model"></param>
        /// <returns></returns>


        // Here hiting two times database
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, [FromBody] Book model)
        {
            var book = await appDbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            book.Title = model.Title;
            book.Description = model.Description;

            //book.NumberOfPage = model.NumberOfPage;
            //book.IsActive = model.IsActive;
            //book.CreatedOn = model.CreatedOn;
            //book.LanguageId = model.LanguageId;
            //book.AuthorId = model.AuthorId;
            await appDbContext.SaveChangesAsync();
            return Ok(book);

        }


        // Here only one time data Hit  database 
        // (but one draw back is that if i send data with some specific field, data is update but rest of field is empty)
        [HttpPut("SingleRequest")]
        public async Task<IActionResult> UpdateBookBySingleQuery([FromBody] Book model)
        {
            var book = appDbContext.Books.Update(model);

            await appDbContext.SaveChangesAsync();
            return Ok(book);

        }
        // Here also geting same issue reset field is update , but rest field is empty
        [HttpPut("SingleRequestWithEntityState")]
        public async Task<IActionResult> UpdateBookBySingleWithentityState([FromBody] Book model)
        {
            var result = appDbContext.Entry(model).State = EntityState.Modified;

            await appDbContext.SaveChangesAsync();
            return Ok(result);

        }

        // updating multiple record at a time
        [HttpPut("Bulk")]
        public async Task<IActionResult> UpdateBulk()
        {
            // Here hiting data base multiple time  decrease the  performance of  application
            // and  unneccesary data hit to database


            //var books = appDbContext.Books.ToList();
            //foreach (var item in books)
            //{
            //  item.Title = "Updated Title";
            //}
            // await appDbContext.SaveChangesAsync();
            // return Ok("All record is update");

            //OR

            // So You can use this below technique to avoid multiple hit to
            // database and improve the performance of application 


            await appDbContext.Books.Where(x => x.NumberOfPage >= 10)
                .ExecuteUpdateAsync(x => x
            .SetProperty(b => b.Title, b => b.Title + "Update specific field")
            .SetProperty(b => b.Description, "Updated Description")  // Here updating   specific field
            );

            return Ok("Update Secussfully");
        }




          //  Here starting for delete code
          //===============================================
        [HttpDelete("{bookid}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int bookid)
        {
            // In this case also data delete permanent

            //var book = new Book { Id = bookid };
            //appDbContext.Entry(book).State = EntityState.Deleted;
            //await appDbContext.SaveChangesAsync();

            return Ok("Data deleted succesfull");

            // OR

            //In this case Data is delete  from table
            // But in this case database hit two time one for geting details
            // and second for delete

            //var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookid);
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //appDbContext.Books.Remove(book);
            //await appDbContext.SaveChangesAsync();
            //return Ok();

        }
        [HttpDelete("Bulk")]
        public async Task<IActionResult>DeleteBulkData()
        {
            // Here hiting data base depends upon records
            //var books = await appDbContext.Books.Where(x=>x.Id <5).ToListAsync();

            // appDbContext.Books.RemoveRange(books);
            // await appDbContext.SaveChangesAsync();
            // return Ok();


            // Here one time hiting data base
            // this is the best method to use

            var books = await appDbContext.Books.Where(x => x.Id < 6).ExecuteDeleteAsync();

            return Ok("Data Delete Successfull");

            
        }
        // End delete
        //==============================================
    }
}
