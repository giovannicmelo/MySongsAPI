using AutoMapper;
using MySongsApi.API.App_Start;
using MySongsApi.API.AutoMapper;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MySongsApi.API.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly IMapper _autoMapper = AutoMapperManager.Instance.Mapper;

        public abstract IHttpActionResult Get();
        public abstract IHttpActionResult Get(int? id);
        public abstract IHttpActionResult Delete(int? id);
    }
}
