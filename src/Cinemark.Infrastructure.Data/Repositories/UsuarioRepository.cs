﻿using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using Cinemark.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Usuario> _mongoCollection;
        private readonly SqlServerContext _sqlServercontext;

        public UsuarioRepository(MongoContext mongoContext,
            SqlServerContext sqlServercontext)
            : base(mongoContext, sqlServercontext)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Usuario>(typeof(Usuario).Name);
            _sqlServercontext = sqlServercontext;
        }

        public async Task<ResultData<Usuario>> GetUsuarioByEmailAndSenhaAsync(Usuario usuario)
        {
            try
            {
                var result = await _mongoCollection.Find(x => x.Email == usuario.Email && x.Senha == usuario.Senha).FirstOrDefaultAsync();

                return new SuccessData<Usuario>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
