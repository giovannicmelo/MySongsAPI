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

namespace MySongsApi.API.Controllers
{
    /// <summary>
    /// Song's API
    /// </summary>
    [RoutePrefix("api/songs")]
    public class SongsController : BaseApiController
    {
        private readonly ISongRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        public SongsController() : base()
        {
            _repository = _kernel.Get<ISongRepository>();
        }

        /// <summary>
        /// Get a list of Songs
        /// </summary>
        /// <remarks>Return a list of Songs</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="200">Ok</response>        
        /// <returns>List of Songs</returns>
        public override IHttpActionResult Get()
        {
            IEnumerable<Song> songs = _repository.Select();

            if (!songs.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<SongDTO> dtos = _autoMapper.Map<List<Song>, List<SongDTO>>(songs.ToList());
            return Ok(dtos);
        }

        /// <summary>
        /// Get a Song by Id
        /// </summary>
        /// <param name="id">Song Id</param>
        /// <remarks>Return an Song</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <returns>Song object</returns>
        public override IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return Content(HttpStatusCode.BadRequest, new { message = "Invalid parameter.", status = HttpStatusCode.BadRequest });

            Song song = _repository.SelectById(id.Value);

            if (song == null)
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            SongDTO dto = _autoMapper.Map<Song, SongDTO>(song);

            return Content(HttpStatusCode.OK, dto);
        }

        /// <summary>
        /// Add a new song
        /// </summary>
        /// <param name="dto">Song DTO</param>
        /// <remarks>Insert a new Song</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="201">Created</response>
        /// <returns>Song added</returns>
        [ApplyModelValidation]
        public IHttpActionResult Post([FromBody] SongDTO dto)
        {
            try
            {
                Song song = _autoMapper.Map<SongDTO, Song>(dto);

                _repository.Insert(song);

                return Created($"{Request.RequestUri}/{song.Id}", song);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        /// <summary>
        /// Modify an existing Song
        /// </summary>
        /// <remarks>Update an Song</remarks>
        /// <param name="id">Song Id</param>
        /// <param name="dto">Song DTO</param>
        /// <response code="500">Internal Server Error</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <returns></returns>
        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] SongDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return Content(HttpStatusCode.BadRequest, new { message = "Invalid parameter.", status = HttpStatusCode.BadRequest });

                Song song = _autoMapper.Map<SongDTO, Song>(dto);

                song.Id = id.Value;

                _repository.Update(song);

                return Content(HttpStatusCode.OK, new { message = "The item was successfully updated.", status = HttpStatusCode.OK });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Remove a Song
        /// </summary>
        /// <remarks>Delete a Song</remarks>
        /// <response code="500">Internal Server Error</response>
        /// <response code="404">Not Found</response>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <param name="id">Song Id</param>
        /// <returns>Status of operation</returns>
        public override IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return Content(HttpStatusCode.BadRequest, new { message = "Invalid parameter.", status = HttpStatusCode.BadRequest });

                Song song = _repository.SelectById(id.Value);

                if (song == null)
                    return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

                _repository.DeleteById(id.Value);

                return Content(HttpStatusCode.OK, new { message = "The item was successfully removed.", status = HttpStatusCode.OK });
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Get a list of Songs by Album
        /// </summary>
        /// <remarks>Return a list of Songs by Album</remarks>
        /// <response code="404">Not Found</response>
        /// <response code="200">Ok</response>
        /// <param name="albumName">Album name</param>
        /// <returns>List of album songs</returns>
        [Route("album/{albumName}")]
        public IHttpActionResult Get(string albumName)
        {
            IEnumerable<Song> songs = _repository.SelectWhere(s => s.Album.Name.ToLower().Contains(albumName.ToLower()));

            if (!songs.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            return Ok(_autoMapper.Map<List<Song>, List<SongDTO>>(songs.ToList()));
        }
    }
}
