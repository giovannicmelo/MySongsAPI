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

        public override IHttpActionResult Get()
        {
            IEnumerable<Album> albums = _repository.Select();

            if (!albums.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<AlbumDTO> dtos = _autoMapper.Map<List<Album>, List<AlbumDTO>>(albums.ToList());

            return Ok(dtos);
        }

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
