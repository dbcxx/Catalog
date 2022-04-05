using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
   public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        //GET items
        [HttpGet]
       public async Task<IEnumerable<ItemDto>> GetItemsAsync(/*string name = null*/)
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());

            //if (!string.IsNullOrEmpty(name))
            //{
            //    items = items.Where(item=>item.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            //}
            return items;
        }

        //GET  /id
        [HttpGet("{id}")] 
        public async Task<ActionResult<ItemDto>>  GetItemAsync(Guid id)
        {
            
            var item = await repository.GetItemAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }

        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.AsDto());
            
        }


        //PUT  /item
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id , UpdateItemDto itemDto)
        {
            var existingItem =  await repository.GetItemAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        //DELETE  /item/{ID}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

           await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
