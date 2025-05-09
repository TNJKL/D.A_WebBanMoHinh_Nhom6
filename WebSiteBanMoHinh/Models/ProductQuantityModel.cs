﻿using System.ComponentModel.DataAnnotations;

namespace WebSiteBanMoHinh.Models
{
    public class ProductQuantityModel
    {     
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số lượng sản phẩm")]
        public int Quantity { get; set; }

       
        public long ProductId{ get; set; }

        public DateTime DateCreate { get; set; }
        
    }
}
