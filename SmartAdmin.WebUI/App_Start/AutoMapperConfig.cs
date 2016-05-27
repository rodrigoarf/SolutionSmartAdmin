using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SmartAdmin.WebUI.AutoMapper;

namespace SmartAdmin.WebUI.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Mapper.Initialize(x =>
            //{
            //    x.AddProfile<DomainToViewModelMapping>();
            //    x.AddProfile<ViewModelToDomainMapping>();
            //});
        }
    }
}