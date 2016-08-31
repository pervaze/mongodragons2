using System.Collections.Generic;
using System;
using System.Net;
using System.Web.Http;
using MongoDragons2.Repository;
using MongoDragons2.Types;

namespace MongoDragons2.Controllers
{
    public class DragonController : ApiController
    {
        public IEnumerable<Dragon> Get()
        {
            IEnumerable<Dragon> dragons = DragonRepository.ToList();
            return dragons;
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
