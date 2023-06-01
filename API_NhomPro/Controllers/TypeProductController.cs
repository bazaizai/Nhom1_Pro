using AppData.IRepositories;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeProductController : ControllerBase
    {
        private readonly IAllRepo<TypeProduct> allRepo;
        DBContextModel dbContextModel = new DBContextModel();
        DbSet<TypeProduct> typeProducts;
        public TypeProductController()
        {
            typeProducts = dbContextModel.TypeProducts;
            allRepo = new AllRepo<TypeProduct>(dbContextModel, typeProducts);
        }

        [HttpGet("{id}")]
        public TypeProduct Get(Guid id)
        {
            return allRepo.GetAll().First(x => x.Id == id);
        }
        [HttpGet]
        public IEnumerable<TypeProduct> Get()
        {
            return allRepo.GetAll();
        }

        [HttpPost]
        public bool CreateBill(string ten,string ma, int trangThai)
        {
            TypeProduct typeProduct = new TypeProduct()
            {
                Id = Guid.NewGuid(),
                Ten = ten,
                Ma = ma,
                TrangThai = trangThai
            };
            return allRepo.AddItem(typeProduct);
        }


        [HttpDelete("Delete-TypeProduct")]
        public bool Delete(Guid id)
        {
            var typeProduct = allRepo.GetAll().First(x => x.Id == id);
            return allRepo.RemoveItem(typeProduct);
        }

        [HttpPut("{id}")]
        public bool Put(string ten, string ma, int trangThai)
        {
            TypeProduct typeProduct = new TypeProduct()
            {
                Ten = ten,
                Ma = ma,
                TrangThai = trangThai
            };
            return allRepo.EditItem(typeProduct);
        }
    }
}

