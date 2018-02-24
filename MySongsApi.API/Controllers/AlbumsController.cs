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

namespace MySongsApi.API.Controllers
{
    [RoutePrefix("api/albums")]
    public class AlbumsController : BaseApiController
    {
        private IRepository<Album> _repository
             = new AlbumsRepository(new MySongsDbContext());

        /// <summary>
        /// Get a list of Albums
        /// </summary>
        /// <remarks>Return a list of Albums</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="200">Ok</response>
        /// <returns>List of Albums</returns>
        public override IHttpActionResult Get()
        {
            IEnumerable<Album> albums = _repository.Select();

            if (!albums.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<AlbumDTO> dtos = _autoMapper.Map<List<Album>, List<AlbumDTO>>(albums.ToList());

            return Ok(dtos);
        }

        /// <summary>
        /// Get an Album by Id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Return an Album</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <returns>An Album</returns>
        public override IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Album album = _repository.SelectById(id.Value);

            if (album == null)
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                 //return NotFound();

            AlbumDTO dto = _autoMapper.Map<Album, AlbumDTO>(album);

            return Content(HttpStatusCode.OK, dto);
        }

        /// <summary>
        /// Add a new Album
        /// </summary>
        /// <param name="dto">Album DTO</param>
        /// <remarks>Insert a new Album</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="201">Created</response>
        /// <returns>Album added</returns>
        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody] AlbumDTO dto)
        {
            try
            {
                Album album = _autoMapper.Map<AlbumDTO, Album>(dto);

                _repository.Insert(album);

                return Created($"{Request.RequestUri}/{album.Id}", album);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Modify an existing Album
        /// </summary>
        /// <remarks>Update an Album</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="200">Ok</response>
        /// <param name="id">Album Id</param>
        /// <param name="dto">Album Model</param>
        /// <returns></returns>
        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] AlbumDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                Album album = _autoMapper.Map<AlbumDTO, Album>(dto);

                album.Id = id.Value;

                _repository.Update(album);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Remove an Album
        /// </summary>
        /// <remarks>Delete an Album</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <param name="id">Album Id</param>
        /// <returns></returns>
        public override IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return BadRequest();

                Album album = _repository.SelectById(id.Value);

                if (album == null)
                    return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                    //return NotFound();

                _repository.DeleteById(id.Value);

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get a list of Albums by Band
        /// </summary>
        /// <remarks>Return a list of Albums by Band</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="200">Ok</response>
        /// <param name="bandName">Band name</param>
        /// <returns>List of band albums</returns>
        [Route("band/{bandName}")]
        public IHttpActionResult Get(string bandName)
        {
            IEnumerable<Album> albums = _repository.SelectWhere(a => a.Band.Name.ToLower().Contains(bandName.ToLower()));

            if (!albums.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                //return NotFound();

            return Ok(_autoMapper.Map<List<Album>, List<AlbumDTO>>(albums.ToList()));
        }
    }
}
