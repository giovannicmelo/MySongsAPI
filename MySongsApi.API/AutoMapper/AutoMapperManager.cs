using AutoMapper;
using MySongsApi.API.DTO;
using MySongsApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySongsApi.API.AutoMapper
{
    public class AutoMapperManager
    {
        #region Properties
        private static readonly Lazy<AutoMapperManager> _instance = new Lazy<AutoMapperManager>(() =>
        {
            return new AutoMapperManager();
        });

        public static AutoMapperManager Instance { get { return _instance.Value; } }

        public IMapper Mapper { get { return _config.CreateMapper(); } }

        private MapperConfiguration _config;
        #endregion

        #region Constructor
        private AutoMapperManager()
        {
            _config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<Album, AlbumDTO>();
                cfg.CreateMap<AlbumDTO, Album>();

                cfg.CreateMap<Song, SongDTO>();
                cfg.CreateMap<SongDTO, Song>();

                cfg.CreateMap<Band, BandDTO>();
                cfg.CreateMap<BandDTO, Band>();
            });
        }
        #endregion
    }
}