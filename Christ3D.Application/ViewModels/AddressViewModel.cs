using Christ3D.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Christ3D.Application.ViewModels
{
    /// <summary>
    /// 地址
    /// </summary>
    public class AddressViewModel
    {
        /// <summary>
        /// 省份
        /// </summary>
        [Required(ErrorMessage = "The Province is Required")]
        [DisplayName("Province")]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }



    }
}
