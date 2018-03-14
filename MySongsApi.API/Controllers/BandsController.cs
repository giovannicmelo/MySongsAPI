using MySongsApi.API.DTO;
using MySongsApi.API.Filters;
using MySongsApi.Domain.Models;
using MySongsApi.Infrastructure.Context;
using MySongsApi.Repositories.Impl;
using MySongsApi.Repositories.Interfaces;
using Ninject;
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
    /// <summary>
    /// Band's API
    /// </summary>
    [RoutePrefix("api/bands")]
    public class BandsController : BaseApiController
    {
        private readonly IBandRepository _repository;
        /// <summary>
        /// Constructor
        /// </summary>
        public BandsController() : base()
        {
            _repository = _kernel.Get<IBandRepository>();
        }

        /// <summary>
        /// Get a list of Bands
        /// </summary>
        /// <remarks>Return a list of Bands</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="200">Ok</response>
        /// <returns>List of Bands</returns>
        public override IHttpActionResult Get()
        {
            IEnumerable<Band> bands = _repository.Select();

            if (!bands.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<BandDTO> dtos = _autoMapper.Map<List<Band>, List<BandDTO>>(bands.ToList());

            return Ok(dtos);
        }

        /// <summary>
        /// Get an Band by Id
        /// </summary>
        /// <param name="id">Band Id</param>
        /// <remarks>Return an Band</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <returns>Band object</returns>
        public override IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return Content(HttpStatusCode.BadRequest, new { message = "Invalid parameter.", status = HttpStatusCode.BadRequest });

            Band band = _repository.SelectById(id.Value);

            if (band == null)
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            BandDTO dto = _autoMapper.Map<Band, BandDTO>(band);

            return Content(HttpStatusCode.OK, dto);
        }

        /// <summary>
        /// Add a new Band
        /// </summary>
        /// <param name="dto">Band DTO</param>
        /// <remarks>Insert a new Band</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="201">Created</response>
        /// <returns>Band added</returns>
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

        /// <summary>
        /// Modify a existing Band
        /// </summary>
        /// <remarks>Update a Band</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <param name="id">Band Id</param>
        /// <param name="dto">Band DTO</param>
        /// <returns>Status of operation</returns>
        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] BandDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return Content(HttpStatusCode.BadRequest, new { message = "The response didn't return any data.", status = HttpStatusCode.BadRequest });

                Band band = _autoMapper.Map<BandDTO, Band>(dto);

                band.Id = id.Value;

                _repository.Update(band);

                return Content(HttpStatusCode.OK, new { message = "The item was successfully updated.", status = HttpStatusCode.OK });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Remove a Band
        /// </summary>
        /// <remarks>Delete a Band</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <param name="id">Band Id</param>
        /// <returns>Status of operation</returns>
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

                return Content(HttpStatusCode.OK, new { message = "The item was successfully removed.", status = HttpStatusCode.OK });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
