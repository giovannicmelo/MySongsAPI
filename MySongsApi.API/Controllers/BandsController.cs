using MySongsApi.API.DTO;
using MySongsApi.API.Filters;
using MySongsApi.Domain.Models;
using MySongsApi.Infrastructure.Context;
using MySongsApi.Repositories.Impl;
using MySongsApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MySongsApi.API.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/bands")]
    public class BandsController : BaseApiController
    {
        private IRepository<Band> _repository
            = new BandsRepository(new MySongsDbContext());

        public override IHttpActionResult Get()
        {
            IEnumerable<Band> bands = _repository.Select();

            if (!bands.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<BandDTO> dtos = _autoMapper.Map<List<Band>, List<BandDTO>>(bands.ToList());

            return Ok(dtos);
        }

        public override IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Band band = _repository.SelectById(id.Value);

            if (band == null)
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            BandDTO dto = _autoMapper.Map<Band, BandDTO>(band);

            return Content(HttpStatusCode.OK, dto);
        }

        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody] BandDTO dto)
        {
            try
            {
                Band band = _autoMapper.Map<BandDTO, Band>(dto);

                _repository.Insert(band);

                return Created($"{Request.RequestUri}/{band.Id}", band);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] BandDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

                Band band = _autoMapper.Map<BandDTO, Band>(dto);

                band.Id = id.Value;

                _repository.Update(band);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        public override IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                Band band = _repository.SelectById(id.Value);

                if (band == null)
                    return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

                _repository.DeleteById(id.Value);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
