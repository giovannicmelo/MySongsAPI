using AutoMapper;
using MySongsApi.API.DTO;
using MySongsApi.Domain.Models;
using System;

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
                cfg.CreateMap<Album, AlbumDTO>().ForMember(p => p.Band, opt =>
                {
                    opt.MapFrom(src => src.Band.Name);
                });
                cfg.CreateMap<AlbumDTO, Album>();

                cfg.CreateMap<Song, SongDTO>().ForMember(p => p.Album, opt =>
                {
                    opt.MapFrom(src => src.Album.Name);
                });
                cfg.CreateMap<SongDTO, Song>();

                cfg.CreateMap<Band, BandDTO>();
                cfg.CreateMap<BandDTO, Band>();
            });
        }
        #endregion
    }
}