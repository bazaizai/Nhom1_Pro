﻿using AppData.Models;
using Nhom1_Pro.Models;

namespace AppView.IServices
{
    public interface IProductDetailService
    {
        Task<IEnumerable<ProductDetailDTO>> GetAll();
        Task<ProductDetailDTO> GetById(Guid id);
        Task<List<ProductDetailDTO>> GetByName(string name);
        Task<bool> UpdateItem(ProductDetailPutViewModel item);
        Task<bool> RemoveItem(Guid id);
        Task<bool> AddItem(ProductDetailViewModel obj);
        Task<object> GetAllBienThe();
        Task<ProductDetailPutViewModel> GetProductUpdate(Guid id);
    }
}