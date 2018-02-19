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
    [RoutePrefix("api/songs")]
    public class SongsController : BaseApiController
    {
        private IRepository<Song> _repository
            = new SongsRepository(new MySongsDbContext());

        public override IHttpActionResult Get()
        {
            IEnumerable<Song> songs = _repository.Select();

            if (!songs.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });

            List<SongDTO> dtos = _autoMapper.Map<List<Song>, List<SongDTO>>(songs.ToList());
            return Ok(dtos);
        }

        public override IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            Song song = _repository.SelectById(id.Value);

            if (song == null)
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                //return NotFound();

            SongDTO dto = _autoMapper.Map<Song, SongDTO>(song);

            return Content(HttpStatusCode.OK, dto);
        }

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

        [ApplyModelValidation]
        public IHttpActionResult Put(int? id, [FromBody] SongDTO dto)
        {
            try
            {
                if (!id.HasValue)
                    return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                    //return BadRequest();

                Song song = _autoMapper.Map<SongDTO, Song>(dto);

                song.Id = id.Value;

                _repository.Update(song);

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

                Song song = _repository.SelectById(id.Value);

                if (song == null)
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

        [Route("album/{albumName}")]
        public IHttpActionResult Get(string albumName)
        {
            IEnumerable<Song> songs = _repository.SelectWhere(s => s.Album.Name.ToLower().Contains(albumName.ToLower()));

            if (!songs.Any())
                return Content(HttpStatusCode.NotFound, new { message = "The response didn't return any data.", status = HttpStatusCode.NotFound });
                //return NotFound();

            return Ok(_autoMapper.Map<List<Song>, List<SongDTO>>(songs.ToList()));
        }
    }
}
