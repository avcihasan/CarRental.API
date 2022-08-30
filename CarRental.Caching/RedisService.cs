using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Caching
{
    public class RedisService
    {
        private readonly string _redisConnection;

        private ConnectionMultiplexer _redis;


        public IDatabase db { get; set; }
        public RedisService(IConfiguration configuration)
        {



            _redisConnection = configuration.GetConnectionString("RedisConnection");
            _redis = ConnectionMultiplexer.Connect(_redisConnection);


        }





        public IDatabase GetDb(int db)
        {

            return _redis.GetDatabase(db);
        }
    }
}
