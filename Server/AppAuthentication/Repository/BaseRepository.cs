using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AppAuthentication.Interface;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using AppAuthenticationModel.Models;

namespace AppAuthentication.Repository
{
    public class BaseRepository : IBaseRepository, IDisposable
    {
        private static readonly HttpClient client = new HttpClient();
        IConfiguration _configuration;
        private AppSurveyContext _context;
        private bool disposed = false;

        public BaseRepository(AppSurveyContext appSurveyContext, IConfiguration configuration)
        {
            _context = appSurveyContext;
            _configuration = configuration;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
