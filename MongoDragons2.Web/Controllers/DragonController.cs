using System.Collections.Generic;
using System;
using System.Net;
using System.Web.Http;
using MongoDragons2.Repository;
using MongoDragons2.Types;
using MongoDragons2.Models;

namespace MongoDragons2.Controllers
{
    public class DragonController : ApiController
    {
        public ResponseModel<IEnumerable<Dragon>> Get()
        {
            IEnumerable<Dragon> dragons = DragonRepository.ToList();
            return new ResponseModel<IEnumerable<Dragon>>()
            {
                data = dragons,
                statusCode = 1,
                statusMessage = ""

            };
        }

        public Dragon Spawn()
        {
            Dragon dragon = DragonRepository.Spawn();
            return dragon;
        }

        public bool Remove(Dragon dragon)
        {
            bool result = DragonRepository.Remove(dragon);
            return result;
        }
    }
}
